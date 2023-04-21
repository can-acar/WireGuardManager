namespace WireGuardManager.Domain.Requests;

public class AddInterfaceRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string ListenPort { get; set; }
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
    public string Dns { get; set; }
    public string Mtu { get; set; }
    public string Table { get; set; }
    public string PreUp { get; set; }
    public string PostUp { get; set; }
    public string PreDown { get; set; }
    public string PostDown { get; set; }
    public string SaveConfig { get; set; }
}