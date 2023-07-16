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
