using FluentResults;

namespace MANAGER.Backend.Core.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string code) : base(code) => Metadata.Add(code, 404);

    public static NotFoundError UserNotFound() => new("usernotfound");
}
