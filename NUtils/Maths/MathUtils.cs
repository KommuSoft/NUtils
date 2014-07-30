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
using System.Collections.Generic;

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
		/// Generate a new <see cref="ulong"/> value.
		/// </summary>
		/// <returns>The randomly generated <see cref="ushort"/>.</returns>
		public static ulong NextUlong () {
			ulong val = ((ulong)random.Next ()) << 0x28;
			val ^= ((ulong)random.Next ()) << 0x14;
			val ^= (ulong)random.Next ();
			return val;
		}

		/// <summary>
		/// Generate a new <see cref="ushort"/> value.
		/// </summary>
		/// <returns>The randomly generated <see cref="ushort"/>.</returns>
		public static ushort NextUshort () {
			return ((ushort)random.Next ());
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
		/// Writes values to the given <see cref="T:IList`1"/> of probabilities. The values are uniformly distributed
		/// and sum up to one (<c>1</c>).
		/// </summary>
		/// <param name="list">The list where item should be written to.</param>
		public static void NextScaledDistribution (IList<double> list) {
			double sum = 0.0d;
			int n = list.Count;
			for (int i = 0x00; i < n; i++) {
				double x = random.Next ();
				sum += x;
				list [i] = x;
			}
			sum = 1.0d / sum;
			for (int i = 0x00; i < n; i++) {
				list [i] *= sum;
			}
		}

		/// <summary>
		/// Writes values to the each of the given rows of the given probability matrix.
		/// The values are uniformly distributed and sum up to one (<c>1</c>) per list.
		/// </summary>
		/// <param name="lists">A matrix that must be filled with uniformly distributed positive values
		/// such that every row sums up to one.</param>
		public static void NextScaledDistribution (double[,] lists) {
			double sum = 0.0d;
			int m = lists.GetLength (0x00);
			int n = lists.GetLength (0x01);
			for (int i = 0x00; i < m; i++) {
				for (int j = 0x00; j < n; j++) {
					double x = random.Next ();
					sum += x;
					lists [i, j] = x;
				}
				sum = 1.0d / sum;
				for (int j = 0x00; j < n; j++) {
					lists [i, j] *= sum;
				}
			}
		}

		/// <summary>
		/// Writes values to the each of the given <see cref="T:IList`1"/> of probabilities.
		/// The values are uniformly distributed and sum up to one (<c>1</c>) per list.
		/// </summary>
		/// <param name="lists">A list of lists, each list is filled with scaled values that sum up to one.</param>
		public static void NextScaledDistribution (IList<IList<double>> lists) {
			foreach (IList<double> list in lists) {
				NextScaledDistribution (list);
			}
		}
		#endregion
		#region Number theory
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
			while (b > 0x00) {
				c = a % b;
				a = b;
				b = c;
			}
			return a;
		}

		/// <summary>
		/// Calculate the greatest common divider of the two given numbers.
		/// </summary>
		/// <returns>The greatest common divider of list of given numbers.</returns>
		/// <param name="values">The list of integer values to calculate the greatest common divider from.</param>
		/// <remarks>
		/// <para>All given numbers must be strictly larger than zero (<c>0</c>).</para>
		/// </remarks>
		public static int GreatestCommonDivider (this IEnumerable<int> values) {
			int r = 0x00;
			IEnumerator<int> enr = values.GetEnumerator ();
			if (enr.MoveNext ()) {
				r = enr.Current;
				while (enr.MoveNext ()) {
					r = GreatestCommonDivider (r, enr.Current);
				}
			}
			return r;
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
			return (a * b) / GreatestCommonDivider (a, b);
		}

		/// <summary>
		/// Calculate the least common multiple of a given list of integers.
		/// </summary>
		/// <returns>The least common multiple of the given <paramref name="values"/>.</returns>
		/// <param name="values">The given list of numbers to calculate the least common multiple from.</param>
		/// <remarks>
		/// <para>All numbers must be strictly larger than zero.</para>
		/// <para>If no values are given, one (<c>1</c>) is returned.</para>
		/// </remarks>
		public static int LeastCommonMultiple (this IEnumerable<int> values) {
			int result = 0x01;
			foreach (int element in values) {
				result = LeastCommonMultiple (result, element);
			}
			return result;
		}

		/// <summary>
		/// Calculate the least common multiple of a given list of integers.
		/// </summary>
		/// <returns>The least common multiple of the given <paramref name="values"/>.</returns>
		/// <param name="values">The given list of numbers to calculate the least common multiple from.</param>
		/// <remarks>
		/// <para>All numbers must be strictly larger than zero.</para>
		/// <para>If no values are given, one (<c>1</c>) is returned.</para>
		/// </remarks>
		public static int LeastCommonMultiple (params int[] values) {
			return LeastCommonMultiple ((IEnumerable<int>)values);
		}
		#endregion
		#region Sums and products
		/// <summary>
		/// Calculate the sum of the powers of <c>a</c> to infinity, or more formally <c>a+a^2+a^3+a^4+...</c>
		/// with <c>a</c> the given <paramref name="factor"/>.
		/// </summary>
		/// <returns>The sum of the power list.</returns>
		/// <param name="factor">The factor of the power sum.</param>
		/// <remarks><para>The absolute value of the <paramref name="factor"/> must be smaller than one. Otherwise the
		/// results are invalid.</para></remarks>
		public static double PowerSum (double factor) {
			return 1.0d / (1.0d - factor);
		}

		/// <summary>
		/// Calculate the sum of the powers of <c>a</c> from one (<c>1</c>) to <paramref name="n"/>, or more
		/// formally <c>a+a^2+a^3+...a^n</c>.
		/// </summary>
		/// <returns>The sum of the power list from one to <paramref name="n"/>.</returns>
		/// <param name="factor">The factor of power sum.</param>
		/// <param name="n">The length of the power sum.</param>
		/// <remarks><para>The absolute value of the <paramref name="factor"/> must be smaller than one. Otherwise the
		/// results are invalid.</para></remarks>
		public static double PowerSum (double factor, int n) {
			return factor * (1.0d - Math.Pow (factor, n)) / (1.0d - factor);
		}

		/// <summary>
		/// Calculate the sum of the powers of <c>a</c> from <paramref name="initial"/> to <paramref name="n"/>, or more
		/// formally <c>a^i+a^(i+1)+a^(i+2)+...a^n</c>.
		/// </summary>
		/// <returns>The sum of the power list from <paramref name="initial"/> to <paramref name="n"/>.</returns>
		/// <param name="factor">The factor of power sum.</param>
		/// <param name="initial">The offset index of the sum.</param>
		/// <param name="n">The end index of the sum.</param>
		/// <remarks><para>The absolute value of the <paramref name="factor"/> must be smaller than one. Otherwise the
		/// results are invalid.</para></remarks>
		public static double PowerSum (double factor, int initial, int n) {
			return (Math.Pow (factor, initial) - Math.Pow (factor, n + 0x01)) / (1.0d - factor);
		}
		#endregion
		#region Min/Max
		/// <summary>
		/// Compares the two given items and returns the minimum of the two.
		/// </summary>
		/// <returns>The minimum of the two given elements (<paramref name="x1"/> and <paramref name="x2"/>) according to their built-in comparison function.</returns>
		/// <param name="x1">The first element to compare.</param>
		/// <param name="x2">The second element to compare.</param>
		/// <typeparam name='T'>The type of elements to calculate the minimum from.</typeparam>
		public static T Minimum<T> (T x1, T x2) where T : IComparable<T> {
			if (x1.CompareTo (x2) <= 0x00) {
				return x1;
			} else {
				return x2;
			}
		}

		/// <summary>
		/// Compares the two given <see cref="T:Tuple`2"/> instance and returns a <see cref="T:Tuple`2"/> with the element-wise minimum of the given ones.
		/// </summary>
		/// <returns>A <see cref="T:Tuple`2"/> where the elements are the minimums of the given ones according to their built-in comparison function.</returns>
		/// <param name="t1">The first tuple to compare.</param>
		/// <param name="t2">The second tuple to compare.</param>
		/// <typeparam name='T1'>The type of the first element of the tuples to calculate the minimum from.</typeparam>
		/// <typeparam name='T2'>The type of the second element of the tuples to calculate the minimum from.</typeparam>
		public static Tuple<T1,T2> Minimum<T1,T2> (Tuple<T1,T2> t1, Tuple<T1,T2> t2)
				where T1 : IComparable<T1>
				where T2 : IComparable<T2> {
			return new Tuple<T1,T2> (Minimum (t1.Item1, t2.Item1),
			                         Minimum (t1.Item2, t2.Item2));
		}

		/// <summary>
		/// Compares the two given <see cref="T:Tuple`3"/> instance and returns a <see cref="T:Tuple`3"/> with the element-wise minimum of the given ones.
		/// </summary>
		/// <returns>A <see cref="T:Tuple`3"/> where the elements are the minimums of the given ones according to their built-in comparison function.</returns>
		/// <param name="t1">The first tuple to compare.</param>
		/// <param name="t2">The second tuple to compare.</param>
		/// <typeparam name='T1'>The type of the first element of the tuples to calculate the minimum from.</typeparam>
		/// <typeparam name='T2'>The type of the second element of the tuples to calculate the minimum from.</typeparam>
		/// <typeparam name='T3'>The type of the third element of the tuples to calculate the minimum from.</typeparam>
		public static Tuple<T1,T2,T3> Minimum<T1,T2,T3> (Tuple<T1,T2,T3> t1, Tuple<T1,T2,T3> t2)
			where T1 : IComparable<T1>
			where T2 : IComparable<T2>
			where T3 : IComparable<T3> {
			return new Tuple<T1,T2,T3> (Minimum (t1.Item1, t2.Item1),
			                            Minimum (t1.Item2, t2.Item2),
			                            Minimum (t1.Item3, t2.Item3));
		}

		/// <summary>
		/// Compares the two given items and returns the maximum of the two.
		/// </summary>
		/// <returns>The maximum of the two given elements (<paramref name="x1"/> and <paramref name="x2"/>) according to their built-in comparison function.</returns>
		/// <param name="x1">The first element to compare.</param>
		/// <param name="x2">The second element to compare.</param>
		public static X Maximum<X> (X x1, X x2) where X : IComparable<X> {
			if (x1.CompareTo (x2) >= 0x00) {
				return x1;
			} else {
				return x2;
			}
		}
		#endregion
	}
}