using A.Domain.Entities;
using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using C.Service.Payment.CibPay.Implementations;
using E.Application.DTOs;
using PaymentService.CibPayIntegration.Models.CreateOrder.Command;

namespace E.Application.DonationService;

public class DonateService : IDonateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CibPayService _cibPayService;
    public DonateService(IUnitOfWork unitOfWork, CibPayService cibPayService)
    {
        _unitOfWork = unitOfWork;
        _cibPayService = cibPayService;
    }

    public async Task<bool> CreateFinishedOrderAsync(string orderId)
    {
        var pendingPayment = await _unitOfWork.PaymentRepository.GetPendingPaymentByDescriminatorAsync(orderId);
        if (pendingPayment == null)
            throw new BadRequestException("ödəniş uğurlu olmadı");

        var payment = new Payment()
        {
            CreatedDate = DateTime.Now,
            PendingPaymentId = pendingPayment.Id,
        };

        await _unitOfWork.PaymentRepository.CreateFinishedPaymentAsync(payment);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<TypedResponseModel<PaymentLinkDto>> CreateOrderAsync(decimal amount)
    {
        var createOrderCommand = new CreateOrderCommand()
        {
            Amount = amount,
            Name = $"anonyms",
            OrderNumber = Guid.NewGuid().ToString(),
            UserId = 1.ToString()
        };

        var createOrderResult = await _cibPayService.CreateOrderAsync(createOrderCommand);


        if (!(createOrderResult?.Succeeded ?? false))
            throw new Exception(createOrderResult?.Message);

        if (!createOrderResult.Succeeded)
            throw new Exception("Response is null");

        var pendingPayment = new PendingPayment()
        {
            CreatedDate = DateTime.Now,
            Discriminator = createOrderResult.OrderId,
        };

        await _unitOfWork.PaymentRepository.CreatePendingPaymentAsync(pendingPayment);

        await _unitOfWork.SaveChangesAsync();

        return new TypedResponseModel<PaymentLinkDto>
        {
            Data = new PaymentLinkDto(createOrderResult.PaymentLink),
            IsSuccess = true,
            Errors = null!
        };
    }
}
