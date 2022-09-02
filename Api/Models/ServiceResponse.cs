namespace Api.Models;

public class ServiceResponse<T>
{
    public bool Success { get; set; } = true;
    public string Error { get; set; } = string.Empty;
    public T? Data { get; set; }
}