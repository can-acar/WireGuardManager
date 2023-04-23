using System;

namespace WireGuardManager.Domain.Entities;

public class Interface
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
    public string ListenPort { get; set; }
    public string Ip { get; set; }
    public string Dns { get; set; }
    public string Subnet { get; set; }
    public string Gateway { get; set; }
    public string EndPoint { get; set; }
    public int IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}