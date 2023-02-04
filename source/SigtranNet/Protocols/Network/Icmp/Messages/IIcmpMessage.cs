/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using SigtranNet.Binary;

namespace SigtranNet.Protocols.Network.Icmp.Messages;

/// <summary>
/// An Internet Control Message Protocol (ICMP) message.
/// </summary>
internal interface IIcmpMessage : IBinarySerializable
{
}

/// <summary>
/// An Internet Control Message Protocol (ICMP) message.
/// </summary>
/// <typeparam name="TMessage">The type of ICMP message.</typeparam>
internal interface IIcmpMessage<TMessage> : IIcmpMessage
    where TMessage : IIcmpMessage<TMessage>
{
}