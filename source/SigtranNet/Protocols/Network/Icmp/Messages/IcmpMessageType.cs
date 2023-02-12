/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.Icmp.Messages;

/// <summary>
/// The type of the Internet Control Message Protocol (ICMP) message.
/// </summary>
internal enum IcmpMessageType : byte
{
    /// <summary>
    /// Echo Reply.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         The data received in the echo message must be returned in the echo
    ///         reply message.
    /// 
    ///         The identifier and sequence number may be used by the echo sender
    ///         to aid in matching the replies with the echo requests. For
    ///         example, the identifier might be used like a port in TCP or UDP to
    ///         identify a session, and the sequence number might be incremented
    ///         on each echo request sent.  The echoer returns these same values
    ///         in the echo reply.
    ///         
    ///         Code 0 may be received from a gateway or a host.
    ///     </code>
    /// </remarks>
    EchoReply = 0,

    /// <summary>
    /// Destination Unreachable.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If, according to the information in the gateway's routing tables,
    ///         the network specified in the internet destination field of a
    ///         datagram is unreachable, e.g., the distance to the network is
    ///         infinity, the gateway may send a destination unreachable message
    ///         to the internet source host of the datagram.In addition, in some
    ///         networks, the gateway may be able to determine if the internet
    ///         destination host is unreachable.Gateways in these networks may
    ///         send destination unreachable messages to the source host when the
    ///         destination host is unreachable.
    ///
    ///         If, in the destination host, the IP module cannot deliver the
    ///         datagram because the indicated protocol module or process port is
    ///         not active, the destination host may send a destination
    ///         unreachable message to the source host.
    ///
    ///         Another case is when a datagram must be fragmented to be forwarded
    ///         by a gateway yet the Don't Fragment flag is on.  In this case the
    ///         gateway must discard the datagram and may return a destination
    ///         unreachable message.
    ///     </code>
    /// </remarks>
    DestinationUnreachable = 3,

    /// <summary>
    /// Source Quench.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         A gateway may discard internet datagrams if it does not have the
    ///         buffer space needed to queue the datagrams for output to the next
    ///         network on the route to the destination network. If a gateway
    ///         discards a datagram, it may send a source quench message to the
    ///         internet source host of the datagram.A destination host may also
    ///         send a source quench message if datagrams arrive too fast to be
    ///         processed. The source quench message is a request to the host to
    ///         cut back the rate at which it is sending traffic to the internet
    ///         destination.  The gateway may send a source quench message for
    ///         every message that it discards.On receipt of a source quench
    ///         message, the source host should cut back the rate at which it is
    ///         sending traffic to the specified destination until it no longer
    ///         receives source quench messages from the gateway.The source host
    ///         can then gradually increase the rate at which it sends traffic to
    ///         the destination until it again receives source quench messages.
    /// 
    ///         The gateway or host may send the source quench message when it
    ///         approaches its capacity limit rather than waiting until the
    ///         capacity is exceeded.This means that the data datagram which
    ///         triggered the source quench message may be delivered.
    ///     </code>
    /// </remarks>
    SourceQuench = 4,

    /// <summary>
    /// Redirect.
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
    ///     </code>
    /// </remarks>
    Redirect = 5,

    /// <summary>
    /// Echo.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         The data received in the echo message must be returned in the echo
    ///         reply message.
    /// 
    ///         The identifier and sequence number may be used by the echo sender
    ///         to aid in matching the replies with the echo requests. For
    ///         example, the identifier might be used like a port in TCP or UDP to
    ///         identify a session, and the sequence number might be incremented
    ///         on each echo request sent.  The echoer returns these same values
    ///         in the echo reply.
    ///         
    ///         Code 0 may be received from a gateway or a host.
    ///     </code>
    /// </remarks>
    Echo = 8,

    /// <summary>
    /// Time Exceeded.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If the gateway processing a datagram finds the time to live field
    ///         is zero it must discard the datagram.  The gateway may also notify
    ///         the source host via the time exceeded message.
    ///
    ///         If a host reassembling a fragmented datagram cannot complete the
    ///         reassembly due to missing fragments within its time limit it
    ///         discards the datagram, and it may send a time exceeded message.
    ///
    ///         If fragment zero is not available then no time exceeded need be
    ///         sent at all.
    ///     </code>
    /// </remarks>
    TimeExceeded = 11,

    /// <summary>
    /// Parameter Problem.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If the gateway or host processing a datagram finds a problem with
    ///         the header parameters such that it cannot complete processing the
    ///         datagram it must discard the datagram. One potential source of
    ///         such a problem is with incorrect arguments in an option.  The
    ///         gateway or host may also notify the source host via the parameter
    ///         problem message.This message is only sent if the error caused
    ///         the datagram to be discarded.
    ///         
    ///         The pointer identifies the octet of the original datagram's header
    ///         where the error was detected (it may be in the middle of an
    ///         option).  For example, 1 indicates something is wrong with the
    ///         Type of Service, and (if there are options present) 20 indicates
    ///         something is wrong with the type code of the first option.
    ///     </code>
    /// </remarks>
    ParameterProblem = 12
}
