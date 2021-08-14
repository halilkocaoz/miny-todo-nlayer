namespace MinyToDo.Models.Enums
{
    public enum ApiResponseStatus : int
    {
        Ok = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
    }
}
