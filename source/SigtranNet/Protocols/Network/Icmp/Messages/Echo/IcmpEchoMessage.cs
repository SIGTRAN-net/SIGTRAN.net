﻿/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Network.Icmp.Messages.Echo;

/// <summary>
/// An Echo message in the Internet Control Message Protocol (ICMP).
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
/// 
///         identify a session, and the sequence number might be incremented
///         on each echo request sent.vThe echoer returns these same values
///         in the echo reply.
/// 
///         Code 0 may be received from a gateway or a host.
///     </code>
/// </remarks>
internal readonly partial struct IcmpEchoMessage : IIcmpMessage<IcmpEchoMessage>
{
    /// <summary>
    /// Indicates whether the message is an Echo or an Echo Reply.
    /// </summary>
    internal readonly bool isReply;

    /// <summary>
    /// The code. (always 0)
    /// </summary>
    internal readonly byte code;

    /// <summary>
    /// The identifier.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If code = 0, an identifier to aid in matching echos and replies,
    ///         may be zero.
    ///     </code>
    /// </remarks>
    internal readonly ushort identifier;

    /// <summary>
    /// The sequence number.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If code = 0, a sequence number to aid in matching echos and
    ///         replies, may be zero.
    ///     </code>
    /// </remarks>
    internal readonly ushort sequenceNumber;

    /// <summary>
    /// The data that must be returned in the Echo Reply message.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         The data received in the echo message must be returned in the echo
    ///         reply message.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> data;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpEchoMessage" />.
    /// </summary>
    /// <param name="isReply">Indicates whether this is an Echo Reply message.</param>
    /// <param name="identifier">The identifier of the Echo.</param>
    /// <param name="sequenceNumber">The sequence number of the Echo.</param>
    /// <param name="data">The data that must be returned in the Echo Reply message.</param>
    internal IcmpEchoMessage(
        bool isReply,
        ushort identifier,
        ushort sequenceNumber,
        ReadOnlyMemory<byte> data)
    {
        this.isReply = isReply;
        this.code = 0;
        this.identifier = identifier;
        this.sequenceNumber = sequenceNumber;
        this.data = data;
    }
}
