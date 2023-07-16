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

using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.DestinationUnreachable;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Echo;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Information;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.ParameterProblem;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Redirect;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.SourceQuench;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.TimeExceeded;
using SigtranNet.Protocols.Network.IP.IPv4.Icmp.Messages.Timestamp;

namespace SigtranNet.Protocols.Network.IP.IPv4.Icmp;

/// <summary>
/// Processes the Internet Control Message Protocol (ICMP).
/// </summary>
internal sealed class IcmpProcessor :
    INetworkProcessor<IcmpDestinationUnreachableMessage>,
    INetworkProcessor<IcmpEchoMessage>,
    INetworkProcessor<IcmpInformationMessage>,
    INetworkProcessor<IcmpParameterProblemMessage>,
    INetworkProcessor<IcmpRedirectMessage>,
    INetworkProcessor<IcmpSourceQuenchMessage>,
    INetworkProcessor<IcmpTimeExceededMessage>,
    INetworkProcessor<IcmpTimestampMessage>
{
}
