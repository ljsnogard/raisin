namespace Raisin.AbsCom.Rpc
{
    using System.Collections.Generic;
    using System.IO.Pipelines;
    using System.Threading.Tasks;

    /// <summary>
    /// 用于读取 Request 或 Response 中的带有 key 的一段数据
    /// </summary>
    public readonly struct SegmentRead
    {
        public string Key { get; init; }
        public PipeReader Reader { get; init; }
    }

    /// <summary>
    /// 用于写入 Request 或 Response 中的带有 key 的一段数据
    /// </summary>
    public readonly struct SegmentWrite
    {
        public string Key { get; init; }
        public PipeWriter Writer { get; init; }
    }

    public readonly struct NoSegments: IAsyncEnumerable<SegmentRead>, IAsyncEnumerable<SegmentWrite>
    {
        IAsyncEnumerator<SegmentRead> IAsyncEnumerable<SegmentRead>.GetAsyncEnumerator(CancellationToken token)
        {
            return GenAsyncEnumerable_().GetAsyncEnumerator(token);

            static async IAsyncEnumerable<SegmentRead> GenAsyncEnumerable_()
            {
                await Task.Yield();
                yield break;
            }
        }

        IAsyncEnumerator<SegmentWrite> IAsyncEnumerable<SegmentWrite>.GetAsyncEnumerator(CancellationToken token)
        {
            return GenAsyncEnumerable_().GetAsyncEnumerator(token);

            static async IAsyncEnumerable<SegmentWrite> GenAsyncEnumerable_()
            {
                await Task.Yield();
                yield break;
            }
        }
    }
}
