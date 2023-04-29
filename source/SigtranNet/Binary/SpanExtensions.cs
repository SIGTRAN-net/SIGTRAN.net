/*
 * © 2023 SIGTRAN.net
 * Licensed by GNU Affero General Public License version 3
 */

namespace SigtranNet.Binary;

/// <summary>
/// Extension methods for <see cref="Span{T}" />.
/// </summary>
internal static class SpanExtensions
{
    /// <summary>
    /// Concatenates <paramref name="first" /> and <paramref name="second" />.
    /// </summary>
    /// <typeparam name="T">The type of element.</typeparam>
    /// <param name="first">The first <see cref="Span{T}" />.</param>
    /// <param name="second">A <see cref="Span{T}" /> to concatenate with <paramref name="first" />.</param>
    /// <returns>The concatenation of <paramref name="first" /> and <paramref name="second" />.</returns>
    internal static Span<T> Concat<T>(this Span<T> first, Span<T> second)
    {
        var resultLength = first.Length + second.Length;
        var result = new Span<T>(new T[resultLength]);
        first.CopyTo(result[0..first.Length]);
        second.CopyTo(result[first.Length..resultLength]);
        return result;
    }
}
