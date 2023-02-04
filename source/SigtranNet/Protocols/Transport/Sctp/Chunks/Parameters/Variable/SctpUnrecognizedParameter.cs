/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters.Variable;

/// <summary>
/// An Unrecognized Parameter in an SCTP Packet Chunk.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         This parameter is returned to the originator of the INIT chunk when the INIT chunk contains an unrecognized parameter that has a type that indicates it SHOULD be reported to the sender.
///     </code>
/// </remarks>
internal readonly partial struct SctpUnrecognizedParameter : ISctpChunkParameterVariableLength<SctpUnrecognizedParameter>
{
    private const SctpChunkParameterType ParameterTypeImplicit = SctpChunkParameterType.UnrecognizedParameter;

    /// <summary>
    /// The complete parameter length.
    /// </summary>
    internal readonly ushort parameterLength;

    /// <summary>
    /// The Unrecognized Parameter.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Parameter Value field will contain an unrecognized parameter copied from the INIT chunk complete with Parameter Type, Length, and Value fields.
    ///     </code>
    /// </remarks>
    internal readonly ISctpChunkParameterVariableLength unrecognizedParameter;

    /// <summary>
    /// Initializes a new instance of <see cref="SctpUnrecognizedParameter" />.
    /// </summary>
    /// <param name="unrecognizedParameter">The unrecognized parameter.</param>
    internal SctpUnrecognizedParameter(ISctpChunkParameterVariableLength unrecognizedParameter)
    {
        this.parameterLength = (ushort)(sizeof(uint) + unrecognizedParameter.ParameterLength);
        this.unrecognizedParameter = unrecognizedParameter;
    }

    SctpChunkParameterType ISctpChunkParameterVariableLength.ParameterType => ParameterTypeImplicit;
    ushort ISctpChunkParameter.ParameterLength => this.parameterLength;
}
