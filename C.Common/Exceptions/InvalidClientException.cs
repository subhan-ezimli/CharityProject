using System.ComponentModel;
using System.Text;

namespace C.Common.Exceptions;

public class InvalidClientException : Exception
{

    public InvalidClientException(string message) : base(message)
    {

    }

    public InvalidClientException(IEnumerable<string> messages) : base(FormattedMessages(messages))
    {

    }

    private static string FormattedMessages(IEnumerable<string> messages)
    {
        var sb = new StringBuilder();

        foreach (var message in messages)
        {

            sb.AppendLine(message);
        }
        return sb.ToString();
    }


}
