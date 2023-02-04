/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP.IPv4.Options;
using System.Net;

namespace SigtranNet.Protocols.Network.IP.IPv4;

/// <summary>
/// An Internet Protocol (IP) version 4 datagram.
/// </summary>
internal readonly partial struct IPv4Datagram
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
        IPProtocol protocol,
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
        IPProtocol protocol,
        IPAddress sourceAddress,
        IPAddress destinationAddress,
        ReadOnlyMemory<IIPv4Option> options,
        ushort payloadLength)
    {
        byte internetHeaderLength = 5; // standard length, no options
        ushort totalLength = (ushort)(internetHeaderLength * sizeof(uint) + payloadLength);
        var sourceAddressBytes = sourceAddress.MapToIPv4().GetAddressBytes().AsSpan();
        var destinationAddressBytes = destinationAddress.MapToIPv4().GetAddressBytes().AsSpan();
        ushort headerChecksum =
            OnesComplementChecksum16Bit.Generate(
                new ReadOnlyMemory<byte>(new byte[]
                {
                    (byte)((byte)IPVersion.IPv4 << 4 | internetHeaderLength),
                    (byte)typeOfService,
                    (byte)(totalLength >> 8), (byte)(totalLength & 0xFF),
                    (byte)(identification >> 8), (byte)(identification & 0xFF),
                    (byte)((byte)flags << 5 | fragmentOffset >> 13), (byte)((byte)fragmentOffset >> 8),
                    timeToLive,
                    (byte)protocol,
                    sourceAddressBytes[0], sourceAddressBytes[1], sourceAddressBytes[2], sourceAddressBytes[3],
                    destinationAddressBytes[0], destinationAddressBytes[1], destinationAddressBytes[2], destinationAddressBytes[3]
                }));

        return new(
            typeOfService,
            totalLength,
            identification,
            flags,
            fragmentOffset,
            timeToLive,
            protocol,
            headerChecksum,
            sourceAddress,
            destinationAddress,
            options);
    }
}
