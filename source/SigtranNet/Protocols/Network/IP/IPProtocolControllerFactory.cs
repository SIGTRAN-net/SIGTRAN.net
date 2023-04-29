/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.Icmp.Controllers;
using SigtranNet.Protocols.Network.IP.Exceptions;
using SigtranNet.Protocols.Network.IP.IPv4.Controllers;
using System.Runtime.InteropServices;

namespace SigtranNet.Protocols.Network.IP;

/// <summary>
/// A factory that creates controllers that handle Internet Protocol (IP) datagrams of a particular Network or Transport protocol.
/// </summary>
internal sealed class IPProtocolControllerFactory
{
    private delegate IIPProtocolController Constructor(IPProtocolControllerFactory factory);
    private static readonly Dictionary<IPProtocol, Constructor> Constructors =
        new()
        {
            { IPProtocol.ICMP, CreateIcmpController }
        };
    private readonly IPv4Controller ipController;

    /// <summary>
    /// Initializes a new instance of <see cref="IPProtocolControllerFactory" />.
    /// </summary>
    /// <param name="ipController">
    /// The Internet Protocol (IP) controller.
    /// </param>
    internal IPProtocolControllerFactory(IPv4Controller ipController)
    {
        this.ipController = ipController;
    }

    /// <summary>
    /// Creates a controller for the specified <paramref name="protocol" />.
    /// </summary>
    /// <param name="protocol">The protocol that is carried by the Internet Protocol (IP).</param>
    /// <returns>The controller for the specified <paramref name="protocol" />.</returns>
    /// <exception cref="IPProtocolNotSupportedException">
    /// An <see cref="IPProtocolNotSupportedException" /> is thrown if the requested protocol is not supported.
    /// </exception>
    internal IIPProtocolController Create(IPProtocol protocol)
    {
        if (protocol == IPProtocol.IPv4)
            return this.ipController;
        ref var constructor = ref CollectionsMarshal.GetValueRefOrNullRef(Constructors, protocol);
        if (constructor == null)
            throw new IPProtocolNotSupportedException(protocol);
        return constructor(this);
    }

    private static IcmpController CreateIcmpController(IPProtocolControllerFactory factory)
    {
        return new(factory.ipController);
    }
}
