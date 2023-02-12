/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Network.IP.IPv4;

namespace SigtranNet.Protocols.Network.Icmp.Messages.TimeExceeded;

/// <summary>
/// A Time Exceeded Message in the Internet Control Message Protocol (ICMP).
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
///
///         Code 0 may be received from a gateway.  Code 1 may be received
///         from a host.
///     </code>
/// </remarks>
internal readonly partial struct IcmpTimeExceededMessage : IIcmpMessage<IcmpTimeExceededMessage>
{
    /// <summary>
    /// The code that identifies the reason for an exceeded time.
    /// </summary>
    internal readonly IcmpTimeExceededCode code;

    /// <summary>
    /// The original Internet Protocol header from the datagram that caused the message.
    /// </summary>
    internal readonly IPv4Header ipHeaderOriginal;

    /// <summary>
    /// The first 64 bits of the original datagram that caused the message.
    /// </summary>
    internal readonly ReadOnlyMemory<byte> originalDataDatagramSample;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpTimeExceededMessage" />.
    /// </summary>
    /// <param name="code">The code that indicates the reason for the exceeded time.</param>
    /// <param name="ipHeaderOriginal">The Internet Header that was received in the original datagram that caused the message.</param>
    /// <param name="originalDataDatagramSample">The first 64 bits of the original datagram's payload.</param>
    internal IcmpTimeExceededMessage(
        IcmpTimeExceededCode code,
        IPv4Header ipHeaderOriginal,
        ReadOnlyMemory<byte> originalDataDatagramSample)
    {
        this.code = code;
        this.ipHeaderOriginal = ipHeaderOriginal;
        this.originalDataDatagramSample = originalDataDatagramSample;
    }
}
