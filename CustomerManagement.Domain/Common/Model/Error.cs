using System.Net;

namespace CustomerManagement.Core.Common.Model;

public record Error(HttpStatusCode Status, string ErrorMessage)
{
    public static Error None = new(HttpStatusCode.InternalServerError, string.Empty);
}