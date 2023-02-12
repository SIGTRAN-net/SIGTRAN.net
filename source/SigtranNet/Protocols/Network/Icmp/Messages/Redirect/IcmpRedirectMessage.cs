/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP.IPv4;
using System.Net;

namespace SigtranNet.Protocols.Network.Icmp.Messages.Redirect;

/// <summary>
/// A Redirect Message in the Internet Control Message Protocol (ICMP).
/// </summary>
/// <remarks>
///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
///     <code>
///         The gateway sends a redirect message to a host in the following
///         situation.A gateway, G1, receives an internet datagram from a
///         host on a network to which the gateway is attached.The gateway,
///         G1, checks its routing table and obtains the address of the next
///         gateway, G2, on the route to the datagram's internet destination
///         network, X.If G2 and the host identified by the internet source
///         address of the datagram are on the same network, a redirect
///         message is sent to the host.The redirect message advises the
///         host to send its traffic for network X directly to gateway G2 as
///         this is a shorter path to the destination.  The gateway forwards
///         the original datagram's data to its internet destination.
/// 
///         For datagrams with the IP source route options and the gateway
///         address in the destination address field, a redirect message is
///         not sent even if there is a better route to the ultimate
///         destination than the next address in the source route.
///         
///         Codes 0, 1, 2, and 3 may be received from a gateway.
///     </code>
/// </remarks>
internal readonly partial struct IcmpRedirectMessage
    : IIcmpMessage<IcmpRedirectMessage>
{
    /// <summary>
    /// The Redirect code.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         0 = Redirect datagrams for the Network.
    /// 
    ///         1 = Redirect datagrams for the Host.
    /// 
    ///         2 = Redirect datagrams for the Type of Service and Network.
    /// 
    ///         3 = Redirect datagrams for the Type of Service and Host.
    ///     </code>
    /// </remarks>
    internal readonly IcmpRedirectCode code;

    /// <summary>
    /// The Gateway Internet Address,
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         Address of the gateway to which traffic for the network specified
    ///         in the internet destination network field of the original
    ///         datagram's data should be sent.
    ///     </code>
    /// </remarks>
    internal readonly IPAddress gatewayInternetAddress;

    /// <summary>
    /// The Internet Header of the original datagram.
    /// </summary>
    internal readonly IPv4Header ipHeaderOriginal;

    /// <summary>
    /// The first 64 bits of the original datagram's payload.
    /// </summary>
    internal readonly ReadOnlyMemory<byte> originalDataDatagramSample;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpRedirectMessage" />.
    /// </summary>
    /// <param name="code">The Redirect code.</param>
    /// <param name="gatewayInternetAddress">The Gateway Internet Address.</param>
    /// <param name="ipHeaderOriginal">The Internet Header of the original datagram.</param>
    /// <param name="originalDataDatagramSample">The first 64 bits of the original datagram's payload.</param>
    internal IcmpRedirectMessage(
        IcmpRedirectCode code,
        IPAddress gatewayInternetAddress,
        IPv4Header ipHeaderOriginal,
        ReadOnlyMemory<byte> originalDataDatagramSample)
    {
        this.code = code;
        this.gatewayInternetAddress = gatewayInternetAddress;
        this.ipHeaderOriginal = ipHeaderOriginal;
        this.originalDataDatagramSample = originalDataDatagramSample;
    }
}
