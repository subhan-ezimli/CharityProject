using C.Common.GlobalResponses.Generics;
using E.Application.DTOs;

namespace E.Application.PaymentProccess;

public interface IOrderService
{
    Task<bool> CreateFinishedOrderAsync(string orderId);

    Task<TypedResponseModel<PaymentLinkDto>> CreateOrderAsync(decimal amount);
}
