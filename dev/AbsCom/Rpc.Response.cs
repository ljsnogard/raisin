namespace Raisin.AbsCom.Rpc
{
    using Raisin.AbsBuff;

    using System.Collections.Generic;

    public readonly struct ResponseStatus(ushort code)
    {
        public readonly ushort Code = code;
    }

    public interface IResponse
    {
        public ResponseStatus Status { get; }

        public IAsyncEnumerable<HeaderRead> Headers { get; }

        public IBuffRead<byte> Payload { get; }
    }

    public static class ResponseStatusExtensions
    {
        public static bool IsOk(this ResponseStatus status)
            => throw new NotImplementedException();

        public static bool IsClientError(this ResponseStatus status)
            => throw new NotImplementedException();

        public static bool IsServerError(this ResponseStatus status)
            => throw new NotImplementedException();
    }
}
