/*
 * This file is part of SIGTRAN.net.
 * 
 * SIGTRAN.net is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, 
 * either version 3 of the License, or (at your option) any later version.
 * 
 * SIGTRAN.net is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with SIGTRAN.net. If not, see <https://www.gnu.org/licenses/>.
 */

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages;

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
    ParameterProblem = 12,

    /// <summary>
    /// Timestamp.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         The data received (a timestamp) in the message is returned in the
    ///         reply together with an additional timestamp.The timestamp is 32
    ///         bits of milliseconds since midnight UT.One use of these
    ///         timestamps is described by Mills[5].
    ///
    ///         The Originate Timestamp is the time the sender last touched the
    ///         message before sending it, the Receive Timestamp is the time the
    ///         echoer first touched it on receipt, and the Transmit Timestamp is
    ///         the time the echoer last touched the message on sending it.
    /// 
    ///         If the time is not available in miliseconds or cannot be provided
    ///         with respect to midnight UT then any time can be inserted in a
    ///         timestamp provided the high order bit of the timestamp is also set
    ///         to indicate this non-standard value.
    /// 
    ///         The identifier and sequence number may be used by the echo sender
    ///         to aid in matching the replies with the requests. For example,
    ///         the identifier might be used like a port in TCP or UDP to identify
    ///         a session, and the sequence number might be incremented on each
    ///         request sent.The destination returns these same values in the
    ///         reply.
    /// 
    ///         Code 0 may be received from a gateway or a host.
    ///     </code>
    /// </remarks>
    Timestamp = 13,

    /// <summary>
    /// Timestamp Reply.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         The data received (a timestamp) in the message is returned in the
    ///         reply together with an additional timestamp.The timestamp is 32
    ///         bits of milliseconds since midnight UT.One use of these
    ///         timestamps is described by Mills[5].
    ///
    ///         The Originate Timestamp is the time the sender last touched the
    ///         message before sending it, the Receive Timestamp is the time the
    ///         echoer first touched it on receipt, and the Transmit Timestamp is
    ///         the time the echoer last touched the message on sending it.
    /// 
    ///         If the time is not available in miliseconds or cannot be provided
    ///         with respect to midnight UT then any time can be inserted in a
    ///         timestamp provided the high order bit of the timestamp is also set
    ///         to indicate this non-standard value.
    /// 
    ///         The identifier and sequence number may be used by the echo sender
    ///         to aid in matching the replies with the requests. For example,
    ///         the identifier might be used like a port in TCP or UDP to identify
    ///         a session, and the sequence number might be incremented on each
    ///         request sent.The destination returns these same values in the
    ///         reply.
    /// 
    ///         Code 0 may be received from a gateway or a host.
    ///     </code>
    /// </remarks>
    TimestampReply = 14,

    /// <summary>
    /// Information Request.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         This message may be sent with the source network in the IP header
    ///         source and destination address fields zero(which means "this"
    ///         network). The replying IP module should send the reply with the
    ///         addresses fully specified.This message is a way for a host to
    ///         find out the number of the network it is on.
    ///         
    ///         The identifier and sequence number may be used by the echo sender
    ///         to aid in matching the replies with the requests.For example,
    ///         the identifier might be used like a port in TCP or UDP to identify
    ///         a session, and the sequence number might be incremented on each
    ///         request sent.The destination returns these same values in the
    ///         reply.
    ///         
    ///         Code 0 may be received from a gateway or a host.
    ///     </code>
    /// </remarks>
    InformationRequest = 15,

    /// <summary>
    /// Information Reply.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         This message may be sent with the source network in the IP header
    ///         source and destination address fields zero(which means "this"
    ///         network). The replying IP module should send the reply with the
    ///         addresses fully specified.This message is a way for a host to
    ///         find out the number of the network it is on.
    ///         
    ///         The identifier and sequence number may be used by the echo sender
    ///         to aid in matching the replies with the requests.For example,
    ///         the identifier might be used like a port in TCP or UDP to identify
    ///         a session, and the sequence number might be incremented on each
    ///         request sent.The destination returns these same values in the
    ///         reply.
    ///         
    ///         Code 0 may be received from a gateway or a host.
    ///     </code>
    /// </remarks>
    InformationReply = 16,
}
