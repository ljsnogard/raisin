namespace Raisin.AbsCom
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IIsReady
    {
        public bool IsReady { get; }
    }

    public interface IHasTryGetFn<T>: IDisposable, IIsReady
    {
        public Func<DotNext.Optional<T>> TryGetFn { get; }
    }

    public interface ITryGet<T>: IDisposable, IIsReady
    {
        public DotNext.Optional<T> TryGet();
    }

    public interface IHasReadyEvent<TSource>
    {
        public delegate void FnReportReady(in TSource source);

        public event FnReportReady Ready;
    }

    public static class IHasTryGetFnExtensions
    {
        public static async ValueTask<TResult> IntoValueTask<TFetch, TResult>(this TFetch fetch, CancellationToken token = default)
            where TFetch : IHasTryGetFn<TResult>, IHasReadyEvent<TFetch>
        {
            async Task<TResult> Fetch_()
            {
                var fn = fetch.TryGetFn;
                if (fn().TryGet(out var result))
                    return result;
                var src = new TaskCompletionSource<TResult>();
                fetch.Ready += (in TFetch f) =>
                {
                    var fn = f.TryGetFn;
                    if (fn().TryGet(out var r) && !token.IsCancellationRequested)
                        src.TrySetResult(r);
                };
                return await src.Task;
            }
            try
            {
                var task = Task.Run(Fetch_, token);
                return await task;
            }
            finally
            {
                fetch.Dispose();
            }
        }
    }

    public static class ITryGetExtensions
    {
        public static async ValueTask<TResult> IntoValueTask<TFetch, TResult>(this TFetch fetch, CancellationToken token = default)
            where TFetch : struct, ITryGet<TResult>, IHasReadyEvent<TFetch>
        {
            Task<TResult> Fetch_()
            {
                if (fetch.IsReady)
                    return Task.FromResult(fetch.TryGet().Value);
                var src = new TaskCompletionSource<TResult>();
                fetch.Ready += (in TFetch f) =>
                {
                    if (f.IsReady && !token.IsCancellationRequested)
                        src.TrySetResult(f.TryGet().Value);
                };
                return src.Task;
            }
            try
            {
                var task = Task.Run(Fetch_, token);
                return await task;
            }
            finally
            {
                fetch.Dispose();
            }
        }
    }
}
