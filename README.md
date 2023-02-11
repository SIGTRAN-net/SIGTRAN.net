# SIGTRAN.net

A suite for telecommunications with SS7 over IP using .NET

**This project is in development**; please have patience until there is a viable product.

## Documentation

This repository provides extensive [documentation](documentation/Index.md) using [Markdown](https://www.markdownguide.org/).

## Technology stack

SIGTRAN.net is developed from the Network Layer (3) to the Application Layer (7) in the OSI model of systems interconnection.

### Network Layer

In the Network Layer, SIGTRAN.net has implementations of the IP and the ICMP protocols.

#### Internet Protocol (IP)

Since Sockets in .NET have extensive support for TCP and UDP in the Transport Layer, they do not need an implementation of the Internet Protocol (IP),
because Sockets already provide the implementation of the IP.

SIGTRAN, however, uses the SCTP protocol in the Transport Layer and requires its specification in the IP header.
Therefore, an implementation of the IP is required. An additional advantage is that SIGTRAN.net has more control over IP datagrams.

For more information on the IP implementation, please refer to the [documentation](documentation/Protocols/Network/IP/Index.md).

#### Internet Control Message Protocol (ICMP)

To handle faults in IP communication, SIGTRAN.net has an implementation of the Internet Control Message Protocol (ICMP).

For more information on the ICMP implementation, please refer to the [documentation](documentation/Protocols/Network/ICMP/Index.md).

### Transport Layer

SIGTRAN uses SCTP as the protocol in the Transport Layer. This differs from TCP and UDP in that its core paradigm is using streams for communication.
This is particularly advantageous in streaming data and channels.

#### Stream Control Transmission Protocol (SCTP)

SIGTRAN.net has its own implementation of the Stream Control Transmission Protocol (SCTP), because .NET does not seem to natively support it.

For more information on the SCTP implementation, please refer to the [documentation](documentation/Protocols/Transport/SCTP/Index.md).