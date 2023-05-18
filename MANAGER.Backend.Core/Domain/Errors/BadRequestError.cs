using FluentResults;

namespace MANAGER.Backend.Core.Domain.Errors;

public class BadRequestError : Error
{
    public BadRequestError(string code) : base(code) => Metadata.Add(code, 500);

    public static BadRequestError NameIsNotEmpty() => new("nameisnotempty");

    public static BadRequestError InvalidFields() => new("invalidfields");

}
