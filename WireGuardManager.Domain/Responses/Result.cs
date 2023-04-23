namespace WireGuardManager.Domain.Responses;

internal class Result<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
}