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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.StreamIdentifier;

/// <summary>
/// An IPv4 Stream Identifier.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         This option provides a way for the 16-bit SATNET stream
///         identifier to be carried through networks that do not support
///         the stream concept.
///
///         Must be copied on fragmentation. Appears at most once in a
///         datagram.
///     </code>
/// </remarks>
internal readonly partial struct IPv4OptionStreamIdentifier : IIPv4Option
{
    internal const byte LengthFixed = 4;
    internal readonly IPv4OptionType optionType;
    internal readonly ushort streamIdentifier;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionStreamIdentifier" />.
    /// </summary>
    /// <param name="optionType">The IPv4 Option Type.</param>
    /// <param name="streamIdentifier">The stream identifier.</param>
    internal IPv4OptionStreamIdentifier(
        IPv4OptionType optionType,
        ushort streamIdentifier)
    {
        this.optionType = optionType;
        this.streamIdentifier = streamIdentifier;
    }

    byte IIPv4Option.Length => LengthFixed;
}
