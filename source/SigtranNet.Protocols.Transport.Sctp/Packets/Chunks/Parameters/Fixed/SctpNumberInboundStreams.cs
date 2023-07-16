﻿/*
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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.Parameters.Fixed;

internal readonly partial struct SctpNumberInboundStreams : ISctpChunkParameter
{
    private const ushort LengthFixed = sizeof(ushort);
    internal readonly ushort value;

    internal SctpNumberInboundStreams(ushort value)
    {
        this.value = value;
    }

    ushort ISctpChunkParameter.ParameterLength => LengthFixed;
}
