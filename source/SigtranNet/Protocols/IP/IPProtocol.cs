/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Protocols.IP;

/// <summary>
/// Assigned Internet Protocol Number
/// </summary>
/// <remarks>
///     Assigned by IANA.<br />
///     See also <a href="https://www.iana.org/assignments/protocol-numbers/protocol-numbers.xhtml">Assigned Internet Protocol Numbers</a>.
/// </remarks>
internal enum IPProtocol : byte
{
    /// <summary>
    /// IPv6 Hop-by-Hop Option
    /// </summary>
    HOPOPT = 0,

    /// <summary>
    /// Internet Control Message
    /// </summary>
    ICMP = 1,

    /// <summary>
    /// Internet Group Management
    /// </summary>
    IGMP = 2,

    /// <summary>
    /// Gateway-to-Gateway
    /// </summary>
    GGP = 3,

    /// <summary>
    /// IPv4 encapsulation
    /// </summary>
    IPv4 = 4,

    /// <summary>
    /// Stream
    /// </summary>
    ST = 5,

    /// <summary>
    /// Transmission Control
    /// </summary>
    TCP = 6,

    /// <summary>
    /// CBT
    /// </summary>
    CBT = 7,

    /// <summary>
    /// Exterior Gateway Protocol
    /// </summary>
    EGP = 8,

    /// <summary>
    /// any private interior gateway (used by Cisco for their IGRP)
    /// </summary>
    IGP = 9,

    /// <summary>
    /// BBN RCC Monitoring
    /// </summary>
    BBN_RCC_MON = 10,

    /// <summary>
    /// Network Voice Protocol
    /// </summary>
    NVP_II = 11,

    /// <summary>
    /// PUP
    /// </summary>
    PUP = 12,

    /// <summary>
    /// ARGUS
    /// </summary>
    [Obsolete]
    ARGUS = 13,

    /// <summary>
    /// EMCON
    /// </summary>
    EMCON = 14,

    /// <summary>
    /// Cross Net Debugger
    /// </summary>
    XNET = 15,

    /// <summary>
    /// Chaos
    /// </summary>
    CHAOS = 16,

    /// <summary>
    /// User Datagram
    /// </summary>
    UDP = 17,

    /// <summary>
    /// Multiplexing
    /// </summary>
    MUX = 18,

    /// <summary>
    /// DCN Measurement Subsystems
    /// </summary>
    DCN_MEAS = 19,

    /// <summary>
    /// Host Monitoring
    /// </summary>
    HMP = 20,

    /// <summary>
    /// Packet Radio Measurement
    /// </summary>
    PRM = 21,

    /// <summary>
    /// XEROX NS IDP
    /// </summary>
    XNS_IDP = 22,

    /// <summary>
    /// Trunk-1
    /// </summary>
    TRUNK_1 = 23,

    /// <summary>
    /// Trunk-2
    /// </summary>
    TRUNK_2 = 24,

    /// <summary>
    /// Leaf-1
    /// </summary>
    LEAF_1 = 25,

    /// <summary>
    /// Leaf-2
    /// </summary>
    LEAF_2 = 26,

    /// <summary>
    /// Reliable Data Protocol
    /// </summary>
    RDP = 27,

    /// <summary>
    /// Internet Reliable Transaction
    /// </summary>
    IRTP = 28,

    /// <summary>
    /// ISO Transport Protocol Class 4
    /// </summary>
    ISO_TP4 = 29,

    /// <summary>
    /// Bulk Data Transfer Protocol
    /// </summary>
    NETBLT = 30,

    /// <summary>
    /// MFE Network Services Protocol
    /// </summary>
    MFE_NSP = 31,

    /// <summary>
    /// MERIT Internodal Protocol
    /// </summary>
    MERIT_INP = 32,

    /// <summary>
    /// Datagram Congestion Control Protocol
    /// </summary>
    DCCP = 33,

    /// <summary>
    /// Third Party Connect Protocol
    /// </summary>
    _3PC = 34,

    /// <summary>
    /// Inter-Domain Policy Routing Protocol
    /// </summary>
    IDPR = 35,

    /// <summary>
    /// XTP
    /// </summary>
    XTP = 36,

    /// <summary>
    /// Datagram Delivery Protocol
    /// </summary>
    DDP = 37,

    /// <summary>
    /// IDPR Control Message Transport Protocol
    /// </summary>
    IDPR_CMTP = 38,

    /// <summary>
    /// TP++ Transport Control
    /// </summary>
    TPpp = 39,

    /// <summary>
    /// IL Transport Protocol
    /// </summary>
    IL = 40,

    /// <summary>
    /// IPv6 encapsulation
    /// </summary>
    IPv6 = 41,

    /// <summary>
    /// Source Demand Routing Protocol
    /// </summary>
    SDRP = 42,

    /// <summary>
    /// Routing Header for IPv6
    /// </summary>
    IPv6_Route = 43,

    /// <summary>
    /// Fragment Header for IPv6
    /// </summary>
    IPv6_Frag = 44,

    /// <summary>
    /// Inter-Domain Routing Protocol
    /// </summary>
    IDRP = 45,

    /// <summary>
    /// Reservation Protocol
    /// </summary>
    RSVP = 46,

    /// <summary>
    /// Generic Routing Encapsulation
    /// </summary>
    GRE = 47,

    /// <summary>
    /// Dynamic Source Routing Protocol
    /// </summary>
    DSR = 48,

    /// <summary>
    /// BNA
    /// </summary>
    BNA = 49,

    /// <summary>
    /// Encap Security Payload
    /// </summary>
    ESP = 50,

    /// <summary>
    /// Authentication Header
    /// </summary>
    AH = 51,

    /// <summary>
    /// Integrated Net Layer Security TUBA
    /// </summary>
    I_NSLP = 52,

    /// <summary>
    /// IP with Encryption
    /// </summary>
    [Obsolete]
    SWIPE = 53,

    /// <summary>
    /// NBMA Address Resolution Protocol
    /// </summary>
    NARP = 54,

    /// <summary>
    /// IP Mobility
    /// </summary>
    MOBILE = 55,

    /// <summary>
    /// Transport Layer Security Protocol using Kryptonet key
    /// </summary>
    TLSP = 56,

    /// <summary>
    /// SKIP
    /// </summary>
    SKIP = 57,

    /// <summary>
    /// ICMP for IPv6
    /// </summary>
    IPv6_ICMP = 58,

    /// <summary>
    /// No Next Header for IPv6
    /// </summary>
    IPv6_NoNxt = 59,

    /// <summary>
    /// Destination Options for IPv6
    /// </summary>
    IPv6_Opts = 60,

    /// <summary>
    /// Any host internal protocol
    /// </summary>
    AnyHostInternal = 61,

    /// <summary>
    /// CFTP
    /// </summary>
    CFTP = 62,

    /// <summary>
    /// Any local network
    /// </summary>
    AnyLocalNetwork = 63,

    /// <summary>
    /// SATNET and Backroom EXPAK
    /// </summary>
    SAT_EXPAK = 64,

    /// <summary>
    /// Kryptolan
    /// </summary>
    KRYPTOLAN = 65,

    /// <summary>
    /// MIT Remote Virtual Disk Protocol
    /// </summary>
    RVD = 66,

    /// <summary>
    /// Internet Pluribus Packet Core
    /// </summary>
    IPPC = 67,

    /// <summary>
    /// Any distributed file system
    /// </summary>
    AnyDistributedFileSystem = 68,

    /// <summary>
    /// SATNET Monitoring
    /// </summary>
    SAT_MON = 69,

    /// <summary>
    /// VISA Protocol
    /// </summary>
    VISA = 70,

    /// <summary>
    /// Internet Packet Core Utility
    /// </summary>
    IPCV = 71,

    /// <summary>
    /// Computer Protocol Network Executive
    /// </summary>
    CPNX = 72,

    /// <summary>
    /// Computer Protocol Heart Beat
    /// </summary>
    CPHB = 73,

    /// <summary>
    /// Wang Span Network
    /// </summary>
    WSN = 74,

    /// <summary>
    /// Packet Video Protocol
    /// </summary>
    PVP = 75,

    /// <summary>
    /// Backroom SATNET Monitoring
    /// </summary>
    BR_SAT_MON = 76,

    /// <summary>
    /// SUN ND PROTOCOL-Temporary
    /// </summary>
    SUN_ND = 77,

    /// <summary>
    /// WIDEBAND Monitoring
    /// </summary>
    WB_MON = 78,

    /// <summary>
    /// WIDEBAND EXPAK
    /// </summary>
    WB_EXPAK = 79,

    /// <summary>
    /// ISO Internet Protocol
    /// </summary>
    ISO_IP = 80,

    /// <summary>
    /// VMTP
    /// </summary>
    VMTP = 81,

    /// <summary>
    /// SECURE-VMTP
    /// </summary>
    SECURE_VMTP = 82,

    /// <summary>
    /// VINES
    /// </summary>
    VINES = 83,

    /// <summary>
    /// Transaction Transport Protocol
    /// </summary>
    TTP = 84,

    /// <summary>
    /// Internet Protocol Traffic Manager
    /// </summary>
    IPTM = 84,

    /// <summary>
    /// NSFNET-IGP
    /// </summary>
    NSFNET_IGP = 85,

    /// <summary>
    /// Dissimilar Gateway Protocol
    /// </summary>
    DGP = 86,

    /// <summary>
    /// TCF
    /// </summary>
    TCF = 87,

    /// <summary>
    /// EIGRP
    /// </summary>
    EIGRP = 88,

    /// <summary>
    /// OSPFIGP
    /// </summary>
    OSPFIGP = 89,

    /// <summary>
    /// Sprite RPC Protocol
    /// </summary>
    Sprite_RPC = 90,

    /// <summary>
    /// Locus Address Resolution Protocol
    /// </summary>
    LARP = 91,

    /// <summary>
    /// Multicast Transport Protocol
    /// </summary>
    MTP = 92,

    /// <summary>
    /// AX.25 Frames
    /// </summary>
    AX_25 = 93,

    /// <summary>
    /// IP-within-IP Encapsulation Protocol
    /// </summary>
    IPIP = 94,

    /// <summary>
    /// Mobile Internetworking Control Pro.
    /// </summary>
    [Obsolete]
    MICP = 95,

    /// <summary>
    /// Semaphore Communications Sec. Pro.
    /// </summary>
    SCC_SP = 96,

    /// <summary>
    /// Ethernet-within-IP Encapsulation
    /// </summary>
    ETHERIP = 97,

    /// <summary>
    /// Encapsulation Header
    /// </summary>
    ENCAP = 98,

    /// <summary>
    /// Any private encryption scheme
    /// </summary>
    AnyPrivateEncryptionScheme = 99,

    /// <summary>
    /// GMTP
    /// </summary>
    GMTP = 100,

    /// <summary>
    /// Ipsilon Flow Management Protocol
    /// </summary>
    IFMP = 101,

    /// <summary>
    /// PNNI over IP
    /// </summary>
    PNNI = 102,

    /// <summary>
    /// Protocol Independent Multicast
    /// </summary>
    PIM = 103,

    /// <summary>
    /// ARIS
    /// </summary>
    ARIS = 104,

    /// <summary>
    /// SCPS
    /// </summary>
    SCPS = 105,

    /// <summary>
    /// QNX
    /// </summary>
    QNX = 106,

    /// <summary>
    /// Active Networks
    /// </summary>
    A_N = 107,

    /// <summary>
    /// IP Payload Compression Protocol
    /// </summary>
    IPComp = 108,

    /// <summary>
    /// Sitara Networks Protocol
    /// </summary>
    SNP = 109,

    /// <summary>
    /// Compaq Peer Protocol
    /// </summary>
    Compaq_Peer = 110,

    /// <summary>
    /// IPX in IP
    /// </summary>
    IPX_in_IP = 111,

    /// <summary>
    /// Virtual Router Redundancy Protocol
    /// </summary>
    VRRP = 112,

    /// <summary>
    /// PGM Reliable Transport Protocol
    /// </summary>
    PGM = 113,

    /// <summary>
    /// Any 0-hop protocol
    /// </summary>
    Any0HopProtocol = 114,

    /// <summary>
    /// Layer Two Tunneling Protocol
    /// </summary>
    L2TP = 115,

    /// <summary>
    /// D-II Data Exchange (DDX)
    /// </summary>
    DDX = 116,

    /// <summary>
    /// Interactive Agent Transfer Protocol
    /// </summary>
    IATP = 117,

    /// <summary>
    /// Schedule Transfer Protocol
    /// </summary>
    STP = 118,

    /// <summary>
    /// SpectraLink Radio Protocol
    /// </summary>
    SRP = 119,

    /// <summary>
    /// UTI
    /// </summary>
    UTI = 120,

    /// <summary>
    /// Simple Message Protocol
    /// </summary>
    SMP = 121,

    /// <summary>
    /// Simple Multicast Protocol
    /// </summary>
    [Obsolete]
    SM = 122,

    /// <summary>
    /// Performance Transparency Protocol
    /// </summary>
    PTP = 123,

    /// <summary>
    /// ISIS over IPv4
    /// </summary>
    ISIS_over_IPv4 = 124,

    /// <summary>
    /// FIRE
    /// </summary>
    FIRE = 125,

    /// <summary>
    /// Combat Radio Transport Protocol
    /// </summary>
    CRTP = 126,

    /// <summary>
    /// Combat Radio User Datagram
    /// </summary>
    CRUDP = 127,

    /// <summary>
    /// SSCOPMCE
    /// </summary>
    SSCOPMCE = 128,

    /// <summary>
    /// IPLT
    /// </summary>
    IPLT = 129,

    /// <summary>
    /// Secure Packet Shield
    /// </summary>
    SPS = 130,

    /// <summary>
    /// Private IP Encapsulation within IP
    /// </summary>
    PIPE = 131,

    /// <summary>
    /// Stream Control Transmission Protocol
    /// </summary>
    SCTP = 132,

    /// <summary>
    /// Fibre Channel
    /// </summary>
    FC = 133,

    /// <summary>
    /// RSVP E2E IGNORE
    /// </summary>
    RSVP_E2E_IGNORE = 134,

    /// <summary>
    /// Mobility Header
    /// </summary>
    MobilityHeader = 135,

    /// <summary>
    /// UDP Lite
    /// </summary>
    UDPLite = 136,

    /// <summary>
    /// MPLS in IPs
    /// </summary>
    MPLS_in_IP = 137,

    /// <summary>
    /// MANET Protocols
    /// </summary>
    Manet = 138,

    /// <summary>
    /// Host Identity Protocol
    /// </summary>
    HIP = 139,

    /// <summary>
    /// Shim6 Protocol
    /// </summary>
    Shim6 = 140,

    /// <summary>
    /// Wrapped Encapsulating Security Payload
    /// </summary>
    WESP = 141,

    /// <summary>
    /// Robust Header Compression
    /// </summary>
    ROHC = 142,

    /// <summary>
    /// Ethernet
    /// </summary>
    Ethernet = 143,

    /// <summary>
    /// AGGFRAG encapsulation payload for ESP
    /// </summary>
    AGGFRAG = 144,

    /// <summary>
    /// Use for experimentation and testing
    /// </summary>
    ExperimentationAndTesting_253 = 253,

    /// <summary>
    /// Use for experimentation and testing
    /// </summary>
    ExperimentationAndTesting_254 = 254,

    /// <summary>
    /// Reserved
    /// </summary>
    Reserved = 255
}
