using C.Common.GlobalResponses.Generics;
using E.Application.DTOs;

namespace E.Application.DonationService;

public interface IDonateService
{
    Task<TypedResponseModel<PaymentLinkDto>> CreateOrderAsync(decimal amount);
    Task<bool> CreateFinishedOrderAsync(string orderId);
}
