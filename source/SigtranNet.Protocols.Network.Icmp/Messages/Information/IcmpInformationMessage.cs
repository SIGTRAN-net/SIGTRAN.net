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

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Information;

/// <summary>
/// An Information Message in the Internet Control Message Protocol (ICMP).
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
internal readonly partial struct IcmpInformationMessage : IIcmpMessage<IcmpInformationMessage>
{
    /// <summary>
    /// Indicates whether the message is an Information Reply Message.
    /// </summary>
    internal readonly bool isReply;

    /// <summary>
    /// The Information Message code.
    /// </summary>
    internal readonly byte code;

    /// <summary>
    /// The identifier of the Information Request.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If code = 0, an identifier to aid in matching request and replies,
    ///         may be zero.
    ///     </code>
    /// </remarks>
    internal readonly ushort identifier;

    /// <summary>
    /// The sequence number of the Information Request.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If code = 0, a sequence number to aid in matching request and
    ///         replies, may be zero.
    ///     </code>
    /// </remarks>
    internal readonly ushort sequenceNumber;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpInformationMessage" />.
    /// </summary>
    /// <param name="isReply">Indicates whether the message is a Information Reply Message.</param>
    /// <param name="identifier">The identifier of the Information Request Message.</param>
    /// <param name="sequenceNumber">The sequence number of the Information Request Message.</param>
    internal IcmpInformationMessage(
        bool isReply,
        ushort identifier,
        ushort sequenceNumber)
    {
        this.isReply = isReply;
        this.code = 0;
        this.identifier = identifier;
        this.sequenceNumber = sequenceNumber;
    }
}
