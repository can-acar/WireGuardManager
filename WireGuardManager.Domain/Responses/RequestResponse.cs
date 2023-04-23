namespace WireGuardManager.Domain.Responses;

public class RequestResponse
{
    public bool IsSuccess { get; set; }
    public object Data { get; set; }
    public string? Message { get; set; }
}

public class PaginationResponse : RequestResponse
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public object? Query { get; set; }
}