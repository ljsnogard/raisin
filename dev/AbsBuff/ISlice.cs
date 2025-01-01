namespace Raisin.AbsBuff
{
    public interface ISlice<TDat>: IDisposable
    {
        public uint Length { get; }
    }

    public interface ISliceRef<TDat>: ISlice<TDat>
    { }

    public interface ISliceRef<TDat, out TSub>: ISliceRef<TDat>
        where TSub: struct, ISliceRef<TDat, TSub>
    {
        public TSub SubSliceRef(uint offset, uint length);
    }

    public interface ISliceMut<TDat>: ISlice<TDat>
    { }

    public interface ISliceMut<TDat, out TSub>: ISliceMut<TDat>
        where TSub: struct, ISliceMut<TDat, TSub>
    {
        public TSub SubSliceMut(uint offset, uint length);
    }

    public readonly struct EmptyArray<T>: ISliceRef<T, EmptyArray<T>>, ISliceMut<T, EmptyArray<T>>
    {
        public static readonly EmptyArray<T> Instance = new EmptyArray<T>();

        public uint Length
            => 0;

        public EmptyArray<T> SubSliceRef(uint offset, uint length)
            => throw new IndexOutOfRangeException();

        public EmptyArray<T> SubSliceMut(uint offset, uint length)
            => throw new IndexOutOfRangeException();

        public void Dispose() {}
    }
}
