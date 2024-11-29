using PaymentService.CibPayIntegration.Models.BaseResponse;
using PaymentService.CibPayIntegration.Models.CreateOrder.Command;
using PaymentService.CibPayIntegration.Models.CreateOrder.CreateResponse;
using PaymentService.CibPayIntegration.Models.GetOrder.Requests;
using PaymentService.CibPayIntegration.Models.GetOrder.Responses;
using PaymentService.CibPayIntegration.Models.Ping.Responses;
using PaymentService.CibPayIntegration.Models.RefundOrder.Requests;

namespace PaymentService.CibPayIntegration.Abstractions;
public interface ICibPayService
{
    Task<CibBaseResponse<PingResponse>> GetPingResponseAsync();
    Task<CibBaseResponse<GetPaymentResponse>> GetOrderInfoAsync(string orderId);
    Task<CibBaseResponse<List<GetPaymentResponse>>> GetOrdersListAsync(GetOrdersRequest query);
    Task<CreateOrderResponse> CreateOrderAsync(CreateOrderCommand createOrderCommand);
    Task<CibBaseResponse<List<GetPaymentResponse>>> RefundOrderAsync(RefundOrderRequest refundOrderCommand);
}