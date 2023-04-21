namespace WireGuardManager.Domain.Entities;

public class Peer
{
    public int Id { get; set; }
    public string PublicKey { get; set; }
    public string AllowedIPs { get; set; }
    public string Endpoint { get; set; }
    public int TransferRX { get; set; }
    public int TransferTX { get; set; }
}