﻿/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

using System.Buffers.Binary;

namespace SigtranNet.Binary;

/// <summary>
/// A CRC32c Checksum algorithm for an unsigned 32-bit checksum.
/// </summary>
/// <remarks>
///     This is an implementation of <a href="https://datatracker.ietf.org/doc/html/rfc9260#name-crc32c-checksum-calculation-2">RFC 9260 Appendix A. CRC32c Checksum Calculation</a>.
/// </remarks>
internal static class CRC32cChecksum
{
    private static readonly uint[] CRC32cTable =
        new uint[256]
        {
            0x00000000u, 0xF26B8303u, 0xE13B70F7u, 0x1350F3F4u,
            0xC79A971Fu, 0x35F1141Cu, 0x26A1E7E8u, 0xD4CA64EBu,
            0x8AD958CFu, 0x78B2DBCCu, 0x6BE22838u, 0x9989AB3Bu,
            0x4D43CFD0u, 0xBF284CD3u, 0xAC78BF27u, 0x5E133C24u,
            0x105EC76Fu, 0xE235446Cu, 0xF165B798u, 0x030E349Bu,
            0xD7C45070u, 0x25AFD373u, 0x36FF2087u, 0xC494A384u,
            0x9A879FA0u, 0x68EC1CA3u, 0x7BBCEF57u, 0x89D76C54u,
            0x5D1D08BFu, 0xAF768BBCu, 0xBC267848u, 0x4E4DFB4Bu,
            0x20BD8EDEu, 0xD2D60DDDu, 0xC186FE29u, 0x33ED7D2Au,
            0xE72719C1u, 0x154C9AC2u, 0x061C6936u, 0xF477EA35u,
            0xAA64D611u, 0x580F5512u, 0x4B5FA6E6u, 0xB93425E5u,
            0x6DFE410Eu, 0x9F95C20Du, 0x8CC531F9u, 0x7EAEB2FAu,
            0x30E349B1u, 0xC288CAB2u, 0xD1D83946u, 0x23B3BA45u,
            0xF779DEAEu, 0x05125DADu, 0x1642AE59u, 0xE4292D5Au,
            0xBA3A117Eu, 0x4851927Du, 0x5B016189u, 0xA96AE28Au,
            0x7DA08661u, 0x8FCB0562u, 0x9C9BF696u, 0x6EF07595u,
            0x417B1DBCu, 0xB3109EBFu, 0xA0406D4Bu, 0x522BEE48u,
            0x86E18AA3u, 0x748A09A0u, 0x67DAFA54u, 0x95B17957u,
            0xCBA24573u, 0x39C9C670u, 0x2A993584u, 0xD8F2B687u,
            0x0C38D26Cu, 0xFE53516Fu, 0xED03A29Bu, 0x1F682198u,
            0x5125DAD3u, 0xA34E59D0u, 0xB01EAA24u, 0x42752927u,
            0x96BF4DCCu, 0x64D4CECFu, 0x77843D3Bu, 0x85EFBE38u,
            0xDBFC821Cu, 0x2997011Fu, 0x3AC7F2EBu, 0xC8AC71E8u,
            0x1C661503u, 0xEE0D9600u, 0xFD5D65F4u, 0x0F36E6F7u,
            0x61C69362u, 0x93AD1061u, 0x80FDE395u, 0x72966096u,
            0xA65C047Du, 0x5437877Eu, 0x4767748Au, 0xB50CF789u,
            0xEB1FCBADu, 0x197448AEu, 0x0A24BB5Au, 0xF84F3859u,
            0x2C855CB2u, 0xDEEEDFB1u, 0xCDBE2C45u, 0x3FD5AF46u,
            0x7198540Du, 0x83F3D70Eu, 0x90A324FAu, 0x62C8A7F9u,
            0xB602C312u, 0x44694011u, 0x5739B3E5u, 0xA55230E6u,
            0xFB410CC2u, 0x092A8FC1u, 0x1A7A7C35u, 0xE811FF36u,
            0x3CDB9BDDu, 0xCEB018DEu, 0xDDE0EB2Au, 0x2F8B6829u,
            0x82F63B78u, 0x709DB87Bu, 0x63CD4B8Fu, 0x91A6C88Cu,
            0x456CAC67u, 0xB7072F64u, 0xA457DC90u, 0x563C5F93u,
            0x082F63B7u, 0xFA44E0B4u, 0xE9141340u, 0x1B7F9043u,
            0xCFB5F4A8u, 0x3DDE77ABu, 0x2E8E845Fu, 0xDCE5075Cu,
            0x92A8FC17u, 0x60C37F14u, 0x73938CE0u, 0x81F80FE3u,
            0x55326B08u, 0xA759E80Bu, 0xB4091BFFu, 0x466298FCu,
            0x1871A4D8u, 0xEA1A27DBu, 0xF94AD42Fu, 0x0B21572Cu,
            0xDFEB33C7u, 0x2D80B0C4u, 0x3ED04330u, 0xCCBBC033u,
            0xA24BB5A6u, 0x502036A5u, 0x4370C551u, 0xB11B4652u,
            0x65D122B9u, 0x97BAA1BAu, 0x84EA524Eu, 0x7681D14Du,
            0x2892ED69u, 0xDAF96E6Au, 0xC9A99D9Eu, 0x3BC21E9Du,
            0xEF087A76u, 0x1D63F975u, 0x0E330A81u, 0xFC588982u,
            0xB21572C9u, 0x407EF1CAu, 0x532E023Eu, 0xA145813Du,
            0x758FE5D6u, 0x87E466D5u, 0x94B49521u, 0x66DF1622u,
            0x38CC2A06u, 0xCAA7A905u, 0xD9F75AF1u, 0x2B9CD9F2u,
            0xFF56BD19u, 0x0D3D3E1Au, 0x1E6DCDEEu, 0xEC064EEDu,
            0xC38D26C4u, 0x31E6A5C7u, 0x22B65633u, 0xD0DDD530u,
            0x0417B1DBu, 0xF67C32D8u, 0xE52CC12Cu, 0x1747422Fu,
            0x49547E0Bu, 0xBB3FFD08u, 0xA86F0EFCu, 0x5A048DFFu,
            0x8ECEE914u, 0x7CA56A17u, 0x6FF599E3u, 0x9D9E1AE0u,
            0xD3D3E1ABu, 0x21B862A8u, 0x32E8915Cu, 0xC083125Fu,
            0x144976B4u, 0xE622F5B7u, 0xF5720643u, 0x07198540u,
            0x590AB964u, 0xAB613A67u, 0xB831C993u, 0x4A5A4A90u,
            0x9E902E7Bu, 0x6CFBAD78u, 0x7FAB5E8Cu, 0x8DC0DD8Fu,
            0xE330A81Au, 0x115B2B19u, 0x020BD8EDu, 0xF0605BEEu,
            0x24AA3F05u, 0xD6C1BC06u, 0xC5914FF2u, 0x37FACCF1u,
            0x69E9F0D5u, 0x9B8273D6u, 0x88D28022u, 0x7AB90321u,
            0xAE7367CAu, 0x5C18E4C9u, 0x4F48173Du, 0xBD23943Eu,
            0xF36E6F75u, 0x0105EC76u, 0x12551F82u, 0xE03E9C81u,
            0x34F4F86Au, 0xC69F7B69u, 0xD5CF889Du, 0x27A40B9Eu,
            0x79B737BAu, 0x8BDCB4B9u, 0x988C474Du, 0x6AE7C44Eu,
            0xBE2DA0A5u, 0x4C4623A6u, 0x5F16D052u, 0xAD7D5351u,
        };

    private static void CRC32c(ref uint c, byte d) =>
        c = (c >> 8) ^ CRC32cTable[(c ^ (d)) & 0xFF];

    /// <summary>
    /// Generates a CRC32c Checksum from <paramref name="data" /> while ignoring the embedded checksum itself at <paramref name="checksumIndex" />.
    /// </summary>
    /// <param name="data">The data for which to gnerate a CRC32c Checksum.</param>
    /// <param name="checksumIndex">The index at which the checksum itself is embedded.</param>
    /// <returns>The CRC32c Checksum for <paramref name="data" />.</returns>
    /// <remarks>
    ///     This is an implementation of <a href="https://datatracker.ietf.org/doc/html/rfc9260#name-crc32c-checksum-calculation-2">RFC 9260 Appendix A. CRC32c Checksum Calculation</a>.
    /// </remarks>
    internal static uint Generate(ReadOnlyMemory<byte> data, int checksumIndex)
    {
        var dataSpan = data.Span;

        uint crc32 = 0xFFFFFFFFu;

        var checksumRangeStart = checksumIndex;
        var checksumRangeEnd = checksumIndex + sizeof(uint);
        for (var i = 0; i < data.Length; ++i)
        {
            // Ignore checksum itself, pretend it's 0x00000000u.
            if (i >= checksumRangeStart && i < checksumRangeEnd)
            {
                CRC32c(ref crc32, 0);
                continue;
            }
            CRC32c(ref crc32, dataSpan[i]);
        }

        crc32 = ~crc32;

        return crc32;
    }

    /// <summary>
    /// Validates the provided <paramref name="data" /> using the CRC32c Checksum located at <paramref name="checksumIndex" />.
    /// </summary>
    /// <param name="data">The data to validate.</param>
    /// <param name="checksumIndex">The index at which the checksum is embedded in the data.</param>
    /// <returns>A <see langword="bool" /> that indicates whether the data is valid with the embedded checksum.</returns>
    internal static bool Validate(ReadOnlyMemory<byte> data, int checksumIndex)
    {
        var checksumSpan = data.Span[checksumIndex..(checksumIndex + sizeof(uint))];

        var crc32Original = BinaryPrimitives.ReadUInt32BigEndian(checksumSpan);
        var crc32 = Generate(data, checksumIndex);

        return crc32Original == crc32;
    }
}
