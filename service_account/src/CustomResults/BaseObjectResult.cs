using System;
namespace service_account.CustomResults
{
    public class BaseObjectResult
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }
        public object Result { get; set; }

        public List<string> Errors { get; set; }

    }
}
