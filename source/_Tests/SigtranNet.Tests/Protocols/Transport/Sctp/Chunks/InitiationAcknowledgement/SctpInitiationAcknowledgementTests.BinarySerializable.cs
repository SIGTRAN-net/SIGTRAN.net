/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks;
using SigtranNet.Protocols.Transport.Sctp.Chunks.InitiationAcknowledgement;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Fixed;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Cookie;
using System.Net;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.InitiationAcknowledgement;

public partial class SctpInitiationAcknowledgementTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Chunk header */
                    (byte)SctpChunkType.InitiationAcknowledgement,
                    0,
                    0, 40,

                    /* Initiate Tag */
                    0, 0, 0, 20,

                    /* Advertised Receiver Window Credit (a_rwnd) */
                    20000 >> 24,
                    (20000 & 0x00FF0000) >> 16,
                    (20000 & 0x0000FF00) >> 8,
                    20000 & 0xFF,

                    /* Number of Outbound Streams */
                    0, 25,

                    /* Number of Inbound Streams */
                    0, 10,

                    /* Initial TSN */
                    0, 0, 0, 5,

                    /* State Cookie Parameter */
                    (ushort)SctpChunkParameterType.StateCookie >> 8,
                    (ushort)SctpChunkParameterType.StateCookie & 0xFF,
                    0, 12,
                    1, 2, 3, 4,
                    4, 3, 2, 1,

                    /* IPv4 Address Parameter */
                    (ushort)SctpChunkParameterType.IPv4Address >> 8,
                    (ushort)SctpChunkParameterType.IPv4Address & 0xFF,
                    0, 8,
                    192, 168, 10, 10,
                }),
                new SctpInitiationAcknowledgement(
                    new SctpInitiateTag(20),
                    new SctpAdvertisedReceiverWindowCredit(20000),
                    new SctpNumberOutboundStreams(25),
                    new SctpNumberInboundStreams(10),
                    new SctpTransmissionSequenceNumber(5),
                    new ReadOnlyMemory<ISctpChunkParameterVariableLength>(
                        new ISctpChunkParameterVariableLength[]
                        {
                            new SctpStateCookieParameter(
                                new ReadOnlyMemory<byte>(
                                    new byte[]
                                    {
                                        1, 2, 3, 4,
                                        4, 3, 2, 1,
                                    })),
                            new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 10, 10 }))
                        })),
            },
        };

    [Theory(DisplayName = $"{nameof(SctpInitiationAcknowledgement)} :: {nameof(SctpInitiationAcknowledgement.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpInitiationAcknowledgement expected)
    {
        var actual = SctpInitiationAcknowledgement.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpInitiationAcknowledgement)} :: {nameof(SctpInitiationAcknowledgement.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpInitiationAcknowledgement chunk)
    {
        var actual = chunk.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
