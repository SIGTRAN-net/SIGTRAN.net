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

namespace SigtranNet.Protocols.Network.IP.IPv4.Datagrams.Header.Options.Security;

/// <summary>
/// An optional security segment in an IPv4 header.
/// </summary>
/// <remarks>
///     From RFC 791:
///     <code>
///         This option provides a way for hosts to send security,
///         compartmentation, handling restrictions, and TCC (closed user
///         group) parameters.
///     </code>
///     <code>
///         Must be copied on fragmentation.  This option appears at most
///         once in a datagram.
///     </code>
/// </remarks>
internal readonly partial struct IPv4OptionSecurity : IIPv4Option
{
    /// <summary>
    /// The fixed length of an IPv4 Security option.
    /// </summary>
    internal const byte LengthFixed = 11;

    /// <summary>
    /// The IPv4 option type.
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         The option-type octet is viewed as having 3 fields:
    ///
    ///             1 bit copied flag,
    ///             2 bits option class,
    ///             5 bits option number.
    /// 
    ///         The copied flag indicates that this option is copied into all
    ///         fragments on fragmentation.
    ///
    ///             0 = not copied
    ///             1 = copied
    ///
    ///         The option classes are:
    ///
    ///             0 = control
    ///             1 = reserved for future use
    ///             2 = debugging and measurement
    ///             3 = reserved for future use
    ///     </code>
    /// </remarks>
    internal readonly IPv4OptionType optionType;

    /// <summary>
    /// The security level of the data in the IPv4 datagram.
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         Specifies one of 16 levels of security (eight of which are
    ///         reserved for future use).
    ///     </code>
    /// </remarks>
    internal readonly IPv4OptionSecurityLevel securityLevel;

    /// <summary>
    /// The security compartments that apply to the data in the IPv4 datagram.
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         An all zero value is used when the information transmitted is
    ///         not compartmented.Other values for the compartments field
    ///         may be obtained from the Defense Intelligence Agency.
    ///     </code>
    /// </remarks>
    internal readonly ushort compartments;

    /// <summary>
    /// The handling restrictions that apply to the data in the IPv4 datagram.
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         The values for the control and release markings are
    ///         alphanumeric digraphs and are defined in the Defense
    ///         Intelligence Agency Manual DIAM 65-19, "Standard Security
    ///         Markings".
    ///     </code>
    /// </remarks>
    internal readonly ushort handlingRestrictions;

    /// <summary>
    /// The transmission control code that applies to the data in the IPv4 datagram.
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         Provides a means to segregate traffic and define controlled
    ///         communities of interest among subscribers.The TCC values are
    ///         trigraphs, and are available from HQ DCA Code 530.
    ///     </code>
    /// </remarks>
    internal readonly uint transmissionControlCode;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4OptionSecurity" />.
    /// </summary>
    /// <param name="optionType">The option type.</param>
    /// <param name="securityLevel">The security level.</param>
    /// <param name="compartments">The compartments.</param>
    /// <param name="handlingRestrictions">The handling restrictions.</param>
    /// <param name="transmissionControlCode">The transmission control code.</param>
    internal IPv4OptionSecurity(
        IPv4OptionType optionType,
        IPv4OptionSecurityLevel securityLevel,
        ushort compartments,
        ushort handlingRestrictions,
        uint transmissionControlCode)
    {
        this.optionType = optionType;
        this.securityLevel = securityLevel;
        this.compartments = compartments;
        this.handlingRestrictions = handlingRestrictions;
        this.transmissionControlCode = transmissionControlCode;
    }

    /// <inheritdoc />
    byte IIPv4Option.Length => LengthFixed;
}
