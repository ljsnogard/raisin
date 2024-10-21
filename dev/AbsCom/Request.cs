namespace Raisin.AbsCom
{
    using System.Collections.Generic;
    using System.Net.Mime;

    public interface IRequestRead
    {
        public Uri Command { get; }
        public IAsyncEnumerable<SegmentRead> Parameters { get; }
    }


    public interface IResponse
    {
        public uint Status { get; }

        public ContentType ContentType { get; }

        public Stream ContentStream { get; }
    }
}
