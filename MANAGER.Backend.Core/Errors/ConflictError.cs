using FluentResults;

namespace MANAGER.Backend.Core.Errors
{
    public class ConflictError : Error
    {
        public ConflictError(string code) : base(code) => Metadata.Add(code, 409);

        public static ConflictError UserAlreadyExists() => new("useralreadyexists");
    }
}
