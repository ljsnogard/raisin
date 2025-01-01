namespace Raisin.AbsCom.Rpc
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Raisin.AbsBuff;

    /// <summary>
    /// 用于读取 Request 或 Response 中的带有 key 的一段数据
    /// </summary>
    public readonly struct HeaderRead
    {
        public string Key { get; init; }
        public IBuffRead<byte> Reader { get; init; }
    }

    /// <summary>
    /// 用于写入 Request 或 Response 中的带有 key 的一段数据
    /// </summary>
    public readonly struct HeaderWrite
    {
        public string Key { get; init; }
        public IBuffWrite<byte> Writer { get; init; }
    }

    public readonly struct NoHeaders: IAsyncEnumerable<HeaderRead>, IAsyncEnumerable<HeaderWrite>
    {
        IAsyncEnumerator<HeaderRead> IAsyncEnumerable<HeaderRead>.GetAsyncEnumerator(CancellationToken token)
        {
            return GenAsyncEnumerable_().GetAsyncEnumerator(token);

            static async IAsyncEnumerable<HeaderRead> GenAsyncEnumerable_()
            {
                await Task.Yield();
                yield break;
            }
        }

        IAsyncEnumerator<HeaderWrite> IAsyncEnumerable<HeaderWrite>.GetAsyncEnumerator(CancellationToken token)
        {
            return GenAsyncEnumerable_().GetAsyncEnumerator(token);

            static async IAsyncEnumerable<HeaderWrite> GenAsyncEnumerable_()
            {
                await Task.Yield();
                yield break;
            }
        }
    }
}
