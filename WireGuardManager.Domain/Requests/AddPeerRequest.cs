using System.Collections.Generic;
using WireGuardManager.Domain.Entities;

namespace WireGuardManager.Domain.Requests;

public class AddPeerRequest
{
    public Interface Interface { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public string Endpoint { get; set; } = string.Empty;
    public bool Nat { get; set; }
    public bool PersistentKeepalive { get; set; }
    public string Description { get; set; } = string.Empty;

    public string IpV4Address { get; set; } = string.Empty;

    public string IpV6Address { get; set; } = string.Empty;

    public string IpV4SubnetMask { get; set; } = string.Empty;

    public string IpV6SubnetMask { get; set; } = string.Empty;

    public bool AccessOtherSubnetsPeer { get; set; } = true;

    public bool AccessOtherSubnetsServer { get; set; } = true;

    public IList<string> AllowedIpV4Addresses { get; set; } = new List<string>();

    public IList<string> AllowedIpV6Addresses { get; set; } = new List<string>();

    public IList<string> AllowedIpV4Subnets { get; set; } = new List<string>();
}