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

using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.ParameterProblem;

/// <summary>
/// A Parameter Problem Message in the Internet Control Message Protocol (ICMP).
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
///         
///         Code 0 may be received from a gateway or a host.
///     </code>
/// </remarks>
internal readonly partial struct IcmpParameterProblemMessage : IIcmpMessage<IcmpParameterProblemMessage>
{
    /// <summary>
    /// Pointer that points to the error.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         If code = 0, identifies the octet where an error was detected.
    ///     </code>
    /// </remarks>
    internal readonly byte pointer;

    /// <summary>
    /// The Internet Header of the original datagram.
    /// </summary>
    internal readonly IPv4Header ipHeaderOriginal;

    /// <summary>
    /// The first 64 bits of the original datagram's payload.
    /// </summary>
    /// <remarks>
    ///     From <a href="https://datatracker.ietf.org/doc/rfc792/">RFC 792</a>:
    ///     <code>
    ///         This data is used by the host to match the
    ///         message to the appropriate process. If a higher level protocol
    ///         uses port numbers, they are assumed to be in the first 64 data
    ///         bits of the original datagram's data.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<byte> originalDataDatagramSample;

    /// <summary>
    /// Initializes a new instance of <see cref="IcmpParameterProblemMessage" />.
    /// </summary>
    /// <param name="pointer">Identifies the octet where an error was detected.</param>
    /// <param name="ipHeaderOriginal">The Internet Header of the original datagram that contains a Parameter Problem.</param>
    /// <param name="originalDataDatagramSample">The first 64 bits of the original datagram's payload.</param>
    internal IcmpParameterProblemMessage(
        byte pointer,
        IPv4Header ipHeaderOriginal,
        ReadOnlyMemory<byte> originalDataDatagramSample)
    {
        this.pointer = pointer;
        this.ipHeaderOriginal = ipHeaderOriginal;
        this.originalDataDatagramSample = originalDataDatagramSample;
    }
}
