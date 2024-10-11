namespace C.Common.Exceptions;

public class PermissionDeniedErrorException : Exception
{
    public PermissionDeniedErrorException() : base("You don't have permission to access this endpoint")

    {

    }
}
