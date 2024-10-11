namespace C.Common.GlobalResponses.Generics;

public class TypedResponseModel<T> : ResponseModel
{
    public T? Data { get; set; }

    public TypedResponseModel(List<string> messages) : base(messages)
    {

    }

    public TypedResponseModel()
    {

    }
}
