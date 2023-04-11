using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

internal class DomainProblemDetails : ProblemDetails
{
    public DomainProblemDetails(string detail)
    {
        Title = "Domain error";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://example.com/probs/authorization";
    }
}



