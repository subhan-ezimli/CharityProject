
using A.Domain.Entities;
using B.Repository.Common;
using C.Common.Exceptions;
using C.Common.GlobalResponses.Generics;
using E.Application.DTOs;
using E.Application.Security;
using PaymentService.CibPayIntegration.Implementations;
using PaymentService.CibPayIntegration.Models.CreateOrder.Command;

namespace E.Application.PaymentProccess
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CibPayService _cibPayService;
        public OrderService(IUnitOfWork unitOfWork, CibPayService cibPayService)
        {
            _unitOfWork = unitOfWork;
            _cibPayService = cibPayService;
        }

        public async Task<bool> CreateFinishedOrderAsync(string orderId)
        {
            var pendingPayment = await _unitOfWork.PaymentRepository.GetPendingPaymentByDescriminatorAsync(orderId);
            if (pendingPayment == null)
                throw new BadRequestException("PaymentNotAvailable");

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
                // Name = $"{currentUser.Name} {currentUser.Surname}",
                Name = "test",
                OrderNumber = Guid.NewGuid().ToString(),
                UserId = "1"
            };

            var createOrderResult = await _cibPayService.CreateOrderAsync(createOrderCommand);


            if (!(createOrderResult?.Succeeded ?? false))
                throw new Exception(createOrderResult?.Message);

            if (!createOrderResult.Succeeded)
                throw new Exception("Response is null");

            //var pendingPayment = new PendingPayment()
            //{
            //    CreatedDate = DateTime.Now,
            //    UserId = 1,
            //    Discriminator = createOrderResult.OrderId,
            //};

            //await _unitOfWork.PaymentRepository.CreatePendingPaymentAsync(pendingPayment);
            //await _unitOfWork.SaveChangesAsync();

            return new TypedResponseModel<PaymentLinkDto>
            {
                Data = new PaymentLinkDto(createOrderResult.PaymentLink),
                IsSuccess = true,
                Errors = null!
            };
        }

    }
}
