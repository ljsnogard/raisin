namespace Raisin.AbsCom.Rpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static Raisin.AbsCom.ITryGetExtensions;

    public interface IClient<TReqAsync, TResponse>
        where TReqAsync : IRequestAsync<TResponse>
        where TResponse : IResponse
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="command">所请求执行的程序的 Uri</param>
        /// <param name="parameters">执行程序所需的参数</param>
        /// <returns></returns>
        public TReqAsync Request<P>(Uri command, P parameters)
            where P : IAsyncEnumerable<SegmentRead>;
    }

    static class Demo
    {
        static async Task ClientDemo<TClient, TReqAsync, TResponse>(TClient client)
            where TClient: IClient<TReqAsync, TResponse>
            where TReqAsync: struct, IRequestAsync<TResponse>, IHasReadyEvent<TReqAsync>
            where TResponse: IResponse
        {
            var response = await client
                .Request(new Uri("/"), new NoSegments())
                .IntoValueTask<TReqAsync, TResponse>();
            if (response.Status.IsOk())
            {

            }
            switch (response.Status)
            {
                case ResponseStatus.Ok: break;
                default: break;
            }
        }
    }
}
