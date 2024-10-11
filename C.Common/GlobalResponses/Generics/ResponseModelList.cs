namespace C.Common.GlobalResponses.Generics
{
    public class ResponseModelList<T> : TypedResponseModel<T>
    {

        int _count = 0;
        List<T> _data = new();
        public int TotalCount { get { return _count; } }


        public List<T> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                _count = _data.Count;
            }
        }


        public ResponseModelList(List<string> errors) : base(errors)
        {

        }

        public ResponseModelList()
        {

        }
    }
}
