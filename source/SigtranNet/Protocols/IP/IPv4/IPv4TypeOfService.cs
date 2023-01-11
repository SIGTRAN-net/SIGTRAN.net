/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP.IPv4;

/// <summary>
/// The Internet Protocol (IP) Type of Service.
/// </summary>
internal enum IPv4TypeOfService : byte
{
    // Routine
    Routine_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b000_0_0_0_00,
    Routine_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b000_0_0_1_00,

    Routine_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b000_0_1_0_00,
    Routine_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b000_0_1_1_00,

    Routine_DelayLow_ThroughputNormal_ReliabilityNormal = 0b000_1_0_0_00,
    Routine_DelayLow_ThroughputNormal_ReliabilityHigh = 0b000_1_0_1_00,

    Routine_DelayLow_ThroughputHigh_ReliabilityNormal = 0b000_1_1_0_00,
    Routine_DelayLow_ThroughputHigh_ReliabilityHigh = 0b000_1_1_1_00,

    // Priority
    Priority_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b001_0_0_0_00,
    Priority_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b001_0_0_1_00,

    Priority_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b001_0_1_0_00,
    Priority_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b001_0_1_1_00,

    Priority_DelayLow_ThroughputNormal_ReliabilityNormal = 0b001_1_0_0_00,
    Priority_DelayLow_ThroughputNormal_ReliabilityHigh = 0b001_1_0_1_00,

    Priority_DelayLow_ThroughputHigh_ReliabilityNormal = 0b001_1_1_0_00,
    Priority_DelayLow_ThroughputHigh_ReliabilityHigh = 0b001_1_1_1_00,

    // Immediate
    Immediate_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b010_0_0_0_00,
    Immediate_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b010_0_0_1_00,

    Immediate_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b010_0_1_0_00,
    Immediate_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b010_0_1_1_00,

    Immediate_DelayLow_ThroughputNormal_ReliabilityNormal = 0b010_1_0_0_00,
    Immediate_DelayLow_ThroughputNormal_ReliabilityHigh = 0b010_1_0_1_00,

    Immediate_DelayLow_ThroughputHigh_ReliabilityNormal = 0b010_1_1_0_00,
    Immediate_DelayLow_ThroughputHigh_ReliabilityHigh = 0b010_1_1_1_00,

    // Flash
    Flash_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b011_0_0_0_00,
    Flash_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b011_0_0_1_00,

    Flash_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b011_0_1_0_00,
    Flash_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b011_0_1_1_00,

    Flash_DelayLow_ThroughputNormal_ReliabilityNormal = 0b011_1_0_0_00,
    Flash_DelayLow_ThroughputNormal_ReliabilityHigh = 0b011_1_0_1_00,

    Flash_DelayLow_ThroughputHigh_ReliabilityNormal = 0b011_1_1_0_00,
    Flash_DelayLow_ThroughputHigh_ReliabilityHigh = 0b011_1_1_1_00,

    // Flash Override
    FlashOverride_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b100_0_0_0_00,
    FlashOverride_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b100_0_0_1_00,

    FlashOverride_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b100_0_1_0_00,
    FlashOverride_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b100_0_1_1_00,

    FlashOverride_DelayLow_ThroughputNormal_ReliabilityNormal = 0b100_1_0_0_00,
    FlashOverride_DelayLow_ThroughputNormal_ReliabilityHigh = 0b100_1_0_1_00,

    FlashOverride_DelayLow_ThroughputHigh_ReliabilityNormal = 0b100_1_1_0_00,
    FlashOverride_DelayLow_ThroughputHigh_ReliabilityHigh = 0b100_1_1_1_00,

    // CRITIC / ECP
    CriticEcp_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b101_0_0_0_00,
    CriticEcp_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b101_0_0_1_00,

    CriticEcp_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b101_0_1_0_00,
    CriticEcp_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b101_0_1_1_00,

    CriticEcp_DelayLow_ThroughputNormal_ReliabilityNormal = 0b101_1_0_0_00,
    CriticEcp_DelayLow_ThroughputNormal_ReliabilityHigh = 0b101_1_0_1_00,

    CriticEcp_DelayLow_ThroughputHigh_ReliabilityNormal = 0b101_1_1_0_00,
    CriticEcp_DelayLow_ThroughputHigh_ReliabilityHigh = 0b101_1_1_1_00,

    // Internetwork Control
    Internetwork_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b110_0_0_0_00,
    Internetwork_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b110_0_0_1_00,

    Internetwork_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b110_0_1_0_00,
    Internetwork_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b110_0_1_1_00,

    Internetwork_DelayLow_ThroughputNormal_ReliabilityNormal = 0b110_1_0_0_00,
    Internetwork_DelayLow_ThroughputNormal_ReliabilityHigh = 0b110_1_0_1_00,

    Internetwork_DelayLow_ThroughputHigh_ReliabilityNormal = 0b110_1_1_0_00,
    Internetwork_DelayLow_ThroughputHigh_ReliabilityHigh = 0b110_1_1_1_00,

    // Network Control
    Network_DelayNormal_ThroughputNormal_ReliabilityNormal = 0b111_0_0_0_00,
    Network_DelayNormal_ThroughputNormal_ReliabilityHigh = 0b111_0_0_1_00,

    Network_DelayNormal_ThroughputHigh_ReliabilityNormal = 0b111_0_1_0_00,
    Network_DelayNormal_ThoughputHigh_ReliabilityHigh = 0b111_0_1_1_00,

    Network_DelayLow_ThroughputNormal_ReliabilityNormal = 0b111_1_0_0_00,
    Network_DelayLow_ThroughputNormal_ReliabilityHigh = 0b111_1_0_1_00,

    Network_DelayLow_ThroughputHigh_ReliabilityNormal = 0b111_1_1_0_00,
    Network_DelayLow_ThroughputHigh_ReliabilityHigh = 0b111_1_1_1_00,
}
