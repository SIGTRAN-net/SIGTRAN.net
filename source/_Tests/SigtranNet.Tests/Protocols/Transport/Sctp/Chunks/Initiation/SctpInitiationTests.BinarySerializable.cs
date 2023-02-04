/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Initiation;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;
using System.Net;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Initiation;

public partial class SctpInitiationTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.Initiation,
                    0,
                    0, 28,

                    /* Initiate Tag */
                    12345 >> 24,
                    (12345 & 0x00FF0000) >> 16,
                    (12345 & 0x0000FF00) >> 8,
                    12345 & 0xFF,

                    /* Advertised Receiver Window Credit (a_rwnd) */
                    112233 >> 24,
                    (112233 & 0x00FF0000) >> 16,
                    (112233 & 0x0000FF00) >> 8,
                    112233 & 0xFF,

                    /* Number of Outbound Streams */
                    25 >> 8,
                    25 & 0xFF,

                    /* Number of Inbound Streams */
                    10 >> 8,
                    10 & 0xFF,

                    /* Initial TSN */
                    0, 0, 0, 1,

                    /* IPv4 Address Parameter */
                    (ushort)SctpChunkParameterType.IPv4Address >> 8,
                    (ushort)SctpChunkParameterType.IPv4Address & 0xFF,
                    0, 8,
                    192, 168, 20, 20,
                }),
                new SctpInitiation(
                    new SctpInitiateTag(12345),
                    new SctpAdvertisedReceiverWindowCredit(112233),
                    new SctpNumberOutboundStreams(25),
                    new SctpNumberInboundStreams(10),
                    new SctpTransmissionSequenceNumber(1),
                    new ReadOnlyMemory<ISctpChunkParameterVariableLength>(
                        new ISctpChunkParameterVariableLength[]
                        {
                            new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 20, 20 }))
                        }))
            }
        };

    [Theory(DisplayName = $"{nameof(SctpInitiation)} :: {nameof(SctpInitiation.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpInitiation expected)
    {
        var actual = SctpInitiation.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpInitiation)} :: {nameof(SctpInitiation.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpInitiation chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
