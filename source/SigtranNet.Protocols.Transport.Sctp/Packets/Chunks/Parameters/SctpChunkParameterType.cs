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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters;

/// <summary>
/// An SCTP chunk parameter type.
/// </summary>
/// <remarks>
///     From RFC 9260:
///     <code>
///         The Type field is a 16-bit identifier of the type of parameter. It takes a value of 0 to 65534.
///
///             The value of 65535 is reserved for IETF-defined extensions. Values other than those defined in specific SCTP chunk descriptions are reserved for use by IETF.
///     </code>
/// </remarks>
internal enum SctpChunkParameterType : ushort
{
    /// <summary>
    /// Heartbeat Info
    /// </summary>
    HeartbeatInfo = 1,

    /* 2-4 Unassigned */

    /// <summary>
    /// IPv4 Address
    /// </summary>
    IPv4Address = 5,

    /// <summary>
    /// IPv6 Address
    /// </summary>
    IPv6Address = 6,

    /// <summary>
    /// State Cookie
    /// </summary>
    StateCookie = 7,

    /// <summary>
    /// Unrecognized Parameter
    /// </summary>
    UnrecognizedParameter = 8,

    /// <summary>
    /// Cookie Preservative
    /// </summary>
    CookiePreservative = 9,

    /* 10 Unassigned */

    /// <summary>
    /// Host Name Address
    /// </summary>
    HostNameAddress = 11,

    /// <summary>
    /// Supported Address Types
    /// </summary>
    SupportedAddressTypes = 12,

    /// <summary>
    /// Outgoing SSN Reset Request Parameter
    /// </summary>
    OutgoingSSNResetRequestParameter = 13,

    /// <summary>
    /// Incoming SSN Reset Request Parameter
    /// </summary>
    IncomingSSNResetRequestParameter = 14,

    /// <summary>
    /// SSN/TSN Reset Request Parameter
    /// </summary>
    SSN_TSN_ResetRequestParameter = 15,

    /// <summary>
    /// Re-configuration Response Parameter
    /// </summary>
    ReconfigurationResponseParameter = 16,

    /// <summary>
    /// Add Outgoing Streams Request Parameter
    /// </summary>
    AddOutgoingStreamsRequestParameter = 17,

    /// <summary>
    /// Add Incoming Streams Request Parameter
    /// </summary>
    AddIncomingStreamsRequestParameter = 18,

    /* 19-32767 Unassigned */

    /* 32768 Reserved for ECN Capable (0x8000) */

    /// <summary>
    /// Random (0x8002)
    /// </summary>
    Random = 32770,

    /// <summary>
    /// Chunk List (0x8003)
    /// </summary>
    ChunkList = 32771,

    /// <summary>
    /// Requested HMAC Algorithm Parameter (0x8004)
    /// </summary>
    RequestedHMACAlgorithmParameter = 32772,

    /// <summary>
    /// Padding (0x8005)
    /// </summary>
    Padding = 32773,

    /// <summary>
    /// Supported Extensions (0x8008)
    /// </summary>
    SupportedExtensions = 32776,

    /* 32777-49151 Unassigned */

    /// <summary>
    /// Forward TSN supported (0xC000)
    /// </summary>
    ForwardTSNSupported = 49152,

    /// <summary>
    /// Add IP Address (0xC001)
    /// </summary>
    AddIPAddress = 49153,

    /// <summary>
    /// Delete IP Address (0xC002)
    /// </summary>
    DeleteIPAddress = 49154,

    /// <summary>
    /// Error Cause Indication (0xC003)
    /// </summary>
    ErrorCauseIndication = 49155,

    /// <summary>
    /// Set Primary Address (0xC004)
    /// </summary>
    SetPrimaryAddress = 49156,

    /// <summary>
    /// Success Indication (0xC005)
    /// </summary>
    SuccessIndication = 49157,

    /// <summary>
    /// Adaptation Layer Indication (0xC006)
    /// </summary>
    AdaptationLayerIndication = 49158,

    /* 49159-65534 Unassigned */

    /* 65535 Reserved for IETF-defined Chunk Extensions */
}
