/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;
using SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable.Address;
using System.Net;

namespace SigtranNet.Tests.Protocols.Transport.Sctp.Chunks.Parameters.ErrorCause;

public partial class SctpUnrecognizedParametersErrorTests
{
    public static readonly IEnumerable<object?[]> ReadOnlyMemoryParameters =
        new[]
        {
            new object?[]
            {
                new ReadOnlyMemory<byte>(new byte[]
                {
                    /* Error Cause Header */
                    (ushort)SctpErrorCauseCode.UnrecognizedParameters >> 8,
                    (ushort)SctpErrorCauseCode.UnrecognizedParameters & 0xFF,
                    20 >> 8,
                    20 & 0xFF,

                    /* IPv4 Address Parameter */
                    (ushort)SctpChunkParameterType.IPv4Address >> 8,
                    (ushort)SctpChunkParameterType.IPv4Address & 0xFF,
                    0, 8,
                    192, 168, 10, 10,

                    /* Invalid Mandatory Parameter */
                    (ushort)SctpChunkParameterType.HeartbeatInfo >> 8,
                    (ushort)SctpChunkParameterType.HeartbeatInfo & 0xFF,
                    0, 8,
                    1, 2, 3, 4
                }),
                new SctpUnrecognizedParametersError(new ISctpChunkParameterVariableLength[]
                {
                    new SctpIPv4AddressParameter(new IPAddress(new byte[] { 192, 168, 10, 10 })),
                    new SctpHeartbeatInfoParameter(new byte[] { 1, 2, 3, 4 })
                })
            }
        };

    [Theory(DisplayName = $"{nameof(SctpUnrecognizedParametersError)} :: {nameof(SctpUnrecognizedParametersError.FromReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void FromReadOnlyMemoryTests(ReadOnlyMemory<byte> memory, SctpUnrecognizedParametersError expected)
    {
        var actual = SctpUnrecognizedParametersError.FromReadOnlyMemory(memory);
        Assert.Equal(expected, actual);
    }

    [Theory(DisplayName = $"{nameof(SctpUnrecognizedParametersError)} :: {nameof(SctpUnrecognizedParametersError.ToReadOnlyMemory)}")]
    [MemberData(nameof(ReadOnlyMemoryParameters))]
    internal void ToReadOnlyMemoryTests(ReadOnlyMemory<byte> expected, SctpUnrecognizedParametersError parameter)
    {
        var actual = parameter.ToReadOnlyMemory();
        Assert.True(expected.Span.SequenceEqual(actual.Span));
    }
}
