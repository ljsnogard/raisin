namespace Raisin.AbsCom.Rpc
{
    using System.Threading;

    using Cysharp.Threading.Tasks;

    /// <summary>
    /// 为客户端的异步操作提供回调式 API，配合扩展来避免不同平台下 Task 导致的 GC 压力
    /// </summary>
    public interface IRequestAsync
    {
        public Uri Command { get; }

        public UniTask<IResponse> GetResponseAsync(CancellationToken token = default);
    }

    public interface IRequestAsync<TResponse>: IRequestAsync
        where TResponse : IResponse
    {
        public new UniTask<TResponse> GetResponseAsync(CancellationToken token = default);
    }
}
