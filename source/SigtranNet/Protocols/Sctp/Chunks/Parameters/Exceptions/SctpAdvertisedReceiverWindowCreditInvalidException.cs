/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.Sctp.Chunks.Parameters.Exceptions;

/// <summary>
/// An exception that is thrown if the Advertised Received Window Credit (a_rwnd) parameter value of an SCTP chunk is invalid.
/// </summary>
internal sealed class SctpAdvertisedReceiverWindowCreditInvalidException : SctpChunkParameterException
{
    /// <summary>
    /// Initializes a new instance of <see cref="SctpAdvertisedReceiverWindowCreditInvalidException" />.
    /// </summary>
    internal SctpAdvertisedReceiverWindowCreditInvalidException()
        : base(ExceptionMessages.AdvertisedReceiverWindowCreditInvalid)
    {
    }
}
