namespace E.Application.DTOs;

public class PaymentLinkDto
{
    public string Link { get; set; }
    public PaymentLinkDto(string link)
    {
        Link = link;
    }
    public PaymentLinkDto()
    {
        Link = null!;
    }
}
