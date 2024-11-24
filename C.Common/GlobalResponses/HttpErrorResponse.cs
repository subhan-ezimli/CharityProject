namespace C.Common.GlobalResponses;

public class HttpErrorResponse
{
    public List<string> Errors { get; set; }
    public bool IsSuccess { get; set; }

    public HttpErrorResponse(List<string> errorMessages)
    {
        Errors = errorMessages;
        IsSuccess = false;

    }
}
