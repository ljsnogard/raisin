namespace Raisin.AbsCom.Rpc
{
    using Cysharp.Threading.Tasks;

    using OneOf;
    using OneOf.Types;

    using Raisin.AbsBuff;

    public readonly struct StreamStatus(byte code)
    {
        public readonly byte Code = code;
    }

    /// <summary>
    /// 从服务端拉取的数据包
    /// </summary>
    public interface IStreamItem
    {
        public StreamStatus Status { get; }

        /// <summary>
        /// 推送数据包的头部
        /// </summary>
        public IAsyncEnumerable<HeaderRead> Headers { get; }

        /// <summary>
        /// 推送数据包的主体数据
        /// </summary>
        public IBuffRead<byte> Payload { get; }
    }

    public interface IPullError
    { }

    public interface IPullAgent
    {
        public Uri Location { get; }

        public UniTask<OneOf<IStreamItem, IPullError>> ReceiveAsync(CancellationToken token = default);
    }

    public interface IPushError
    { }

    public interface IPushAgent
    {
        public Uri Location { get; }

        public UniTask<OneOf<IPushError, None>> SendItemAsync<THeaders, TPayload>(THeaders headers, TPayload payload, CancellationToken token = default)
            where THeaders : IAsyncEnumerable<HeaderRead>
            where TPayload : IBuffRead<byte>;
    }
}
