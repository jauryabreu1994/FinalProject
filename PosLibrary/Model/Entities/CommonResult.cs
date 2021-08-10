namespace PosLibrary.Model.Entities
{
    public class CommonResult
    {
        public bool result { get; set; } = false;
        public string message { get; set; } = string.Empty;
        public object response { get; set; } = null;

        public CommonResult(bool result, string message, object response) 
        {
            this.result = result;
            this.message = message;
            this.response = response;
        }
    }
}
