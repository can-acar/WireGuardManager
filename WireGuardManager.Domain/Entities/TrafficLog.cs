namespace WireGuardManager.Domain.Entities;

public class TrafficLog
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InterfaceId { get; set; }
    public int PeerId { get; set; }
    public string BytesSent { get; set; }
    public string BytesReceived { get; set; }
    public string ConnectedAt { get; set; }
    public string DisconnectedAt { get; set; }
    public string IpAddress { get; set; }
}