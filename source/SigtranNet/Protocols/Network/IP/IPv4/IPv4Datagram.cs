/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;
using SigtranNet.Protocols.Network.IP.IPv4.Options;
using System.ComponentModel.DataAnnotations;
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
