using System.Net;

namespace WireGuardManager.Domain.Helpers;

public static class NetworkHelper
{
    public static bool IsInReservedIpV4Range(string ipV4Address)
    {
        var ip = IPAddress.Parse(ipV4Address);
        var octets = ip.GetAddressBytes();
        return octets[0] switch
        {
            10 => true,
            172 => octets[1] >= 16 && octets[1] <= 31,
            192 => octets[1] == 168,
            _ => false
        };
    }

    public static bool IsInRange(string ipV4Address, string ipV4SubnetMask, string ipV4Gateway)
    {
        var ip = IPAddress.Parse(ipV4Address);
        var subnet = IPAddress.Parse(ipV4SubnetMask);
        var gateway = IPAddress.Parse(ipV4Gateway);
        var ipBytes = ip.GetAddressBytes();
        var subnetBytes = subnet.GetAddressBytes();
        var gatewayBytes = gateway.GetAddressBytes();
        var networkBytes = new byte[4];
        for (var i = 0; i < 4; i++)
        {
            networkBytes[i] = (byte) (ipBytes[i] & subnetBytes[i]);
        }

        var network = new IPAddress(networkBytes);
        return network.Equals(gateway);
    }


    public static bool IsInRange(string ipV4Address, string ipV4SubnetMask)
    {
        var ip = IPAddress.Parse(ipV4Address);
        var subnet = IPAddress.Parse(ipV4SubnetMask);
        var ipBytes = ip.GetAddressBytes();
        var subnetBytes = subnet.GetAddressBytes();
        var networkBytes = new byte[4];
        for (var i = 0; i < 4; i++)
        {
            networkBytes[i] = (byte) (ipBytes[i] & subnetBytes[i]);
        }

        var network = new IPAddress(networkBytes);
        return network.Equals(ip);
    }


    public static bool IsValidIpV4Address(string ipV4Address)
    {
        return IPAddress.TryParse(ipV4Address, out var _);
    }

    public static bool IsValidIpV6Address(string ipV6Address)
    {
        return IPAddress.TryParse(ipV6Address, out var _);
    }

    public static bool IsValidIpV4SubnetMask(string ipV4SubnetMask)
    {
        return IPAddress.TryParse(ipV4SubnetMask, out var _);
    }

    public static bool IsValidIpV6SubnetMask(string ipV6SubnetMask)
    {
        return IPAddress.TryParse(ipV6SubnetMask, out var _);
    }

    public static bool IsValidIpV4AddressAndSubnetMask(string ipV4Address, string ipV4SubnetMask)
    {
        return IPAddress.TryParse(ipV4Address, out var _) && IPAddress.TryParse(ipV4SubnetMask, out var _);
    }

    public static bool IsValidIpV6AddressAndSubnetMask(string ipV6Address, string ipV6SubnetMask)
    {
        return IPAddress.TryParse(ipV6Address, out var _) && IPAddress.TryParse(ipV6SubnetMask, out var _);
    }

    public static bool IsValidIpV4AddressAndSubnetMaskAndGateway(string ipV4Address, string ipV4SubnetMask,
        string ipV4Gateway)
    {
        return IPAddress.TryParse(ipV4Address, out var _) && IPAddress.TryParse(ipV4SubnetMask, out var _) &&
               IPAddress.TryParse(ipV4Gateway, out var _);
    }


    public static bool IsValidIpV6AddressAndSubnetMaskAndGateway(string ipV6Address, string ipV6SubnetMask,
        string ipV6Gateway)
    {
        return IPAddress.TryParse(ipV6Address, out var _) && IPAddress.TryParse(ipV6SubnetMask, out var _) &&
               IPAddress.TryParse(ipV6Gateway, out var _);
    }
}