namespace WireGuardManager.Domain.Requests;

public class UpdateInterfaceRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
    public string ListenPort { get; set; }
    public string Address { get; set; }
    public string Endpoint { get; set; }
    public string Dns { get; set; }
    public string MTU { get; set; }
    public string PreSharedKey { get; set; }
    public string[] AllowedIPs { get; set; }
}