using System.Data.Common;

namespace C.Common.GlobalResponses.Generics
{
    public class Pagination<T>
    {

        public List<T> Data { get; set; }
        public int TotalDataCount { get; set; }

        public bool IsSuccess { get; set; }

        public Pagination(List<T> datas, int totalDataCount, bool isSuccess)
        {
            Data = datas;
            totalDataCount = totalDataCount;
            IsSuccess = isSuccess;

        }

        public Pagination()
        {
            Data = new List<T>();
            TotalDataCount = 0;
            IsSuccess = true;
        }
    }
}
