//
//  ArrayUtils.cs
//
//  Author:
//       Willem Van Onsem <vanonsem.willem@gmail.com>
//
//  Copyright (c) 2014 Willem Van Onsem
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;

namespace NUtils {
	/// <summary>
	/// A utility class that provides operations on arrays like swapping and more advanced copying.
	/// </summary>
	public static class ArrayUtils {

		/// <summary>
		/// Swap values from the given <paramref name="source"/> array to the <paramref name="target"/> array and vice
		/// versa in the specified index range.
		/// </summary>
		/// <param name="source">The first array to swap values from.</param>
		/// <param name="sourceOffset">The index of the <paramref name="source"/> array at which swapping begins.</param>
		/// <param name="target">The second array to swap values from.</param>
		/// <param name="targetOffset">The index of the <paramref name="target"/> array at which swapping begins.</param>
		/// <param name="length">The length of the ranges to swap values from.</param>
		/// <param name="sourceStride">The difference in index values between two swap opperations with respect to the <paramref name="source"/> array.</param>
		/// <param name="targetStride">The difference in index values between two swap opperations with respect to the <paramref name="target"/> array.</param>
		/// <typeparam name="T">The type of elements to swap.</typeparam>
		public static void Swap<T> (T[] source, int sourceOffset, T[] target, int targetOffset, int length, int sourceStride = 0x01, int targetStride = 0x01) {
			int mx = Math.Min (Math.Min (source.Length - sourceOffset, target.Length - targetOffset), length) + sourceOffset;
			T tmp;
			for (int i = sourceOffset, j = targetOffset; i < mx; i += sourceStride, j += targetStride) {
				tmp = source [i];
				source [i] = target [j];
				target [j] = tmp;
			}
		}

		/// <summary>
		/// Copy values from the given <paramref name="source"/> array to the given <paramref name="target"/> array.
		/// </summary>
		/// <param name="source">The given array that provides the values to copy.</param>
		/// <param name="sourceOffset">The offset of the <paramref name="source"/> array.</param>
		/// <param name="target">The given array to copy the values to.</param>
		/// <param name="targetOffset">The offset of the <paramref name="target"/> array.</param>
		/// <param name="length">The length of the ranges of the arrays.</param>
		/// <param name="sourceStride">The difference in index values between a copy operation with respect to the <paramref name="source"/> array.</param>
		/// <param name="sourceStride">The difference in index values between a copy operation with respect to the <paramref name="target"/> array.</param>
		/// <typeparam name="TS">The type of values provided by the <paramref name="source"/> array.</typeparam>
		/// <typeparam name="TT">The type of values stored in the <paramref name="target"/> array.</typeparam>
		public static void Copy<TS,TT> (TS[] source, int sourceOffset, TT[] target, int targetOffset, int length, int sourceStride = 0x01, int targetStride = 0x01) where TS : TT {
			int mx = Math.Min (Math.Min (source.Length - sourceOffset, target.Length - targetOffset), length) + sourceOffset;
			for (int i = sourceOffset, j = targetOffset; i < mx; i += sourceStride, j += targetStride) {
				target [j] = source [i];
			}
		}
	}
}