namespace MinyToDo.Models.Enums
{
    public enum ApiResponseType : int
    {
        Ok = 1,
        Created,
        NoContent,
        BadRequest,
        NotFound,
        Unauthorized,
        Forbidden,
    }
}
