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

using SigtranNet.Protocols.Network.Datagrams;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header;
using SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams;

/// <summary>
/// An Internet Protocol (IP) version 4 datagram.
/// </summary>
internal readonly partial struct IPv4Datagram : INetworkDatagram<IPv4Datagram>
{
    internal readonly IPv4Header header;
    internal readonly ReadOnlyMemory<byte> payload;

    private IPv4Datagram(IPv4Header header, ReadOnlyMemory<byte> payload)
    {
        this.header = header;
        this.payload = payload;
    }

    internal IPv4Datagram(
        IPv4TypeOfService typeOfService,
        ushort identification,
        IPv4Flags flags,
        ushort fragmentOffset,
        byte timeToLive,
        InternetProtocol protocol,
        IPAddress sourceAddress,
        IPAddress destinationAddress,
        ReadOnlyMemory<IIPv4Option> options,
        ReadOnlyMemory<byte> payload)
        : this(
              CreateHeader(
                  typeOfService,
                  identification,
                  flags,
                  fragmentOffset,
                  timeToLive,
                  protocol,
                  sourceAddress,
                  destinationAddress,
                  options,
                  (ushort)payload.Length),
              payload)
    {
    }

    private static IPv4Header CreateHeader(
        IPv4TypeOfService typeOfService,
        ushort identification,
        IPv4Flags flags,
        ushort fragmentOffset,
        byte timeToLive,
        InternetProtocol protocol,
        IPAddress sourceAddress,
        IPAddress destinationAddress,
        ReadOnlyMemory<IIPv4Option> options,
        ushort payloadLength)
    {
        byte internetHeaderLength = 5; // standard length, no options
        var headerLength = internetHeaderLength * sizeof(uint);
        var optionsSpan = options.Span;
        for (var i = 0; i < options.Length; i++)
        {
            headerLength += optionsSpan[i].Length;
        }
        internetHeaderLength = (byte)Math.Ceiling((double)headerLength / sizeof(uint));
        ushort totalLength = (ushort)(internetHeaderLength * sizeof(uint) + payloadLength);

        return new(
            typeOfService,
            totalLength,
            identification,
            flags,
            fragmentOffset,
            timeToLive,
            protocol,
            sourceAddress,
            destinationAddress,
            options);
    }
}
