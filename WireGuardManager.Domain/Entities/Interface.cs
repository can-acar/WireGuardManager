namespace WireGuardManager.Domain.Entities;

public class Interface
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    public string ListenPort { get; set; }
}