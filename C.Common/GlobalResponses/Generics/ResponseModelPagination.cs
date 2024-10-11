namespace C.Common.GlobalResponses.Generics
{
    public class ResponseModelPagination<T> : TypedResponseModel<T>
    {
        public Pagination<T> Data { get; set; }


        public ResponseModelPagination(List<string> messages) : base(messages) { }

        public ResponseModelPagination() { }

    }
}
