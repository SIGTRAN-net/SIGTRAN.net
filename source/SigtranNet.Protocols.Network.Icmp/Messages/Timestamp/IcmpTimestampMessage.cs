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

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Timestamp;

/// <summary>
/// A Timestamp Message in the Internet Message Control Protocol (ICMP).
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
internal readonly partial struct IcmpTimestampMessage : IIcmpMessage<IcmpTimestampMessage>
{
    /// <summary>
    /// Indicates whether the message is a Timestamp Reply.
    /// </summary>
    internal readonly bool isReply;

    /// <summary>
    /// The Timestamp code. (always 0)
    /// </summary>
    internal readonly byte code;
    
    /// <summary>
    /// The identifier of the Timestamp Message.
    /// </summary>
    internal readonly ushort identifier;

    /// <summary>
    /// The sequence number of the Timestamp Message.
    /// </summary>
    internal readonly ushort sequenceNumber;

    /// <summary>
    /// The originate Timestamp.
    /// </summary>
    internal readonly uint originateTimestamp;

    /// <summary>
    /// The receive Timestamp.
    /// </summary>
    internal readonly uint receiveTimestamp;

    /// <summary>
    /// The transmit Timestamp.
    /// </summary>
    internal readonly uint transmitTimestamp;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpTimestampMessage" />.
    /// </summary>
    /// <param name="isReply">Indicates whether the message is a Timestamp Reply.</param>
    /// <param name="identifier">The identifier of the Timestamp Message.</param>
    /// <param name="sequenceNumber">The sequence number of the Timestamp Message.</param>
    /// <param name="originateTimestamp">The originate Timestamp.</param>
    /// <param name="receiveTimestamp">The receive Timestamp.</param>
    /// <param name="transmitTimestamp">The transmit Timestamp.</param>
    internal IcmpTimestampMessage(
        bool isReply,
        ushort identifier,
        ushort sequenceNumber,
        uint originateTimestamp,
        uint receiveTimestamp,
        uint transmitTimestamp)
    {
        this.isReply = isReply;
        this.identifier = identifier;
        this.sequenceNumber = sequenceNumber;
        this.originateTimestamp = originateTimestamp;
        this.receiveTimestamp = receiveTimestamp;
        this.transmitTimestamp = transmitTimestamp;
    }
}
