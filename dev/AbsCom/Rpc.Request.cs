namespace Raisin.AbsCom.Rpc
{
    using System.Threading;

    using Cysharp.Threading.Tasks;
    
    using OneOf;

    /// <summary>
    /// 供客户端使用的 RPC 请求操作符
    /// </summary>
    public interface IRequest
    {
        public Uri Command { get; }

        public UniTask<OneOf<IResponse, IRpcError>> GetResponseAsync(CancellationToken token = default);
    }

    public interface IRequest<TResponse, TErr>: IRequest
        where TResponse : IResponse
        where TErr: IRpcError
    {
        public new UniTask<OneOf<TResponse, TErr>> GetResponseAsync(CancellationToken token = default);
    }
}
