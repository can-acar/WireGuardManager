using System;

namespace WireGuardManager.Domain.Entities;

public class Peer
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int InterfaceId { get; set; }
    public string Name { get; set; }
    public string PrivateKey { get; set; }
    public string PublicKey { get; set; }
    public string AllowedIPs { get; set; }
    public string Endpoint { get; set; }
    public string PersistentKeepalive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int IsActive { get; set; }
    public int IsDeleted { get; set; }

}