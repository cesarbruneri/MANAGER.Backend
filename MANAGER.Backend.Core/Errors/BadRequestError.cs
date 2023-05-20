using FluentResults;

namespace MANAGER.Backend.Core.Errors;

public class BadRequestError : Error
{
    public BadRequestError(string code) : base(code) => Metadata.Add(code, 500);

    public static BadRequestError InvalidFields() => new("invalidfields");

}
