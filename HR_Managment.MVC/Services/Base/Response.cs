namespace HR_Managment.MVC.Services.Base
{
    public class Response<T>
    {
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; } = new List<string>();
        public bool IsSuccess { get; set; }
        public T Data { get; set; }

    }
}
