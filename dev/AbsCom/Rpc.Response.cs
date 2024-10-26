namespace Raisin.AbsCom.Rpc
{
    using System.Collections.Generic;

    public enum ResponseStatus
    {
        Ok = 200,
    }

    public interface IResponse
    {
        public ResponseStatus Status { get; }

        public IAsyncEnumerable<SegmentRead> Segments { get; }
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
