using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentService.CibPayIntegration.Certificate;
using PaymentService.CibPayIntegration.Models.BaseResponse;
using PaymentService.CibPayIntegration.Models.CreateOrder.Command;
using PaymentService.CibPayIntegration.Models.CreateOrder.CreateResponse;
using PaymentService.CibPayIntegration.Models.GetOrder.Requests;
using PaymentService.CibPayIntegration.Models.GetOrder.Responses;
using PaymentService.CibPayIntegration.Models.Ping.Responses;
using PaymentService.CibPayIntegration.Models.RefundOrder.Requests;

namespace PaymentService.CibPayIntegration.Implementations;

public class CibPayService
{
    private readonly HttpClient _httpClient;
    private readonly X509Certificate2 _clientCertificate;
    private readonly string _credentials;
    private readonly string _baseUrl;
    private readonly string _paymentUrl;
    private readonly string username;
    private readonly string password;
    private readonly string _returnUrl;
    private readonly bool _autoCharge;
    private readonly byte _force3D;
    private readonly string _currency;
    private readonly string _expirationTimeout;
    private readonly string _language;


    public CibPayService(HttpClient httpClient,
                       IConfiguration configuration)
    {
        _httpClient = httpClient;
        username = configuration["CibPay:Username"];
        password = configuration["CibPay:Password"];
        _baseUrl = configuration["CibPay:BaseUrl"];
        _returnUrl = configuration["CibPay:ReturnUrl"];
        _paymentUrl = configuration["CibPay:PaymentUrl"];
        _autoCharge = bool.Parse(configuration["CibPay:AutoCharge"]);
        _force3D = byte.Parse(configuration["CibPay:Force3D"]);
        _currency = configuration["CibPay:Currency"];
        _expirationTimeout = configuration["CibPay:ExpirationTimeout"];
        _language = configuration["CibPay:Language"];
        _clientCertificate = GetCertificate();
        _credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));
        ConfigureHttpClient();
    }

    private void ConfigureHttpClient()
    {
        _httpClient.DefaultRequestHeaders.Accept.Clear();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _credentials);
    }


    private async Task<CibBaseResponse<T>> SendRequestAsync<T>(string endpoint, HttpMethod method, object requestData = null)
    {
        using var handler = new HttpClientHandler { ClientCertificates = { _clientCertificate } };

        using var httpClientWithCertificate = new HttpClient(handler);
        httpClientWithCertificate.BaseAddress = _httpClient.BaseAddress;
        httpClientWithCertificate.DefaultRequestHeaders.Authorization = _httpClient.DefaultRequestHeaders.Authorization;

        using var request = new HttpRequestMessage(method, endpoint);

        var response = new CibBaseResponse<T>();

        if (requestData is not null)
        {
            var jsonContent = JsonConvert.SerializeObject(requestData);
            request.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        using var providerResponse = await httpClientWithCertificate.SendAsync(request);

        response.StatusCode = providerResponse.StatusCode;

        var stringResponseContent = await providerResponse.Content.ReadAsStringAsync();

        if (providerResponse.IsSuccessStatusCode)
        {
            response.Data = JsonConvert.DeserializeObject<T>(stringResponseContent);
        }
        else
        {
            var deserializedResponseContent = JsonConvert.DeserializeObject<CibPayBaseResponse>(stringResponseContent);

            response.failure_message = deserializedResponseContent.failure_message;
            response.failure_type = deserializedResponseContent.failure_type;
            response.order_id = deserializedResponseContent.order_id;
        }
        return response;
    }

    public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderCommand command)
    {
        try
        {
            var endpoint = "orders/create";

            var requestData = new
            {
                amount = command.Amount,
                currency = _currency,
                extra_fields = new { user_id = command.UserId },
                merchant_order_id = command.OrderNumber,
                options = new
                {
                    auto_charge = _autoCharge,
                    expiration_timeout = _expirationTimeout,
                    force3d = _force3D,
                    language = _language,
                    return_url = _returnUrl
                },

                client = new
                {
                    name = command.Name,
                    email = "noreply@birdoc.az"
                }
            };

            var result = await SendRequestAsync<CreateOrderProviderResponse>(endpoint, HttpMethod.Post, requestData);

            if (!string.IsNullOrEmpty(result.failure_message))
                return new CreateOrderResponse(false, result.failure_message);

            return new CreateOrderResponse($"{_paymentUrl}{result.Data.orders.First().id}", result.Data.orders.First().id);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred: " + ex.InnerException.Message, ex);
        }
    }


    public async Task<CibBaseResponse<GetPaymentResponse>> GetOrderInfoAsync(string orderId)
    {
        try
        {
            var endpoint = $"orders/{orderId}";
            return await SendRequestAsync<GetPaymentResponse>(endpoint, HttpMethod.Get);

        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred: " + ex.Message, ex);
        }
    }

    public async Task<CibBaseResponse<List<GetPaymentResponse>>> GetOrdersListAsync(GetOrdersRequest query)
    {
        try
        {
            var endpoint = "orders/";
            var queryParameters = new Dictionary<string, string>
            {
                ["status"] = query.Status,
                ["created_from"] = query.CreatedFrom?.ToString("yyyy-MM-dd HH:mm:ss"),
                ["created_to"] = query.CreatedTo?.ToString("yyyy-MM-dd HH:mm:ss"),
                ["merchant_order_id"] = query.MerchantOrderId,
                ["card.type"] = query.CardType,
                ["card.subtype"] = query.CardSubtype,
                ["location.ip"] = query.IpAddress,
                ["expand"] = query.Expand
            };

            endpoint = QueryHelpers.AddQueryString(endpoint, queryParameters);

            return await SendRequestAsync<List<GetPaymentResponse>>(endpoint, HttpMethod.Get); ;
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred: " + ex.Message, ex);
        }
    }

    public async Task<CibBaseResponse<PingResponse>> GetPingResponseAsync()
    {
        try
        {
            var endpoint = "ping";
            return await SendRequestAsync<PingResponse>(endpoint, HttpMethod.Get);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred: " + ex.Message, ex);
        }
    }


    public async Task<CibBaseResponse<List<GetPaymentResponse>>> RefundOrderAsync(RefundOrderRequest refundOrderCommand)
    {
        try
        {
            var endpoint = $"orders/{refundOrderCommand.OrderId}/refund";

            var requestData = new
            {
                amount = refundOrderCommand.RefundAmount,
            };

            return await SendRequestAsync<List<GetPaymentResponse>>(endpoint, HttpMethod.Put, requestData);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred: " + ex.Message, ex);
        }
    }

    private X509Certificate2 GetCertificate()
    {
        //var cPath = new CertificatePath();
        //string a = cPath.CurrentPath;
        // return new X509Certificate2(cPath.CurrentPath, "nBR2SFVWZ02g");
        return new X509Certificate2("D:\\Repos\\CharityProject\\PaymentService\\CibPayIntegration\\Certificate\\taxiapp.p12", "nBR2SFVWZ02g");
    }
}
