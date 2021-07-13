namespace MinyToDo.Models.Enums
{
    public enum ApiResponseType : int
    {
        Created = 1,
        NoContent,
        Ok,
        NotFound,
        Unauthorized,
        Forbidden,
    }
}
