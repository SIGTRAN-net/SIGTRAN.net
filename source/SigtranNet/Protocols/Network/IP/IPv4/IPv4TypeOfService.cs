/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System;

namespace SigtranNet.Protocols.Network.IP.IPv4;

/// <summary>
/// The Internet Protocol (IP) Type of Service.
/// </summary>
/// <remarks>
///     From <a href="https://datatracker.ietf.org/doc/html/rfc791">RFC 791</a>:
///     <code>
///         The Type of Service provides an indication of the abstract
///         parameters of the quality of service desired. These parameters are
///         to be used to guide the selection of the actual service parameters
///         when transmitting a datagram through a particular network.Several
///         networks offer service precedence, which somehow treats high
///         precedence traffic as more important than other traffic(generally
///         by accepting only traffic above a certain precedence at time of high
///         load).  The major choice is a three way tradeoff between low-delay,
///         high-reliability, and high-throughput.
///     </code>
/// </remarks>
internal enum IPv4TypeOfService : byte
{
    /// <summary>
    /// High Reliability.
    /// </summary>
    ReliabilityHigh = 0b000_0_0_1_00,

    /// <summary>
    /// High Throughput.
    /// </summary>
    ThroughputHigh = 0b000_0_1_0_00,

    /// <summary>
    /// Low Delay.
    /// </summary>
    LowDelay = 0b000_1_0_0_00,

    /// <summary>
    /// Precedence: Priority.
    /// </summary>
    PrecedencePriority = 0b001_0_0_0_00,

    /// <summary>
    /// Precedence: Immediate.
    /// </summary>
    PrecedenceImmediate = 0b010_0_0_0_00,

    /// <summary>
    /// Precedence: Flash.
    /// </summary>
    PrecedenceFlash = 0b011_0_0_0_00,

    /// <summary>
    /// Precedence: Flash Override.
    /// </summary>
    PrecedenceFlashOverride = 0b100_0_0_0_00,

    /// <summary>
    /// Precedence: CRITIC/ECP.
    /// </summary>
    PrecedenceCriticEcp = 0b101_0_0_0_00,

    /// <summary>
    /// Precedence: Internetwork Control.
    /// </summary>
    PrecedenceInternetworkControl = 0b110_0_0_0_00,

    /// <summary>
    /// Precedence: Network Control.
    /// </summary>
    PrecedenceNetworkControl = 0b111_0_0_0_00,
}
