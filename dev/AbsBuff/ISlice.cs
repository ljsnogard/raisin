namespace Raisin.AbsBuff
{
    public interface ISlice<TDat>: IDisposable
    {
        public uint Length { get; }
    }

    public interface ISliceRef<TDat>: ISlice<TDat>
    {
        public ReadOnlySpan<TDat> AsReadOnlySpan();
    }

    public interface ISliceRef<TDat, TSub>: ISliceRef<TDat>
        where TSub: struct, ISliceRef<TDat, TSub>
    {
        public TSub SubSliceRef(uint offset, uint length);
    }

    public interface ISliceMut<TDat>: ISlice<TDat>
    {
        public Span<TDat> AsSpan();
    }

    public interface ISliceMut<TDat, TSub>: ISliceMut<TDat>
        where TSub: struct, ISliceMut<TDat, TSub>
    {
        public TSub SubSliceMut(uint offset, uint length);
    }

    public readonly struct EmptyBuff<T>: ISliceRef<T, EmptyBuff<T>>, ISliceMut<T, EmptyBuff<T>>
    {
        public uint Length
            => 0;

        public ref readonly T RefAt(uint index)
            => throw new IndexOutOfRangeException();

        public ref T MutAt(uint index)
            => throw new IndexOutOfRangeException();

        public ReadOnlySpan<T> AsReadOnlySpan()
            => new ReadOnlySpan<T>(Array.Empty<T>());

        public Span<T> AsSpan()
            => new Span<T>(Array.Empty<T>());

        public EmptyBuff<T> SubSliceRef(uint offset, uint length)
            => EmptyBuff<T>.SubSlice_(in this, offset, length);

        public EmptyBuff<T> SubSliceMut(uint offset, uint length)
            => EmptyBuff<T>.SubSlice_(in this, offset, length);

        public void Dispose()
        { }

        private static EmptyBuff<T> SubSlice_(in EmptyBuff<T> arr, uint offset, uint length)
        {
            if (offset == 0 && length == 0)
                return arr;
            else
                throw new IndexOutOfRangeException();
        }
    }
}
