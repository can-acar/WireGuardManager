namespace WireGuardManager.Domain.Requests;

public class GetAllInterfacesRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string SortBy { get; set; }
    public string SortOrder { get; set; }

    public string Search { get; set; }
}