/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.IP.IPv4.Options;
using System.Buffers.Binary;
using System.Net;

namespace SigtranNet.Protocols.IP.IPv4;

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

    private static ushort GetUInt16(
        byte internetHeaderLength,
        IPv4TypeOfService typeOfService) =>
        (ushort)((ushort)(((ushort)IPVersion.IPv4 << 4) + internetHeaderLength) << 8 + (byte)typeOfService);

    private static ushort GetUInt16(
        IPv4Flags flags,
        ushort fragmentOffset) =>
        (ushort)(((ushort)flags) << 13 + fragmentOffset);

    private static ushort GetUInt16(
        byte timeToLive,
        IPProtocol protocol) =>
        (ushort)(timeToLive << 8 + (ushort)protocol);

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
        byte internetHeaderLength = sizeof(uint) * 5; // standard length, no options
        ushort totalLength = (ushort)(internetHeaderLength + payloadLength);
        var sourceAddressBytes = sourceAddress.MapToIPv4().GetAddressBytes().AsSpan();
        var destinationAddressBytes = destinationAddress.MapToIPv4().GetAddressBytes().AsSpan();
        ushort headerChecksum =
            OnesComplementChecksum16Bit.Calculate(
                new ReadOnlyMemory<ushort>(new ushort[]
                {
                    GetUInt16(internetHeaderLength, typeOfService),
                    totalLength,
                    identification,
                    GetUInt16(flags, fragmentOffset),
                    GetUInt16(timeToLive, protocol),
                    BinaryPrimitives.ReadUInt16BigEndian(sourceAddressBytes[0..2]),
                    BinaryPrimitives.ReadUInt16BigEndian(sourceAddressBytes[2..4]),
                    BinaryPrimitives.ReadUInt16BigEndian(destinationAddressBytes[0..2]),
                    BinaryPrimitives.ReadUInt16BigEndian(destinationAddressBytes[2..4])
                }));

        return new(
            internetHeaderLength,
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
