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

namespace SigtranNet.Protocols.Transport.Sctp.Packets.Chunks.PayloadData;

internal enum SctpPayloadProtocolIdentifier : uint
{
    /// <summary>
    /// Reserved by SCTP
    /// </summary>
    Reserved = 0,

    /// <summary>
    /// IUA
    /// </summary>
    IUA = 1,

    /// <summary>
    /// M2UA
    /// </summary>
    M2UA = 2,

    /// <summary>
    /// M3UA
    /// </summary>
    M3UA = 3,

    /// <summary>
    /// SUA
    /// </summary>
    SUA = 4,

    /// <summary>
    /// M2PA
    /// </summary>
    M2PA = 5,

    /// <summary>
    /// V5UA
    /// </summary>
    V5UA = 6,

    /// <summary>
    /// H.248
    /// </summary>
    H_248 = 7,

    /// <summary>
    /// BICC/Q.2150.3
    /// </summary>
    BICC_Q_2150_3 = 8,

    /// <summary>
    /// TALI
    /// </summary>
    TALI = 9,

    /// <summary>
    /// DUA
    /// </summary>
    DUA = 10,

    /// <summary>
    /// ASAP
    /// </summary>
    ASAP = 11,

    /// <summary>
    /// ENRP
    /// </summary>
    ENRP = 12,

    /// <summary>
    /// H.323
    /// </summary>
    H_323 = 13,

    /// <summary>
    /// Q.IPC/Q.2150.3
    /// </summary>
    Q_IPC_Q_2150_3 = 14,

    /// <summary>
    /// SIMCO
    /// </summary>
    SIMCO = 15,

    /// <summary>
    /// DPP Segment Chunk
    /// </summary>
    DDPSegmentChunk = 16,

    /// <summary>
    /// DDP Stream Session Control
    /// </summary>
    DDPStreamSessionControl = 17,

    /// <summary>
    /// S1 Application Protocol (S1AP)
    /// </summary>
    S1AP = 18,

    /// <summary>
    /// RUA
    /// </summary>
    RUA = 19,

    /// <summary>
    /// HNBAP
    /// </summary>
    HNBAP = 20,

    /// <summary>
    /// ForCES-HP
    /// </summary>
    ForCES_HP = 21,

    /// <summary>
    /// ForCES-MP
    /// </summary>
    ForCES_MP = 22,

    /// <summary>
    /// ForCES-LP
    /// </summary>
    ForCES_LP = 23,

    /// <summary>
    /// SBc-AP
    /// </summary>
    SBc_AP = 24,

    /// <summary>
    /// NBAP
    /// </summary>
    NBAP = 25,

    /* 26 Unassigned */

    /// <summary>
    /// X2AP
    /// </summary>
    X2AP = 27,

    /// <summary>
    /// Inter Router Capability Protocol (IRCP)
    /// </summary>
    IRCP = 28,

    /// <summary>
    /// LCS-AP
    /// </summary>
    LCS_AP = 29,

    /// <summary>
    /// MPICH2
    /// </summary>
    MPICH2 = 30,

    /// <summary>
    /// Service Area Broadcast Protocol (SABP)
    /// </summary>
    SABP = 31,

    /// <summary>
    /// Fractal Generator Protocol (FGP)
    /// </summary>
    FGP = 32,

    /// <summary>
    /// Ping Pong Protoocl (PPP)
    /// </summary>
    PPP = 33,

    /// <summary>
    /// CalcApp Protocol (CALCAPP)
    /// </summary>
    CALCAPP = 34,

    /// <summary>
    /// Scripting Service Protocol (SSP)
    /// </summary>
    SSP = 35,

    /// <summary>
    /// NetPerfMeter Protocol Control Channel (NPMP-CONTROL)
    /// </summary>
    NPMP_CONTROL = 36,

    /// <summary>
    /// NetPerfMeter Protocol Data Channel (NPMP-DATA)
    /// </summary>
    NPMP_DATA = 37,

    /// <summary>
    /// Echo (ECHO)
    /// </summary>
    Echo = 38,

    /// <summary>
    /// Discard (DISCARD)
    /// </summary>
    Discard = 39,

    /// <summary>
    /// Daytime (DAYTIME)
    /// </summary>
    Daytime = 40,

    /// <summary>
    /// Character Generator (CHARGEN)
    /// </summary>
    CharacterGenerator = 41,

    /// <summary>
    /// 3GPP RNA
    /// </summary>
    _3GPP_RNA = 42,

    /// <summary>
    /// 3GPP M2AP
    /// </summary>
    _3GPP_M2AP = 43,

    /// <summary>
    /// 3GPP M3AP
    /// </summary>
    _3GPP_M3AP = 44,

    /// <summary>
    /// SSH over SCTP
    /// </summary>
    SSH_over_SCTP = 45,

    /// <summary>
    /// Diameter in a SCTP DATA chunk
    /// </summary>
    Diameter_SCTP_DATA = 46,

    /// <summary>
    /// Diameter in a DTLS/SCTP DATA chunk
    /// </summary>
    Diameter_DTLS_SCTP_DATA = 47,

    /// <summary>
    /// R14P. BER encoded ASN.1 over SCTP
    /// </summary>
    R14P_BER_Encoded_ASN_1_over_SCTP = 48,

    /// <summary>
    /// Generic Data Transfer (GDT) Protocol
    /// </summary>
    GDT = 49,

    /// <summary>
    /// WebRTC DCEP
    /// </summary>
    WebRTC_DCEP = 50,

    /// <summary>
    /// WebRTC String
    /// </summary>
    WebRTC_String = 51,

    /// <summary>
    /// WebRTC Binary Partial
    /// </summary>
    [Obsolete]
    WebRTC_Binary_Partial = 52,

    /// <summary>
    /// WebRTC Binary
    /// </summary>
    WebRTC_Binary = 53,

    /// <summary>
    /// WebRTC String Partial
    /// </summary>
    [Obsolete]
    WebRTC_String_Partial = 54,

    /// <summary>
    /// 3GPP PUA
    /// </summary>
    _3GPP_PUA = 55,

    /// <summary>
    /// WebRTC String Empty
    /// </summary>
    WebRTC_String_Empty = 56,

    /// <summary>
    /// WebRTC Binary Empty
    /// </summary>
    WebRTC_Binary_Empty = 57,

    /// <summary>
    /// 3GPP XwAP
    /// </summary>
    _3GPP_XwAP = 58,

    /// <summary>
    /// 3GPP Xw-Control Plane
    /// </summary>
    _3GPP_Xw_ControlPlane = 59,

    /// <summary>
    /// 3GPP NG Application Protocol (NGAP)
    /// </summary>
    NGAP = 60,

    /// <summary>
    /// 3GPP Xn Application Protocol (XnAP)
    /// </summary>
    XnAP = 61,

    /// <summary>
    /// 3GPP F1 Application Protocol (F1 AP)
    /// </summary>
    F1_AP = 62,

    /// <summary>
    /// HTTP/SCTP
    /// </summary>
    HTTP_SCTP = 63,

    /// <summary>
    /// 3GPP E1 Application Protocol (E1AP)
    /// </summary>
    E1AP = 64,

    /// <summary>
    /// ELE2 Lawful Interception
    /// </summary>
    ELE2_LawfulInterception = 65,

    /// <summary>
    /// 3GPP NGAP over DTLS over SCTP
    /// </summary>
    _3GPP_NGAP_over_DTLS_over_SCTP = 66,

    /// <summary>
    /// 3GPP XnAP over DTLS over SCTP
    /// </summary>
    _3GPP_XnAP_over_DTLS_over_SCTP = 67,

    /// <summary>
    /// 3GPP F1AP over DTLS over SCTP
    /// </summary>
    _3GPP_F1AP_over_DTLS_over_SCTP = 68,

    /// <summary>
    /// 3GPP E1AP over DTLS over SCTP
    /// </summary>
    _3GPP_E1AP_over_DTLS_over_SCTP = 69,

    /// <summary>
    /// E2-CP
    /// </summary>
    E2_CP = 70,

    /// <summary>
    /// E2-UP
    /// </summary>
    E2_UP = 71,

    /// <summary>
    /// E2-DU
    /// </summary>
    E2_DU = 72,

    /// <summary>
    /// 3GPP W1AP
    /// </summary>
    _3GPP_W1AP = 73
}
