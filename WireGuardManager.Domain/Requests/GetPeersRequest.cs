namespace WireGuardManager.Domain.Requests;

public class GetPeersRequest
{
    public string InterFaceId { get; set; }
    public string PeerId { get; set; }
}