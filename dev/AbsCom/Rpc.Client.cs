namespace Raisin.AbsCom.Rpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cysharp.Threading.Tasks;

    public interface IClient<TReqAsync, TResponse>
        where TResponse : IResponse
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="P"></typeparam>
        /// <param name="command">所请求执行的程序的 Uri</param>
        /// <param name="parameters">执行程序所需的参数</param>
        /// <returns></returns>
        public UniTask<TResponse> Request<P>(Uri command, P parameters)
            where P : IAsyncEnumerable<SegmentRead>;
    }

    /// <summary>
    /// C# 13 preview is suggested for using ref struct in async method.
    /// </summary>
    static class Demo
    {
        static async Task ClientDemo<TClient, TReqAsync, TResponse>(TClient client)
            where TClient: IClient<TReqAsync, TResponse>
            where TResponse: IResponse
        {
            var response = await client.Request(new Uri("/"), new NoSegments());
            await foreach (var seg in response.Segments)
            {
                var key = seg.Key;
                var slice = await seg.Reader.ReadAsync(1);
                var span = slice.AsReadOnlySpan();
            }
        }
    }
}
