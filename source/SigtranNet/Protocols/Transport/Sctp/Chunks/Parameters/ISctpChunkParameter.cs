/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.Transport.Sctp.Chunks.Parameters;

/// <summary>
/// A parameter of an SCTP chunk.
/// </summary>
internal interface ISctpChunkParameter : IBinarySerializable
{
    /// <summary>
    /// Gets the length of the SCTP chunk parameter.
    /// </summary>
    /// <remarks>
    ///     From RFC 9260:
    ///     <code>
    ///         The Parameter Length field contains the size of the parameter in bytes, including the Parameter Type, Parameter Length, and Parameter Value fields. Thus, a parameter with a zero-length Parameter Value field would have a Parameter Length field of 4. The Parameter Length does not include any padding bytes.
    ///     </code>
    /// </remarks>
    ushort ParameterLength { get; }
}
