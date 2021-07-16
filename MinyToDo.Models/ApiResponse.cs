using MinyToDo.Models.Enums;

namespace MinyToDo.Models
{
    public class ApiResponse
    {
        public ApiResponse(ApiResponseType _type)
        {
            Type = _type;
        }

        public ApiResponse(ApiResponseType _type, object _data)
        {
            Data = _data;
            Type = _type;
        }
        
        public ApiResponse(ApiResponseType _type, string _message)
        {
            Message = _message;
            Type = _type;
        }

        public object Data { get; set; }
        public string Message { get; set; }
        public ApiResponseType Type { get; set; }
    }
}