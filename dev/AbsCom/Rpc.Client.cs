namespace Raisin.AbsCom.Rpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Cysharp.Threading.Tasks;
    
    using OneOf;

    using Raisin.AbsBuff;

    public interface IClientError
    { }

    public interface IClient
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <typeparam name="THeaders"></typeparam>
        /// <typeparam name="TPayload"></typeparam>
        /// <param name="location">所请求执行的程序的位置 Uri 表示</param>
        /// <param name="headers">零个或多个请求头数据</param>
        /// <param name="payload">请求体自身的数据</param>
        /// <returns></returns>
        public UniTask<OneOf<IRequest, IClientError>> Request<THeaders, TPayload>(Uri location, THeaders headers, TPayload payload)
            where THeaders : IAsyncEnumerable<HeaderRead>
            where TPayload : IBuffRead<byte>;

        /// <summary>
        /// 拉取数据流
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public UniTask<OneOf<IPullAgent, IClientError>> Pull(Uri location);

        /// <summary>
        /// 推送数据流
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public UniTask<OneOf<IPushAgent, IClientError>> Push(Uri location);
    }

    /// <summary>
    /// C# 13 preview is suggested for using ref struct in async method.
    /// </summary>
    static class Demo
    {
        private readonly struct DummyBuffIo<T>: IBuffRead<T>, IBuffWrite<T>
        {
            public UniTask<OneOf<ISliceRef<T>, IBuffReadError>> ReadAsync(uint length, CancellationToken token = default)
                => throw new NotImplementedException();

            public UniTask<OneOf<ISliceMut<T>, IBuffWriteError>> WriteAsync(uint length, CancellationToken token = default)
                => throw new NotImplementedException();
        }

        static async Task ClientDemo<TClient>(TClient client)
            where TClient: IClient
        {
            // 向服务器发送请求
            var makeReq = await client.Request(new Uri("/"), new NoHeaders(), new DummyBuffIo<byte>());
            if (!makeReq.TryPickT0(out var agent, out var clientErr))
                throw new Exception(clientErr.ToString());

            // 从服务器接收回复
            var getResp = await agent.GetResponseAsync();
            if (!getResp.TryPickT0(out var response, out var rpcErr))
                throw  new Exception(rpcErr.ToString());

            // 解析回复数据
            await foreach (var header in response.Headers)
            {
                var key = header.Key;
                var slice = await header.Reader.ReadAsync(1);
            }

            // 向服务器上的某个流推送数据
            var makePush = await client.Push(new Uri("/"));
            if (!makePush.TryPickT0(out var pushAgent, out var makePushErr))
                throw new Exception(makePushErr.ToString());

            // 推送过程
            var pushTask = UniTask.Run(async () =>
            {
                while (true)
                {
                    var x = await pushAgent.SendItemAsync(
                        new NoHeaders(),
                        new DummyBuffIo<byte>()
                    );
                    if (x.IsT0)
                        break;
                }
            });

            // 从服务器上的某个流拉取数据
            var makePull = await client.Pull(new Uri("/"));
            if (makePull.TryPickT0(out var pullAgent, out var makePullErr))
                throw new Exception(makePullErr.ToString());

            // 拉取过程
            var pullTask = UniTask.Run(async () =>
            {
                while (true)
                {
                    var x = await pullAgent.ReceiveAsync();
                    if (!x.TryPickT0(out var item, out var pullErr))
                        break;
                    // 解析拉取到的对象
                    await foreach (var header in item.Headers)
                    {
                        var key = header.Key;
                        var slice = await header.Reader.ReadAsync(1);
                    }
                }
            });
        }
    }
}
