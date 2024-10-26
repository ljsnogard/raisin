namespace Raisin.AbsBuff
{
    using System.Threading;

    using Cysharp.Threading.Tasks;

    public interface IBuffRead<TData>
    {
        public bool CanRead { get; }

        public UniTask<ISliceRef<TData>> ReadAsync(uint length, CancellationToken token = default);
    }

    public interface IBuffRead<TSliceRef, TData>: IBuffRead<TData>
        where TSliceRef: struct, ISliceRef<TData, TSliceRef>
    {
        public new UniTask<TSliceRef> ReadAsync(uint length, CancellationToken token = default);
    }
}
