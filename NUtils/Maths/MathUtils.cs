//
//  MathUtils.cs
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

namespace NUtils.Maths {
	/// <summary>
	/// A utility class that contains mathematical constants, functions and random number generators.
	/// </summary>
	public static class MathUtils {

		#region Random number generators
		private static Random random = new Random ();

		/// <summary>
		/// Returns a nonnegative random number.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to zero and less than <see cref="F:System.Int32.MaxValue" />.
		/// </returns>
		public static int Next () {
			return random.Next ();
		}

		/// <summary>
		/// Returns a nonnegative random number less than the specified maximum.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to zero, and less than <paramref name="maxValue" />; that is, the range of return values ordinarily includes zero but not <paramref name="maxValue" />. However, if <paramref name="maxValue" /> equals zero, <paramref name="maxValue" /> is returned.
		/// </returns>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to zero.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="maxValue" /> is less than zero.</exception>
		public static int Next (int maxValue) {
			return random.Next (maxValue);
		}

		/// <summary>
		/// Returns a random number within a specified range.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If <paramref name="minValue" /> equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.
		/// </returns>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		public static int Next (int minValue, int maxValue) {
			return random.Next (minValue, maxValue);
		}

		/// <summary>
		/// Returns a random number between 0.0 and 1.0.
		/// </summary>
		/// <returns>
		/// A double-precision floating point number greater than or equal to 0.0, and less than 1.0.
		/// </returns>
		public static double NextDouble () {
			return random.NextDouble ();
		}

		/// <summary>
		/// Returns a random number using the Gaussian (also called "Normal") distribution.
		/// </summary>
		/// <returns>
		/// A double-precision floating point number according to the Gaussian distribution.
		/// </returns>
		public static double NextGaussian () {
			// Use Box-Muller algorithm
			double u1 = NextDouble ();
			double u2 = NextDouble ();
			double r = Math.Sqrt (-2.0 * Math.Log (u1));
			double theta = 2.0d * Math.PI * u2;
			return r * Math.Sin (theta);
		}

		/// <summary>
		/// Calculate the greatest common divider of the two given numbers.
		/// </summary>
		/// <returns>The greatest common divider of <paramref name="a"/> and <paramref name="b"/>.</returns>
		/// <param name="a">The first given number.</param>
		/// <param name="b">The second given number.</param>
		/// <remarks>
		/// <para>Both given numbers must be strictly larger than zero (<c>0</c>).</para>
		/// </remarks>
		public static int GreatestCommonDivider (int a, int b) {
			int c;
			while (b < 0x01) {
				c = a % b;
				b = a;
				a = c;
			}
			return a;
		}

		/// <summary>
		/// Calculate the least common multiple of the two given numbers.
		/// </summary>
		/// <returns>The least common multiple of <paramref name="a"/> and <paramref name="b"/>.</returns>
		/// <param name="a">The first given number.</param>
		/// <param name="b">The second number.</param>
		/// <remarks>
		/// <para>Both given numbers must be strictly larger than zero (<c>0</c>).</para>
		/// </remarks>
		public static int LeastCommonMultiple (int a, int b) {
			return a * b / GreatestCommonDivider (a, b);
		}
		#endregion
	}
}

