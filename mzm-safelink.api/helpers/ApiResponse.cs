using System.Text.Json.Serialization;
using FluentValidation.Results;

namespace mzm_safelink.api.helpers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? Data { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<Errors>? Errors { get; set; }

    public ApiResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public ApiResponse(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public ApiResponse(bool success, string message, List<Errors> errors)
    {
        Success = success;
        Message = message;
        Errors = errors;
    }

    public ApiResponse(bool success, string message, List<ValidationFailure> errors)
    {
        Success = success;
        Message = message;
        Errors = [.. errors.Select(e => new Errors("INVALID_DATA", $"{e.PropertyName}: {e.ErrorMessage}"))];
    }
}

public class Errors
{
    public string Code { get; set; }
    public string Message { get; set; }

    public Errors(string code, string message)
    {
        Code = code;
        Message = message;
    }
}