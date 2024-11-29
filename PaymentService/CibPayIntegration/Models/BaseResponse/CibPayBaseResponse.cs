using System.Net;

namespace PaymentService.CibPayIntegration.Models.BaseResponse;

public class CibPayBaseResponse
{
    public string? failure_type { get; set; }
    public string? failure_message { get; set; }
    public string? order_id { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}

public class CibPayBaseResponse<T> : CibPayBaseResponse
{
    public T? Data { get; set; }
}