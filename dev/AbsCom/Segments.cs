namespace Raisin.AbsCom
{
    using System.Collections.Generic;
    using System.IO.Pipelines;
    using System.Threading.Tasks;

    public readonly struct SegmentRead
    {
        public string Key { get; init; }
        public PipeReader Reader { get; init; }
    }

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
