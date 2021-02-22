using System.Collections.Generic;

namespace NFTAnbarAPI.Services
{
    public class GridData<T>
    {
        public List<T> Data { get; set; }
        public List<Filter> Filters { get; set; }
        public string SortBy { get; set; } = "id";
        public SortType SortType { get; set; } = SortType.Asc;
        public int PageSize { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
        public int Count { get; set; }

    }
}

public class Filter
{
    public string Key { get; set; }
    public string Value { get; set; }
}

public enum SortType {
    Asc = 0,
    Desc,
}