namespace Horus.Models
{
    public class ModelResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsOk { get; set; }
    }
}
