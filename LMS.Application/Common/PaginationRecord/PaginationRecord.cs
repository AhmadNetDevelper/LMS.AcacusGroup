namespace LMS.Application.Common.PaginationRecord
{
    public class PaginationRecord<T>
    {
        public IEnumerable<T> DataRecord { get; set; }
        public int CountRecord { get; set; }
    }
}
