namespace Ecom.API.Helper
{
    public class Pagination <T> where T : class
    {
        public Pagination(int totalCount , int pageNumber, int pageSize, IEnumerable<T> data)
        {
            PageNumber = pageNumber;
            TotalCount = totalCount;
            PageSize = pageSize;
            Data = data;
        }

        public int PageNumber { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
