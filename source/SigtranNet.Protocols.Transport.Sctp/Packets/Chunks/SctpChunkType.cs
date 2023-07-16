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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks;

/// <summary>
/// An SCTP packet chunk type.
/// </summary>
internal enum SctpChunkType : byte
{
    /// <summary>
    /// Payload Data (DATA)
    /// </summary>
    PayloadData = 0,

    /// <summary>
    /// Initiation (INIT)
    /// </summary>
    Initiation = 1,

    /// <summary>
    /// Initiation Acknowledgement (INIT ACK)
    /// </summary>
    InitiationAcknowledgement = 2,

    /// <summary>
    /// Selective Acknowledgement (SACK)
    /// </summary>
    SelectiveAcknowledgement = 3,

    /// <summary>
    /// Heartbeat Request (HEARTBEAT)
    /// </summary>
    HeartbeatRequest = 4,

    /// <summary>
    /// Heartbeat Acknowledgement (HEARTBEAT ACK)
    /// </summary>
    HeartbeatAcknowledgement = 5,

    /// <summary>
    /// Abort (ABORT)
    /// </summary>
    Abort = 6,

    /// <summary>
    /// Shutdown (SHUTDOWN)
    /// </summary>
    Shutdown = 7,

    /// <summary>
    /// Shutdown Acknowledgement (SHUTDOWN ACK)
    /// </summary>
    ShutdownAcknowledgement = 8,

    /// <summary>
    /// Operation Error (ERROR)
    /// </summary>
    OperationError = 9,

    /// <summary>
    /// State Cookie (COOKIE ECHO)
    /// </summary>
    StateCookie = 10,

    /// <summary>
    /// Cookie Acknowledgement (COOKIE ACK)
    /// </summary>
    CookieAcknowledgement = 11,

    /// <summary>
    /// Reserved for Explicit COngestion Notification Echo (ECNE)
    /// </summary>
    ReservedForExplicitCongestionNotificationEcho = 12,

    /// <summary>
    /// Reserved for Congestion Window Reduced (CWR)
    /// </summary>
    ReservedForCongestionWindowReduced = 13,

    /// <summary>
    /// Shutdown Complete (SHUTDOWN COMPLETE)
    /// </summary>
    ShutdownComplete = 14
}
