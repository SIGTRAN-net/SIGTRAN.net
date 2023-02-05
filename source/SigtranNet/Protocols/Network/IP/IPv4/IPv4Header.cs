/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Net;
using SigtranNet.Protocols.Network.IP.IPv4.Options;

namespace SigtranNet.Protocols.Network.IP.IPv4;

/// <summary>
/// A header of a datagram in the Internet Protocol (IP) version 4, also called the "Internet Header".
/// </summary>
internal readonly partial struct IPv4Header : IIPHeader<IPv4Header>
{
    /// <summary>
    /// The Internet Protocol version (4 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         The Version field indicates the format of the internet header.  This
    ///         document describes version 4.
    ///     </code>
    /// </remarks>
    internal const IPVersion Version = IPVersion.IPv4;

    /// <summary>
    /// The Internet Header Length (IHL) in 32 bit words (4 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         Internet Header Length is the length of the internet header in 32
    ///         bit words, and thus points to the beginning of the data. Note that
    ///         the minimum value for a correct header is 5.
    ///     </code>
    /// </remarks>
    internal readonly byte internetHeaderLength;

    /// <summary>
    /// The Type of Service (8 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         The Type of Service provides an indication of the abstract
    ///         parameters of the quality of service desired.These parameters are
    ///         to be used to guide the selection of the actual service parameters
    ///         when transmitting a datagram through a particular network.Several
    ///         networks offer service precedence, which somehow treats high
    ///         precedence traffic as more important than other traffic(generally
    ///         by accepting only traffic above a certain precedence at time of high
    ///         load).  The major choice is a three way tradeoff between low-delay,
    ///         high-reliability, and high-throughput.
    ///     </code>
    /// </remarks>
    internal readonly IPv4TypeOfService typeOfService;

    /// <summary>
    /// The total length of the datagram, measured in octets, including internet header and data (16 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         Total Length is the length of the datagram, measured in octets,
    ///         including internet header and data.This field allows the length of
    ///         a datagram to be up to 65,535 octets. Such long datagrams are
    ///         impractical for most hosts and networks.All hosts must be prepared
    ///         to accept datagrams of up to 576 octets (whether they arrive whole
    ///         or in fragments).  It is recommended that hosts only send datagrams
    ///         larger than 576 octets if they have assurance that the destination
    ///         is prepared to accept the larger datagrams.
    ///
    ///         The number 576 is selected to allow a reasonable sized data block to
    ///         be transmitted in addition to the required header information.  For
    ///         example, this size allows a data block of 512 octets plus 64 header
    ///         octets to fit in a datagram.  The maximal internet header is 60
    ///         octets, and a typical internet header is 20 octets, allowing a
    ///         margin for headers of higher level protocols.
    ///     </code>
    /// </remarks>
    internal readonly ushort totalLength;

    /// <summary>
    /// The identification of the datagram (16 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         An identifying value assigned by the sender to aid in assembling the
    ///         fragments of a datagram.
    ///     </code>
    /// </remarks>
    internal readonly ushort identification;

    /// <summary>
    /// Various Control Flags (3 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         Various Control Flags.
    ///
    ///         Bit 0: reserved, must be zero
    ///         Bit 1: (DF) 0 = May Fragment,  1 = Don't Fragment.
    ///         Bit 2: (MF) 0 = Last Fragment, 1 = More Fragments.
    ///     </code>
    /// </remarks>
    internal readonly IPv4Flags flags;

    /// <summary>
    /// The fragment offset (13 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         This field indicates where in the datagram this fragment belongs.
    ///         The fragment offset is measured in units of 8 octets (64 bits).  The
    ///         first fragment has offset zero.
    ///     </code>
    /// </remarks>
    internal readonly ushort fragmentOffset;

    /// <summary>
    /// The Time to Live (8 bits).
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         This field indicates the maximum time the datagram is allowed to
    ///         remain in the internet system.  If this field contains the value
    ///         zero, then the datagram must be destroyed.  This field is modified
    ///         in internet header processing.  The time is measured in units of
    ///         seconds, but since every module that processes a datagram must
    ///         decrease the TTL by at least one even if it process the datagram in
    ///         less than a second, the TTL must be thought of only as an upper
    ///         bound on the time a datagram may exist.  The intention is to cause
    ///         undeliverable datagrams to be discarded, and to bound the maximum
    ///         datagram lifetime.
    ///     </code>
    /// </remarks>
    internal readonly byte timeToLive;

    /// <summary>
    /// The Assigned Internet Protocol Number.
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         This field indicates the next level protocol used in the data
    ///         portion of the internet datagram.  The values for various protocols
    ///         are specified in "Assigned Numbers"
    ///     </code>
    /// </remarks>
    internal readonly IPProtocol protocol;

    /// <summary>
    /// The source address.
    /// </summary>
    internal readonly IPAddress sourceAddress;

    /// <summary>
    /// The destination address.
    /// </summary>
    internal readonly IPAddress destinationAddress;

    /// <summary>
    /// The optional header segments.
    /// </summary>
    /// <remarks>
    ///     From RFC 791:
    ///     <code>
    ///         The options may appear or not in datagrams.  They must be
    ///         implemented by all IP modules (host and gateways).  What is optional
    ///         is their transmission in any particular datagram, not their
    ///         implementation.
    ///
    ///         In some environments the security option may be required in all
    ///         datagrams.
    ///     </code>
    /// </remarks>
    internal readonly ReadOnlyMemory<IIPv4Option> options;

    /// <summary>
    /// Initializes a new instance of <see cref="IPv4Header" />.
    /// </summary>
    /// <param name="typeOfService">The type of service.</param>
    /// <param name="totalLength">The total length.</param>
    /// <param name="identification">The identification.</param>
    /// <param name="flags">The flags.</param>
    /// <param name="fragmentOffset">The fragment offset.</param>
    /// <param name="timeToLive">The time to live (TTL).</param>
    /// <param name="protocol">The IP protocol.</param>
    /// <param name="sourceAddress">The source address.</param>
    /// <param name="destinationAddress">The destination address.</param>
    /// <param name="options">The optional header segments.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// An <see cref="ArgumentOutOfRangeException" /> is thrown if the internet header length is too short.
    /// </exception>
    internal IPv4Header(
        IPv4TypeOfService typeOfService,
        ushort totalLength,
        ushort identification,
        IPv4Flags flags,
        ushort fragmentOffset,
        byte timeToLive,
        IPProtocol protocol,
        IPAddress sourceAddress,
        IPAddress destinationAddress,
        ReadOnlyMemory<IIPv4Option> options)
    {
        // Guards
        if (totalLength < internetHeaderLength)
            throw new ArgumentOutOfRangeException(nameof(totalLength));

        // Fields
        var internetHeaderLengthOctets = 5 * sizeof(uint);
        var optionsSpan = options.Span;
        for (var i = 0; i < options.Length; i++)
        {
            internetHeaderLengthOctets += optionsSpan[i].Length;
        }
        this.internetHeaderLength = (byte)(internetHeaderLengthOctets / sizeof(uint) + internetHeaderLengthOctets % sizeof(uint));

        this.typeOfService = typeOfService;
        this.totalLength = totalLength;
        this.identification = identification;
        this.flags = flags;
        this.fragmentOffset = fragmentOffset;
        this.timeToLive = timeToLive;
        this.protocol = protocol;
        this.sourceAddress = sourceAddress;
        this.destinationAddress = destinationAddress;
        this.options = options;
    }

    /// <inheritdoc />
    public byte InternetHeaderLength => this.internetHeaderLength;
}
