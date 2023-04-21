namespace WireGuardManager.Domain.Requests;

public class GetAllowedIpsRequest
{
    public string InterFaceId { get; set; }
    public string PeerId { get; set; }
    public string AllowedIpId { get; set; }
}