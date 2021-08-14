using MinyToDo.Models.Enums;

namespace MinyToDo.Models
{
    public class ApiResponse
    {
        public ApiResponse(ApiResponseStatus status)
        {
            StatusCode = (int)status;
        }

        public ApiResponse(ApiResponseStatus status, object _data)
        {
            Data = _data;
            StatusCode = (int)status;
        }
        
        public ApiResponse(ApiResponseStatus status, string _message)
        {
            Message = _message;
            StatusCode = (int)status;
        }

        public object Data { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}