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
		/// <summary>
		/// The random generator used for static random numbers
		/// </summary>
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
		#region Constants
		/// <summary>
		/// The square root of 2*pi. This is a constant that is very popular in probability theory and statistics. The constant
		/// can furthermore be used to approximate a lot of formulas.
		/// </summary>
		public static readonly double Sqrt2Pi = Math.Sqrt (2.0d * Math.PI);
		/// <summary>
		/// The natural logarithm of the square root of 2*pi. This is a modification of the <see cref="P:Sqrt2Pi"/> constant
		/// to use in log-space.
		/// </summary>
		public static readonly double LogSqrt2Pi = Math.Log (Sqrt2Pi);
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
		#region Functions
		/// <summary>
		/// Calculate the factorial for the given <paramref name="n"/>.
		/// </summary>
		/// <param name="n">The value to calculate the factorial from.</param>
		/// <returns>The factorial of the given value.</returns>
		/// <remarks>
		/// <para>This function will only work for values up to 20, due to <see cref="ulong"/> overflow.</para>
		/// <para>If <paramref name="n"/> is less than or equal to zero, one is returned.</para>
		/// </remarks>
		public static ulong Factorial (int n) {
			ulong r = 0x01;
			for (uint i = 0x02; i <= n; r*= i, i++)
				;
			return r;
		}

		/// <summary>
		/// Calculate the factorial for the given <paramref name="n"/>, divided by the factorial for <paramref name="k"/>,
		/// or in mathematical form: n!/k!.
		/// </summary>
		/// <param name="k">The value to calculate the factorial of the divider.</param>
		/// <param name="n">The value to calculate the factorial of the numerator.</param>
		/// <returns>The factorial of the given <paramref name="n"/> divided by the factorial of the given <paramref name="k"/>.</returns>
		/// <remarks>
		/// <para>This function will only work for small number (or a small difference between the numbers), due to <see cref="ulong"/> overflow.</para>
		/// <para>If <paramref name="n"/> is less or equal to <paramref name="k"/> or equal to zero, one is returned.</para>
		/// <para>If <paramref name="k"/> is strictly negative, the result is one.</para>
		/// <para>If <paramref name="k"/> is zero, the factorial of <paramref name="n"/> is returned.</para>
		/// </remarks>
		public static ulong Factorial (int k, int n) {
			ulong r = 0x01;
			for (uint i = (uint)k+0x01; i <= n; r*= i, i++)
				;
			return r;
		}

		/// <summary>
		/// Calculate the binomium for the given <paramref name="n"/>and <paramref name="k"/>,
		/// or the number of ways you can pick <paramref name="k"/> elements out of a collection of <paramref name="n"/>.
		/// </summary>
		/// <param name="n">The number of available items.</param>
		/// <param name="k">The number of items to pick.</param>
		/// <returns>The number of ways to pick <paramref name="k"/> items out of a collection of <paramref name="n"/> items.</returns>
		/// <remarks>
		/// <para>This function will only work for small number due to <see cref="ulong"/> overflow.</para>
		/// <para>If <paramref name="n"/> is less or equal to <paramref name="k"/> or equal to zero, one is returned.</para>
		/// <para>If <paramref name="k"/> is less than or equal to zero, one is returned.</para>
		/// <para>If <paramref name="k"/> is one, the result is always <paramref name="n"/>, this is a special property of the binomial operator.</para>
		/// </remarks>
		public static ulong Binomial (int n, int k) {
			ulong r = 0x01;
			uint l = (uint)(n - k);
			if (k > (n >> 0x01)) {
				l = (uint)k;
				k = (n - k);
			}
			uint div = 0x02;
			for (uint i = (uint)n; i > l; r*= i, i--) {
				for (; div <= k && (r%div) == 0x00; r /= div, div++)
					;
			}
			return r;
		}

		/// <summary>
		/// Calculate the multiset binomium for the given <paramref name="n"/>and <paramref name="k"/>,
		/// or the number of ways you can pick <paramref name="k"/> elements with repitition out of a collection of <paramref name="n"/>.
		/// </summary>
		/// <param name="n">The number of available items.</param>
		/// <param name="k">The number of items in a resulting multiset.</param>
		/// <returns>The number of ways to pick <paramref name="k"/> items with repitition out of a collection of <paramref name="n"/> items.</returns>
		/// <remarks>
		/// <para>This function will only work for small number due to <see cref="ulong"/> overflow.</para>
		/// </remarks>
		public static ulong MultisetBinomial (int n, int k) {
			return Binomial (n + k - 0x01, k);
		}
		#endregion
		#region Approximations
		/// <summary>
		/// Approximate the natural logarithm of the factorial of <paramref name="n"/> using the Stirling approximation.
		/// </summary>
		/// <returns>An approximation for the natural logarithm of the factorial for .</returns>
		/// <param name="n">The value to calculate the logarithm of the factorial from.</param>
		/// <remarks>
		/// <para>If <paramref name="n"/> is less than or equal to zero (<c>0</c>), the result is <see cref="double.NaN"/>.</para>
		/// <para>The Stirling approximation is less accurate than the <see cref="M:LogFactorialGospor"/> approximation, but less computationally expensive as well.</para>
		/// <para>Especially if <paramref name="n"/> is low, the relative error is significant op to 100%.</para>
		/// <para>In the table below, we list the result of this operation for values from one to ten with absolute and relative error, please note that the values are logarithmic:
		/// the error on the factorial can blow up.
		/// <list type="table">
		/// <listheader><term>n</term><description>result</description><description>approximation</description><description>absolute difference</description><description>relative difference</description></listheader>
		/// <item><term>1</term><description>0</description><description>-0.0810614667953273</description><description>0.0810614667953273</description><description>Infinity</description></item>
		/// <item><term>2</term><description>0.693147180559945</description><description>0.651806484604536</description><description>0.0413406959554093</description><description>0.0596420170417675</description></item>
		/// <item><term>3</term><description>1.79175946922806</description><description>1.76408154354306</description><description>0.0276779256849982</description><description>0.0154473444456932</description></item>
		/// <item><term>4</term><description>3.17805383034795</description><description>3.15726315824418</description><description>0.0207906721037654</description><description>0.00654195089624682</description></item>
		/// <item><term>5</term><description>4.78749174278205</description><description>4.77084705159222</description><description>0.0166446911898213</description><description>0.00347670389508576</description></item>
		/// <item><term>6</term><description>6.5792512120101</description><description>6.56537508318703</description><description>0.0138761288230711</description><description>0.00210907417514943</description></item>
		/// <item><term>7</term><description>8.52516136106541</description><description>8.51326465111952</description><description>0.0118967099458924</description><description>0.00139548208438903</description></item>
		/// <item><term>8</term><description>10.6046029027453</description><description>10.5941916374833</description><description>0.0104112652619754</description><description>0.000981768516695731</description></item>
		/// <item><term>9</term><description>12.8018274800815</description><description>12.7925720178988</description><description>0.00925546218271123</description><description>0.000722979761843528</description></item>
		/// <item><term>10</term><description>15.1044125730755</description><description>15.0960820096422</description><description>0.00833056343336125</description><description>0.000551531772126707</description></item>
		/// <item><term>11</term><description>17.5023078458739</description><description>17.4947341703859</description><description>0.00757367548795074</description><description>0.000432724390100144</description></item>
		/// <item><term>12</term><description>19.9872144956619</description><description>19.9802716555547</description><description>0.00694284010720381</description><description>0.000347364066599211</description></item>
		/// <item><term>13</term><description>22.5521638531234</description><description>22.5457548589354</description><description>0.00640899418800345</description><description>0.000284185332713243</description></item>
		/// <item><term>14</term><description>25.1912211827387</description><description>25.1852698126259</description><description>0.00595137011276137</description><description>0.000236247781304041</description></item>
		/// <item><term>15</term><description>27.8992713838409</description><description>27.8937166502889</description><description>0.00555473355196057</description><description>0.000199099592083894</description></item>
		/// <item><term>16</term><description>30.6718601060807</description><description>30.6666524501611</description><description>0.00520765591960881</description><description>0.000169786113447238</description></item>
		/// <item><term>17</term><description>33.5050734501369</description><description>33.5001720541885</description><description>0.00490139594843697</description><description>0.000146288172020615</description></item>
		/// <item><term>18</term><description>36.3954452080331</description><description>36.3908160542837</description><description>0.00462915374933459</description><description>0.000127190469106086</description></item>
		/// <item><term>19</term><description>39.3398841871995</description><description>39.3354986269503</description><description>0.00438556024923997</description><description>0.000111478728009752</description></item>
		/// <item><term>20</term><description>42.3356164607535</description><description>42.3314501410615</description><description>0.00416631969199699</description><description>9.84116930447748E-05</description></item>
		/// </list>
		/// </para>
		/// </remarks>
		public static double LogFactorialStirling (int n) {
			return n * (Math.Log (n) - 1.0d) + Math.Log (Math.Sqrt (n)) + LogSqrt2Pi;
		}

		/// <summary>
		/// Approximate the natural logarithm of the factorial of <paramref name="n"/> over the factorial of <paramref name="n"/> minus <paramref name="k"/> using the Stirling approximation.
		/// </summary>
		/// <returns>An approximation for the natural logarithm of the factorial for the devision between the factorials of <paramref name="n"/> and <paramref name="n"/>-<paramref name="k"/>.</returns>
		/// <param name="n">The value of the factorial numerator.</param>
		/// <param name="k">The value to substract from <paramref name="n"/> for the factorial divider.</param>
		/// <remarks>
		/// <para>If <paramref name="n"/> is less than or equal to zero (<c>0</c>), the result is <see cref="double.NaN"/>.</para>
		/// <para>If <paramref name="k"/> is negative, the approximate still works and in that case the divider is greater than the numerator.</para>
		/// <para>If <paramref name="k"/> is greater than or equal to <paramref name="n"/>, the result is <see cref="double.NaN"/>.</para>
		/// <para>The Stirling approximation is less accurate than the <see cref="M:LogFactorialGosperDiv"/> approximation, but less computationally expensive as well.</para>
		/// <para>In the table below, we list the result of this operation for values from one to ten with absolute and relative error, please note that the values are logarithmic:
		/// the error on the factorial can blow up.
		/// <list type="table">
		/// <listheader><term>(n,k)</term><description>result</description><description>approximation</description><description>absolute difference</description><description>relative difference</description></listheader>
		/// <item><term>(1, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(2, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(2, 1)</term><description>0.693147180559945</description><description>0.732867951399863</description><description>-0.039720770839918</description><description>-0.0573049591110366</description></item>
		/// <item><term>(3, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(3, 1)</term><description>1.09861228866811</description><description>1.11227505893852</description><description>-0.0136627702704109</description><description>-0.0124363894445189</description></item>
		/// <item><term>(3, 2)</term><description>1.79175946922806</description><description>1.84514301033838</description><description>-0.0533835411103292</description><description>-0.0297939215766101</description></item>
		/// <item><term>(4, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(4, 1)</term><description>1.38629436111989</description><description>1.39318161470112</description><description>-0.00688725358123299</description><description>-0.00496810329349479</description></item>
		/// <item><term>(4, 2)</term><description>2.484906649788</description><description>2.50545667363964</description><description>-0.0205500238516438</description><description>-0.0082699379686545</description></item>
		/// <item><term>(4, 3)</term><description>3.17805383034795</description><description>3.23832462503951</description><description>-0.0602707946915619</description><description>-0.0189646865374094</description></item>
		/// <item><term>(5, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(5, 1)</term><description>1.6094379124341</description><description>1.61358389334804</description><description>-0.00414598091394391</description><description>-0.00257604277985074</description></item>
		/// <item><term>(5, 2)</term><description>2.99573227355399</description><description>3.00676550804917</description><description>-0.0110332344951778</description><description>-0.00368298415468499</description></item>
		/// <item><term>(5, 3)</term><description>4.0943445622221</description><description>4.11904056698769</description><description>-0.0246960047655884</description><description>-0.00603173582249397</description></item>
		/// <item><term>(5, 4)</term><description>4.78749174278205</description><description>4.85190851838755</description><description>-0.0644167756055056</description><description>-0.0134552243776973</description></item>
		/// <item><term>(6, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(6, 1)</term><description>1.79175946922806</description><description>1.79452803159481</description><description>-0.00276856236675016</description><description>-0.00154516407715314</description></item>
		/// <item><term>(6, 2)</term><description>3.40119738166216</description><description>3.40811192494285</description><description>-0.0069145432806943</description><description>-0.0020329732458265</description></item>
		/// <item><term>(6, 3)</term><description>4.78749174278205</description><description>4.80129353964397</description><description>-0.0138017968619275</description><description>-0.00288288682330075</description></item>
		/// <item><term>(6, 4)</term><description>5.88610403145016</description><description>5.9135685985825</description><description>-0.0274645671323395</description><description>-0.00466600097205095</description></item>
		/// <item><term>(6, 5)</term><description>6.5792512120101</description><description>6.64643654998236</description><description>-0.0671853379722567</description><description>-0.0102116997523386</description></item>
		/// <item><term>(7, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(7, 1)</term><description>1.94591014905531</description><description>1.94788956793249</description><description>-0.00197941887717934</description><description>-0.00101722007983786</description></item>
		/// <item><term>(7, 2)</term><description>3.73766961828337</description><description>3.7424175995273</description><description>-0.00474798124392883</description><description>-0.0012703052245986</description></item>
		/// <item><term>(7, 3)</term><description>5.34710753071747</description><description>5.35600149287534</description><description>-0.00889396215787297</description><description>-0.00166332210578896</description></item>
		/// <item><term>(7, 4)</term><description>6.73340189183736</description><description>6.74918310757647</description><description>-0.0157812157391071</description><description>-0.00234372104808389</description></item>
		/// <item><term>(7, 5)</term><description>7.83201418050547</description><description>7.86145816651499</description><description>-0.0294439860095181</description><description>-0.00375943982364162</description></item>
		/// <item><term>(7, 6)</term><description>8.52516136106541</description><description>8.59432611791485</description><description>-0.0691647568494353</description><description>-0.00811301439586964</description></item>
		/// <item><term>(8, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(8, 1)</term><description>2.07944154167984</description><description>2.08092698636375</description><description>-0.0014854446839192</description><description>-0.000714347893001702</description></item>
		/// <item><term>(8, 2)</term><description>4.02535169073515</description><description>4.02881655429625</description><description>-0.00346486356109743</description><description>-0.000860760457048324</description></item>
		/// <item><term>(8, 3)</term><description>5.8171111599632</description><description>5.82334458589105</description><description>-0.00623342592784937</description><description>-0.00107156727049527</description></item>
		/// <item><term>(8, 4)</term><description>7.4265490723973</description><description>7.4369284792391</description><description>-0.0103794068417926</description><description>-0.00139760832933433</description></item>
		/// <item><term>(8, 5)</term><description>8.81284343351719</description><description>8.83011009394022</description><description>-0.0172666604230276</description><description>-0.0019592609982561</description></item>
		/// <item><term>(8, 6)</term><description>9.91145572218531</description><description>9.94238515287874</description><description>-0.0309294306934369</description><description>-0.00312057396616382</description></item>
		/// <item><term>(8, 7)</term><description>10.6046029027453</description><description>10.6752531042786</description><description>-0.0706502015333523</description><description>-0.00666222037555625</description></item>
		/// <item><term>(9, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(9, 1)</term><description>2.19722457733622</description><description>2.19838038041548</description><description>-0.00115580307925933</description><description>-0.000526028650498966</description></item>
		/// <item><term>(9, 2)</term><description>4.27666611901606</description><description>4.27930736677924</description><description>-0.00264124776318031</description><description>-0.000617595035402948</description></item>
		/// <item><term>(9, 3)</term><description>6.22257626807137</description><description>6.22719693471173</description><description>-0.00462066664035721</description><description>-0.000742564886519156</description></item>
		/// <item><term>(9, 4)</term><description>8.01433573729942</description><description>8.02172496630653</description><description>-0.00738922900710826</description><description>-0.000922001429602972</description></item>
		/// <item><term>(9, 5)</term><description>9.62377364973352</description><description>9.63530885965458</description><description>-0.0115352099210515</description><description>-0.00119861608771014</description></item>
		/// <item><term>(9, 6)</term><description>11.0100680108534</description><description>11.0284904743557</description><description>-0.0184224635022865</description><description>-0.0016732379385964</description></item>
		/// <item><term>(9, 7)</term><description>12.1086802995215</description><description>12.1407655332942</description><description>-0.0320852337726976</description><description>-0.00264977131933737</description></item>
		/// <item><term>(9, 8)</term><description>12.8018274800815</description><description>12.8736334846941</description><description>-0.0718060046126165</description><description>-0.00560904329669654</description></item>
		/// <item><term>(10, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(10, 1)</term><description>2.30258509299405</description><description>2.3035099917434</description><description>-0.000924898749350422</description><description>-0.000401678423162107</description></item>
		/// <item><term>(10, 2)</term><description>4.49980967033027</description><description>4.50189037215887</description><description>-0.00208070182860887</description><description>-0.00046239774147073</description></item>
		/// <item><term>(10, 3)</term><description>6.5792512120101</description><description>6.58281735852263</description><description>-0.00356614651252851</description><description>-0.000542029236703819</description></item>
		/// <item><term>(10, 4)</term><description>8.52516136106541</description><description>8.53070692645512</description><description>-0.00554556538970807</description><description>-0.000650493891533219</description></item>
		/// <item><term>(10, 5)</term><description>10.3169208302935</description><description>10.3252349580499</description><description>-0.00831412775645823</description><description>-0.000805872982183361</description></item>
		/// <item><term>(10, 6)</term><description>11.9263587427276</description><description>11.938818851398</description><description>-0.0124601086704033</description><description>-0.00104475380450895</description></item>
		/// <item><term>(10, 7)</term><description>13.3126531038475</description><description>13.3320004660991</description><description>-0.0193473622516382</description><description>-0.0014533062719141</description></item>
		/// <item><term>(10, 8)</term><description>14.4112653925156</description><description>14.4442755250376</description><description>-0.033010132522044</description><description>-0.00229057835123817</description></item>
		/// <item><term>(10, 9)</term><description>15.1044125730755</description><description>15.1771434764375</description><description>-0.0727309033619665</description><description>-0.00481520900002517</description></item>
		/// <item><term>(11, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(11, 1)</term><description>2.39789527279837</description><description>2.39865216074378</description><description>-0.000756887945412288</description><description>-0.000315646789915471</description></item>
		/// <item><term>(11, 2)</term><description>4.70048036579242</description><description>4.70216215248718</description><description>-0.00168178669476138</description><description>-0.000357790388191071</description></item>
		/// <item><term>(11, 3)</term><description>6.89770494312864</description><description>6.90054253290266</description><description>-0.00283758977401938</description><description>-0.000411381727315856</description></item>
		/// <item><term>(11, 4)</term><description>8.97714648480847</description><description>8.98146951926641</description><description>-0.00432303445793991</description><description>-0.000481559977355337</description></item>
		/// <item><term>(11, 5)</term><description>10.9230566338638</description><description>10.9293590871989</description><description>-0.00630245333511681</description><description>-0.000576986236213211</description></item>
		/// <item><term>(11, 6)</term><description>12.7148161030918</description><description>12.7238871187937</description><description>-0.00907101570186875</description><description>-0.000713420912132812</description></item>
		/// <item><term>(11, 7)</term><description>14.3242540155259</description><description>14.3374710121418</description><description>-0.0132169966158138</description><description>-0.000922700519097747</description></item>
		/// <item><term>(11, 8)</term><description>15.7105483766458</description><description>15.7306526268429</description><description>-0.0201042501970488</description><description>-0.00127966571981245</description></item>
		/// <item><term>(11, 9)</term><description>16.8091606653139</description><description>16.8429276857814</description><description>-0.0337670204674581</description><description>-0.00200884631539854</description></item>
		/// <item><term>(11, 10)</term><description>17.5023078458739</description><description>17.5757956371813</description><description>-0.0734877913073753</description><description>-0.00419874864243688</description></item>
		/// <item><term>(12, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(12, 1)</term><description>2.484906649788</description><description>2.48553748516874</description><description>-0.000630835380741601</description><description>-0.000253866832701913</description></item>
		/// <item><term>(12, 2)</term><description>4.88280192258637</description><description>4.88418964591252</description><description>-0.00138772332615389</description><description>-0.000284206352859554</description></item>
		/// <item><term>(12, 3)</term><description>7.18538701558042</description><description>7.18769963765592</description><description>-0.00231262207550298</description><description>-0.000321850732672911</description></item>
		/// <item><term>(12, 4)</term><description>9.38261159291664</description><description>9.3860800180714</description><description>-0.00346842515476098</description><description>-0.000369665217451765</description></item>
		/// <item><term>(12, 5)</term><description>11.4620531345965</description><description>11.4670070044352</description><description>-0.00495386983868151</description><description>-0.000432197424013766</description></item>
		/// <item><term>(12, 6)</term><description>13.4079632836518</description><description>13.4148965723676</description><description>-0.00693328871586019</description><description>-0.000517102304741085</description></item>
		/// <item><term>(12, 7)</term><description>15.1997227528798</description><description>15.2094246039625</description><description>-0.00970185108261035</description><description>-0.000638291318884232</description></item>
		/// <item><term>(12, 8)</term><description>16.8091606653139</description><description>16.8230084973105</description><description>-0.0138478319965571</description><description>-0.000823826499863996</description></item>
		/// <item><term>(12, 9)</term><description>18.1954550264338</description><description>18.2161901120116</description><description>-0.0207350855777904</description><description>-0.00113957499538577</description></item>
		/// <item><term>(12, 10)</term><description>19.2940673151019</description><description>19.3284651709501</description><description>-0.0343978558482014</description><description>-0.00178282035023675</description></item>
		/// <item><term>(12, 11)</term><description>19.9872144956619</description><description>20.06133312235</description><description>-0.0741186266881186</description><description>-0.00370830195994573</description></item>
		/// <item><term>(13, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(13, 1)</term><description>2.56494935746154</description><description>2.56548320338074</description><description>-0.000533845919204357</description><description>-0.000208131173292517</description></item>
		/// <item><term>(13, 2)</term><description>5.04985600724954</description><description>5.05102068854949</description><description>-0.00116468129994818</description><description>-0.000230636536621276</description></item>
		/// <item><term>(13, 3)</term><description>7.44775128004791</description><description>7.44967284929327</description><description>-0.00192156924535869</description><description>-0.000258006634902868</description></item>
		/// <item><term>(13, 4)</term><description>9.75033637304195</description><description>9.75318284103666</description><description>-0.00284646799470778</description><description>-0.000291935363643226</description></item>
		/// <item><term>(13, 5)</term><description>11.9475609503782</description><description>11.9515632214521</description><description>-0.00400227107396844</description><description>-0.000334986453770028</description></item>
		/// <item><term>(13, 6)</term><description>14.027002492058</description><description>14.0324902078159</description><description>-0.00548771575788898</description><description>-0.000391225121760411</description></item>
		/// <item><term>(13, 7)</term><description>15.9729126411133</description><description>15.9803797757484</description><description>-0.00746713463506588</description><description>-0.000467487352046609</description></item>
		/// <item><term>(13, 8)</term><description>17.7646721103414</description><description>17.7749078073432</description><description>-0.0102356970018143</description><description>-0.000576182714673114</description></item>
		/// <item><term>(13, 9)</term><description>19.3741100227755</description><description>19.3884917006912</description><description>-0.0143816779157646</description><description>-0.000742314248182655</description></item>
		/// <item><term>(13, 10)</term><description>20.7604043838954</description><description>20.7816733153924</description><description>-0.0212689314969943</description><description>-0.00102449504854026</description></item>
		/// <item><term>(13, 11)</term><description>21.8590166725635</description><description>21.8939483743309</description><description>-0.0349317017674018</description><description>-0.00159804543318029</description></item>
		/// <item><term>(13, 12)</term><description>22.5521638531234</description><description>22.6268163257307</description><description>-0.074652472607319</description><description>-0.0033102132945429</description></item>
		/// <item><term>(14, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(14, 1)</term><description>2.63905732961526</description><description>2.6395149536905</description><description>-0.000457624075245189</description><description>-0.000173404370609829</description></item>
		/// <item><term>(14, 2)</term><description>5.2040066870768</description><description>5.20499815707125</description><description>-0.000991469994451322</description><description>-0.000190520507383947</description></item>
		/// <item><term>(14, 3)</term><description>7.6889133368648</description><description>7.69053564223999</description><description>-0.00162230537519203</description><description>-0.000210992802768868</description></item>
		/// <item><term>(14, 4)</term><description>10.0868086096632</description><description>10.0891878029838</description><description>-0.00237919332060521</description><description>-0.000235871762087955</description></item>
		/// <item><term>(14, 5)</term><description>12.3893937026572</description><description>12.3926977947272</description><description>-0.00330409206995341</description><description>-0.000266687147833939</description></item>
		/// <item><term>(14, 6)</term><description>14.5866182799934</description><description>14.5910781751426</description><description>-0.00445989514921052</description><description>-0.00030575250984168</description></item>
		/// <item><term>(14, 7)</term><description>16.6660598216733</description><description>16.6720051615064</description><description>-0.00594533983312928</description><description>-0.000356733378899655</description></item>
		/// <item><term>(14, 8)</term><description>18.6119699707286</description><description>18.6198947294389</description><description>-0.00792475871031328</description><description>-0.000425788281561635</description></item>
		/// <item><term>(14, 9)</term><description>20.4037294399566</description><description>20.4144227610337</description><description>-0.0106933210770599</description><description>-0.000524086594488905</description></item>
		/// <item><term>(14, 10)</term><description>22.0131673523907</description><description>22.0280066543817</description><description>-0.0148393019910067</description><description>-0.00067411026107495</description></item>
		/// <item><term>(14, 11)</term><description>23.3994617135106</description><description>23.4211882690829</description><description>-0.021726555572247</description><description>-0.000928506639949854</description></item>
		/// <item><term>(14, 12)</term><description>24.4980740021787</description><description>24.5334633280214</description><description>-0.035389325842651</description><description>-0.00144457584051316</description></item>
		/// <item><term>(14, 13)</term><description>25.1912211827387</description><description>25.2663312794212</description><description>-0.0751100966825646</description><description>-0.00298159807885895</description></item>
		/// <item><term>(15, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(15, 1)</term><description>2.70805020110221</description><description>2.70844683766301</description><description>-0.000396636560795471</description><description>-0.000146465734141131</description></item>
		/// <item><term>(15, 2)</term><description>5.34710753071747</description><description>5.34796179135351</description><description>-0.000854260636040216</description><description>-0.000159761259920949</description></item>
		/// <item><term>(15, 3)</term><description>7.91205688817901</description><description>7.91344499473425</description><description>-0.00138810655524679</description><description>-0.00017544193309842</description></item>
		/// <item><term>(15, 4)</term><description>10.396963537967</description><description>10.398982479903</description><description>-0.00201894193598839</description><description>-0.000194185728228799</description></item>
		/// <item><term>(15, 5)</term><description>12.7948588107654</description><description>12.7976346406468</description><description>-0.00277582988140068</description><description>-0.000216948848162759</description></item>
		/// <item><term>(15, 6)</term><description>15.0974439037594</description><description>15.1011446323902</description><description>-0.00370072863075244</description><description>-0.000245122860157203</description></item>
		/// <item><term>(15, 7)</term><description>17.2946684810956</description><description>17.2995250128056</description><description>-0.00485653171000777</description><description>-0.000280810916688939</description></item>
		/// <item><term>(15, 8)</term><description>19.3741100227755</description><description>19.3804519991694</description><description>-0.00634197639393008</description><description>-0.000327342850147681</description></item>
		/// <item><term>(15, 9)</term><description>21.3200201718308</description><description>21.3283415671019</description><description>-0.00832139527110698</description><description>-0.000390308977385569</description></item>
		/// <item><term>(15, 10)</term><description>23.1117796410588</description><description>23.1228695986967</description><description>-0.0110899576378607</description><description>-0.000479840056027491</description></item>
		/// <item><term>(15, 11)</term><description>24.7212175534929</description><description>24.7364534920447</description><description>-0.0152359385518039</description><description>-0.000616310200694432</description></item>
		/// <item><term>(15, 12)</term><description>26.1075119146128</description><description>26.1296351067459</description><description>-0.0221231921330336</description><description>-0.000847387993363353</description></item>
		/// <item><term>(15, 13)</term><description>27.2061242032809</description><description>27.2419101656844</description><description>-0.0357859624034447</description><description>-0.0013153642222632</description></item>
		/// <item><term>(15, 14)</term><description>27.8992713838409</description><description>27.9747781170843</description><description>-0.0755067332433654</description><description>-0.00270640520336666</description></item>
		/// <item><term>(16, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(16, 1)</term><description>2.77258872223978</description><description>2.77293579987213</description><description>-0.000347077632353088</description><description>-0.000125181794749821</description></item>
		/// <item><term>(16, 2)</term><description>5.48063892334199</description><description>5.48138263753514</description><description>-0.000743714193148115</description><description>-0.000135698447489515</description></item>
		/// <item><term>(16, 3)</term><description>8.11969625295725</description><description>8.12089759122565</description><description>-0.00120133826839641</description><description>-0.00014795359715074</description></item>
		/// <item><term>(16, 4)</term><description>10.6846456104188</description><description>10.6863807946064</description><description>-0.00173518418759855</description><description>-0.000162399788525184</description></item>
		/// <item><term>(16, 5)</term><description>13.1695522602068</description><description>13.1719182797751</description><description>-0.0023660195683437</description><description>-0.000179658314997761</description></item>
		/// <item><term>(16, 6)</term><description>15.5674475330052</description><description>15.5705704405189</description><description>-0.00312290751375599</description><description>-0.000200604980818788</description></item>
		/// <item><term>(16, 7)</term><description>17.8700326259992</description><description>17.8740804322623</description><description>-0.00404780626310242</description><description>-0.000226513647054748</description></item>
		/// <item><term>(16, 8)</term><description>20.0672572033354</description><description>20.0724608126778</description><description>-0.00520360934235953</description><description>-0.000259308449063713</description></item>
		/// <item><term>(16, 9)</term><description>22.1466987450153</description><description>22.1533877990415</description><description>-0.00668905402628184</description><description>-0.000302033910484622</description></item>
		/// <item><term>(16, 10)</term><description>24.0926088940706</description><description>24.101277366974</description><description>-0.00866847290345873</description><description>-0.00035979801695914</description></item>
		/// <item><term>(16, 11)</term><description>25.8843683632986</description><description>25.8958053985688</description><description>-0.0114370352702089</description><description>-0.000441851047307202</description></item>
		/// <item><term>(16, 12)</term><description>27.4938062757327</description><description>27.5093892919169</description><description>-0.0155830161841521</description><description>-0.000566782788380466</description></item>
		/// <item><term>(16, 13)</term><description>28.8801006368526</description><description>28.902570906618</description><description>-0.0224702697653925</description><description>-0.000778053721070457</description></item>
		/// <item><term>(16, 14)</term><description>29.9787129255207</description><description>30.0148459655565</description><description>-0.0361330400357964</description><description>-0.00120528990439201</description></item>
		/// <item><term>(16, 15)</term><description>30.6718601060807</description><description>30.7477139169564</description><description>-0.0758538108757136</description><description>-0.00247307501447151</description></item>
		/// <item><term>(17, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(17, 1)</term><description>2.83321334405622</description><description>2.83351960402739</description><description>-0.000306259971174505</description><description>-0.000108096332320687</description></item>
		/// <item><term>(17, 2)</term><description>5.605802066296</description><description>5.60645540389952</description><description>-0.000653337603527149</description><description>-0.000116546677139252</description></item>
		/// <item><term>(17, 3)</term><description>8.31385226739821</description><description>8.31490224156253</description><description>-0.00104997416432084</description><description>-0.000126292136370789</description></item>
		/// <item><term>(17, 4)</term><description>10.9529095970135</description><description>10.954417195253</description><description>-0.00150759823957003</description><description>-0.000137643630326421</description></item>
		/// <item><term>(17, 5)</term><description>13.517858954475</description><description>13.5199003986338</description><description>-0.00204144415877572</description><description>-0.000151018305905604</description></item>
		/// <item><term>(17, 6)</term><description>16.002765604263</description><description>16.0054378838025</description><description>-0.00267227953951732</description><description>-0.000166988607194587</description></item>
		/// <item><term>(17, 7)</term><description>18.4006608770614</description><description>18.4040900445463</description><description>-0.00342916748492783</description><description>-0.000186361104518952</description></item>
		/// <item><term>(17, 8)</term><description>20.7032459700554</description><description>20.7076000362897</description><description>-0.00435406623427781</description><description>-0.000210308385485803</description></item>
		/// <item><term>(17, 9)</term><description>22.9004705473916</description><description>22.9059804167052</description><description>-0.00550986931353847</description><description>-0.000240600703035163</description></item>
		/// <item><term>(17, 10)</term><description>24.9799120890715</description><description>24.9869074030689</description><description>-0.00699531399745368</description><description>-0.00028003757469243</description></item>
		/// <item><term>(17, 11)</term><description>26.9258222381268</description><description>26.9347969710014</description><description>-0.00897473287463768</description><description>-0.000333313233492625</description></item>
		/// <item><term>(17, 12)</term><description>28.7175817073548</description><description>28.7293250025962</description><description>-0.0117432952413843</description><description>-0.000408923542415715</description></item>
		/// <item><term>(17, 13)</term><description>30.3270196197889</description><description>30.3429088959443</description><description>-0.0158892761553275</description><description>-0.000523931344211598</description></item>
		/// <item><term>(17, 14)</term><description>31.7133139809088</description><description>31.7360905106454</description><description>-0.0227765297365679</description><description>-0.000718200871415682</description></item>
		/// <item><term>(17, 15)</term><description>32.8119262695769</description><description>32.8483655695839</description><description>-0.0364393000069754</description><description>-0.00111055046593719</description></item>
		/// <item><term>(17, 16)</term><description>33.5050734501369</description><description>33.5812335209838</description><description>-0.076160070846889</description><description>-0.00227309069953935</description></item>
		/// <item><term>(18, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(18, 1)</term><description>2.89037175789616</description><description>2.89064400009527</description><description>-0.000272242199101491</description><description>-9.41893368414484E-05</description></item>
		/// <item><term>(18, 2)</term><description>5.72358510195238</description><description>5.72416360412266</description><description>-0.000578502170275996</description><description>-0.00010107339368094</description></item>
		/// <item><term>(18, 3)</term><description>8.49617382419216</description><description>8.49709940399479</description><description>-0.000925579802627752</description><description>-0.00010894077990639</description></item>
		/// <item><term>(18, 4)</term><description>11.2042240252944</description><description>11.2055462416578</description><description>-0.001322216363425</description><description>-0.000118010525355437</description></item>
		/// <item><term>(18, 5)</term><description>13.8432813549096</description><description>13.8450611953483</description><description>-0.00177984043867063</description><description>-0.00012857070466457</description></item>
		/// <item><term>(18, 6)</term><description>16.4082307123712</description><description>16.410544398729</description><description>-0.00231368635787632</description><description>-0.000141007668555751</description></item>
		/// <item><term>(18, 7)</term><description>18.8931373621592</description><description>18.8960818838978</description><description>-0.0029445217386197</description><description>-0.000155851391019749</description></item>
		/// <item><term>(18, 8)</term><description>21.2910326349575</description><description>21.2947340446416</description><description>-0.00370140968403376</description><description>-0.000173848293199103</description></item>
		/// <item><term>(18, 9)</term><description>23.5936177279516</description><description>23.598244036385</description><description>-0.00462630843337664</description><description>-0.000196083046132251</description></item>
		/// <item><term>(18, 10)</term><description>25.7908423052878</description><description>25.7966244168004</description><description>-0.0057821115126373</description><description>-0.000224192426295896</description></item>
		/// <item><term>(18, 11)</term><description>27.8702838469676</description><description>27.8775514031642</description><description>-0.00726755619655961</description><description>-0.000260763623236307</description></item>
		/// <item><term>(18, 12)</term><description>29.816193996023</description><description>29.8254409710967</description><description>-0.00924697507373651</description><description>-0.000310132643856889</description></item>
		/// <item><term>(18, 13)</term><description>31.607953465251</description><description>31.6199690026915</description><description>-0.0120155374404831</description><description>-0.000380142847707388</description></item>
		/// <item><term>(18, 14)</term><description>33.2173913776851</description><description>33.2335528960395</description><description>-0.0161615183544299</description><description>-0.000486537855145572</description></item>
		/// <item><term>(18, 15)</term><description>34.603685738805</description><description>34.6267345107407</description><description>-0.0230487719356631</description><description>-0.000666078524398803</description></item>
		/// <item><term>(18, 16)</term><description>35.7022980274731</description><description>35.7390095696792</description><description>-0.0367115422060849</description><description>-0.00102826832541242</description></item>
		/// <item><term>(18, 17)</term><description>36.3954452080331</description><description>36.471877521079</description><description>-0.0764323130459914</description><description>-0.0021000516028616</description></item>
		/// <item><term>(19, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(19, 1)</term><description>2.94443897916644</description><description>2.94468257266654</description><description>-0.000243593500102168</description><description>-8.27300215170799E-05</description></item>
		/// <item><term>(19, 2)</term><description>5.8348107370626</description><description>5.83532657276181</description><description>-0.000515835699203215</description><description>-8.84065863399196E-05</description></item>
		/// <item><term>(19, 3)</term><description>8.66802408111882</description><description>8.6688461767892</description><description>-0.00082209567037772</description><description>-9.48423380789234E-05</description></item>
		/// <item><term>(19, 4)</term><description>11.4406128033586</description><description>11.4417819766613</description><description>-0.00116917330272948</description><description>-0.000102194989274197</description></item>
		/// <item><term>(19, 5)</term><description>14.1486630044608</description><description>14.1502288143243</description><description>-0.00156580986352672</description><description>-0.000110668397645279</description></item>
		/// <item><term>(19, 6)</term><description>16.7877203340761</description><description>16.7897437680148</description><description>-0.00202343393877413</description><description>-0.00012053059608498</description></item>
		/// <item><term>(19, 7)</term><description>19.3526696915376</description><description>19.3552269713956</description><description>-0.0025572798579816</description><description>-0.000132140934493386</description></item>
		/// <item><term>(19, 8)</term><description>21.8375763413256</description><description>21.8407644565643</description><description>-0.00318811523872142</description><description>-0.000145992173714269</description></item>
		/// <item><term>(19, 9)</term><description>24.235471614124</description><description>24.2394166173081</description><description>-0.00394500318413193</description><description>-0.000162778065430048</description></item>
		/// <item><term>(19, 10)</term><description>26.538056707118</description><description>26.5429266090515</description><description>-0.00486990193348547</description><description>-0.000183506350417107</description></item>
		/// <item><term>(19, 11)</term><description>28.7352812844542</description><description>28.741306989467</description><description>-0.00602570501273547</description><description>-0.00020969709511754</description></item>
		/// <item><term>(19, 12)</term><description>30.8147228261341</description><description>30.8222339758307</description><description>-0.00751114969665778</description><description>-0.00024375197982594</description></item>
		/// <item><term>(19, 13)</term><description>32.7606329751894</description><description>32.7701235437632</description><description>-0.00949056857383113</description><description>-0.000289694298062514</description></item>
		/// <item><term>(19, 14)</term><description>34.5523924444175</description><description>34.564651575358</description><description>-0.0122591309405848</description><description>-0.000354798324321693</description></item>
		/// <item><term>(19, 15)</term><description>36.1618303568515</description><description>36.1782354687061</description><description>-0.0164051118545316</description><description>-0.000453658227270108</description></item>
		/// <item><term>(19, 16)</term><description>37.5481247179714</description><description>37.5714170834072</description><description>-0.023292365435772</description><description>-0.000620333654762355</description></item>
		/// <item><term>(19, 17)</term><description>38.6467370066395</description><description>38.6836921423457</description><description>-0.0369551357061866</description><description>-0.000956229130025587</description></item>
		/// <item><term>(19, 18)</term><description>39.3398841871995</description><description>39.4165600937456</description><description>-0.0766759065460931</description><description>-0.00194906284373461</description></item>
		/// <item><term>(20, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(20, 1)</term><description>2.99573227355399</description><description>2.99595151411123</description><description>-0.000219240557234546</description><description>-7.31842959299061E-05</description></item>
		/// <item><term>(20, 2)</term><description>5.94017125272043</description><description>5.94063408677777</description><description>-0.000462834057337602</description><description>-7.7915945121218E-05</description></item>
		/// <item><term>(20, 3)</term><description>8.8305430106166</description><description>8.83127808687303</description><description>-0.000735076256438205</description><description>-8.32424750725356E-05</description></item>
		/// <item><term>(20, 4)</term><description>11.6637563546728</description><description>11.6647976909004</description><description>-0.0010413362276136</description><description>-8.92796622244609E-05</description></item>
		/// <item><term>(20, 5)</term><description>14.4363450769126</description><description>14.4377334907726</description><description>-0.00138841385996535</description><description>-9.6174887242463E-05</description></item>
		/// <item><term>(20, 6)</term><description>17.1443952780148</description><description>17.1461803284356</description><description>-0.00178505042075727</description><description>-0.00010411859921629</description></item>
		/// <item><term>(20, 7)</term><description>19.7834526076301</description><description>19.7856952821261</description><description>-0.00224267449601001</description><description>-0.000113361127629717</description></item>
		/// <item><term>(20, 8)</term><description>22.3484019650916</description><description>22.3511784855068</description><description>-0.00277652041521392</description><description>-0.000124237984422818</description></item>
		/// <item><term>(20, 9)</term><description>24.8333086148796</description><description>24.8367159706756</description><description>-0.0034073557959573</description><description>-0.000137209094800831</description></item>
		/// <item><term>(20, 10)</term><description>27.231203887678</description><description>27.2353681314193</description><description>-0.00416424374136426</description><description>-0.000152921764257678</description></item>
		/// <item><term>(20, 11)</term><description>29.533788980672</description><description>29.5388781231627</description><description>-0.00508914249071779</description><description>-0.000172315935962308</description></item>
		/// <item><term>(20, 12)</term><description>31.7310135580082</description><description>31.7372585035782</description><description>-0.0062449455699749</description><description>-0.000196808890411218</description></item>
		/// <item><term>(20, 13)</term><description>33.8104550996881</description><description>33.818185489942</description><description>-0.00773039025389011</description><description>-0.000228639047628833</description></item>
		/// <item><term>(20, 14)</term><description>35.7563652487434</description><description>35.7660750578745</description><description>-0.00970980913107411</description><description>-0.000271554702597053</description></item>
		/// <item><term>(20, 15)</term><description>37.5481247179714</description><description>37.5606030894693</description><description>-0.0124783714978207</description><description>-0.000332330085498205</description></item>
		/// <item><term>(20, 16)</term><description>39.1575626304055</description><description>39.1741869828173</description><description>-0.0166243524117675</description><description>-0.000424550234872352</description></item>
		/// <item><term>(20, 17)</term><description>40.5438569915254</description><description>40.5673685975184</description><description>-0.0235116059930007</description><description>-0.000579905508198571</description></item>
		/// <item><term>(20, 18)</term><description>41.6424692801935</description><description>41.679643656457</description><description>-0.0371743762634154</description><description>-0.000892703456494994</description></item>
		/// <item><term>(20, 19)</term><description>42.3356164607535</description><description>42.4125116078568</description><description>-0.076895147103329</description><description>-0.0018163228395319</description></item>
		/// 
		/// </list>
		/// </para>
		/// </remarks>
		public static double LogFactorialDivStirling (int n, int k) {
			int nk = n - k;
			return (n + 0.5d) * Math.Log ((double)n / nk) + k * (Math.Log (nk) - 1.0d);
		}

		/// <summary>
		/// Approximate the natural logarithm of the factorial of <paramref name="n"/> using the Gosper approximation.
		/// </summary>
		/// <returns>An approximation for the natural logarithm of the factorial for .</returns>
		/// <param name="n">The value to calculate the logarithm of the factorial from.</param>
		/// <remarks>
		/// <para>If <paramref name="n"/> is less than or equal to zero (<c>0</c>), the result is <see cref="double.NaN"/>.</para>
		/// <para>The Gosper approximation is more accurate than the <see cref="M:LogFactorialStirling"/> approximation, but more computationally expensive as well.</para>
		/// <para>The Gosper approximation is always greater than the real value for log(n!)</para>
		/// <para>In the table below, we list the result of this operation for the first values with absolute and relative error, please note that the values are logarithmic:
		/// the error on the factorial can blow up.
		/// <list type="table">
		/// <listheader><term>n</term><description>result</description><description>approximation</description><description>absolute difference</description><description>relative difference</description></listheader>
		/// <item><term>1</term><description>0</description><description>0.0858383680212085</description><description>-0.0858383680212085</description><description>-Infinity</description></item>
		/// <item><term>2</term><description>0.693147180559945</description><description>0.75194062310881</description><description>-0.0587934425488649</description><description>-0.0848210080020375</description></item>
		/// <item><term>3</term><description>1.79175946922806</description><description>1.83982059996262</description><description>-0.0480611307345604</description><description>-0.0268234277870269</description></item>
		/// <item><term>4</term><description>3.17805383034795</description><description>3.22034331067159</description><description>-0.0422894803236478</description><description>-0.0133067224726706</description></item>
		/// <item><term>5</term><description>4.78749174278205</description><description>4.82617511075166</description><description>-0.0386833679696093</description><description>-0.00808009079659113</description></item>
		/// <item><term>6</term><description>6.5792512120101</description><description>6.61546749241281</description><description>-0.0362162804027122</description><description>-0.00550462039458246</description></item>
		/// <item><term>7</term><description>8.52516136106541</description><description>8.55958345752761</description><description>-0.0344220964621993</description><description>-0.00403770615057279</description></item>
		/// <item><term>8</term><description>10.6046029027453</description><description>10.637661433334</description><description>-0.0330585305887983</description><description>-0.00311737562377185</description></item>
		/// <item><term>9</term><description>12.8018274800815</description><description>12.8338146385748</description><description>-0.0319871584933722</description><description>-0.00249864002175794</description></item>
		/// <item><term>10</term><description>15.1044125730755</description><description>15.1355357196443</description><description>-0.0311231465687793</description><description>-0.00206053339831687</description></item>
		/// <item><term>11</term><description>17.5023078458739</description><description>17.5327194505514</description><description>-0.0304116046775356</description><description>-0.00173757683531461</description></item>
		/// <item><term>12</term><description>19.9872144956619</description><description>20.0170299412394</description><description>-0.0298154455774977</description><description>-0.00149172590227463</description></item>
		/// <item><term>13</term><description>22.5521638531234</description><description>22.5814725615182</description><description>-0.0293087083947867</description><description>-0.0012995962864436</description></item>
		/// <item><term>14</term><description>25.1912211827387</description><description>25.2200938599217</description><description>-0.0288726771829815</description><description>-0.0011461404341432</description></item>
		/// <item><term>15</term><description>27.8992713838409</description><description>27.927764902239</description><description>-0.0284935183980721</description><description>-0.00102129973238568</description></item>
		/// <item><term>16</term><description>30.6718601060807</description><description>30.7000208923531</description><description>-0.0281607862724016</description><description>-0.000918131022214031</description></item>
		/// <item><term>17</term><description>33.5050734501369</description><description>33.5329398957076</description><description>-0.0278664455707585</description><description>-0.000831708237029536</description></item>
		/// <item><term>18</term><description>36.3954452080331</description><description>36.4230494222085</description><description>-0.0276042141754118</description><description>-0.000758452438694695</description></item>
		/// <item><term>19</term><description>39.3398841871995</description><description>39.3672532968968</description><description>-0.0273691096973394</description><description>-0.000695708954482505</description></item>
		/// <item><term>20</term><description>42.3356164607535</description><description>42.3627735906277</description><description>-0.0271571298742472</description><description>-0.000641472408921287</description></item>
		/// </list>
		/// </para>
		/// </remarks>
		public static double LogFactorialGosper (int n) {
			return Math.Log (Math.Sqrt (0x06 * n + 0x02) * Math.PI / 3.0d) + n * (Math.Log (n) - 1.0d);
		}

		/// <summary>
		/// Approximate the natural logarithm of the factorial of <paramref name="n"/> over the factorial of <paramref name="n"/> minus <paramref name="k"/> using the Gosper approximation.
		/// </summary>
		/// <returns>An approximation for the natural logarithm of the factorial for the devision between the factorials of <paramref name="n"/> and <paramref name="n"/>-<paramref name="k"/>.</returns>
		/// <param name="n">The value of the factorial numerator.</param>
		/// <param name="k">The value to substract from <paramref name="n"/> for the factorial divider.</param>
		/// <remarks>
		/// <para>If <paramref name="n"/> is less than or equal to zero (<c>0</c>), the result is <see cref="double.NaN"/>.</para>
		/// <para>If <paramref name="k"/> is negative, the approximate still works and in that case the divider is greater than the numerator.</para>
		/// <para>If <paramref name="k"/> is greater than or equal to <paramref name="n"/>, the result is <see cref="double.NaN"/>.</para>
		/// <para>The Gosper approximation is more accurate than the <see cref="M:LogFactorialStirlingDiv"/> approximation, but more computationally expensive as well.</para>
		/// <para>In the table below, we list the result of this operation for values from one to ten with absolute and relative error, please note that the values are logarithmic:
		/// the error on the factorial can blow up.
		/// <list type="table">
		/// <listheader><term>(n,k)</term><description>result</description><description>approximation</description><description>absolute difference</description><description>relative difference</description></listheader>
		/// <item><term>(1, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(2, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(2, 1)</term><description>0.693147180559945</description><description>0.695813965323002</description><description>-0.00266678476305693</description><description>-0.00384735715278048</description></item>
		/// <item><term>(3, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(3, 1)</term><description>1.09861228866811</description><description>1.09928731573689</description><description>-0.000675027068780532</description><description>-0.000614436117038972</description></item>
		/// <item><term>(3, 2)</term><description>1.79175946922806</description><description>1.79510128105989</description><description>-0.00334181183183757</description><description>-0.00186510069528324</description></item>
		/// <item><term>(4, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(4, 1)</term><description>1.38629436111989</description><description>1.38655900132611</description><description>-0.000264640206222966</description><description>-0.000190897556568853</description></item>
		/// <item><term>(4, 2)</term><description>2.484906649788</description><description>2.485846317063</description><description>-0.000939667275003497</description><description>-0.000378149929730223</description></item>
		/// <item><term>(4, 3)</term><description>3.17805383034795</description><description>3.18166028238601</description><description>-0.00360645203806031</description><description>-0.00113479891486466</description></item>
		/// <item><term>(5, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(5, 1)</term><description>1.6094379124341</description><description>1.60956780749941</description><description>-0.000129895065312136</description><description>-8.07083419053323E-05</description></item>
		/// <item><term>(5, 2)</term><description>2.99573227355399</description><description>2.99612680882553</description><description>-0.000394535271535101</description><description>-0.000131699109101977</description></item>
		/// <item><term>(5, 3)</term><description>4.0943445622221</description><description>4.09541412456242</description><description>-0.00106956234031497</description><description>-0.000261229196532128</description></item>
		/// <item><term>(5, 4)</term><description>4.78749174278205</description><description>4.79122808988542</description><description>-0.00373634710337267</description><description>-0.00078043938331713</description></item>
		/// <item><term>(6, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(6, 1)</term><description>1.79175946922806</description><description>1.79183260727737</description><description>-7.31380493119715E-05</description><description>-4.08191225262405E-05</description></item>
		/// <item><term>(6, 2)</term><description>3.40119738166216</description><description>3.40140041477678</description><description>-0.000203033114623441</description><description>-5.96945992367603E-05</description></item>
		/// <item><term>(6, 3)</term><description>4.78749174278205</description><description>4.78795941610289</description><description>-0.000467673320846629</description><description>-9.76865018204419E-05</description></item>
		/// <item><term>(6, 4)</term><description>5.88610403145016</description><description>5.88724673183978</description><description>-0.00114270038962694</description><description>-0.000194135269020281</description></item>
		/// <item><term>(6, 5)</term><description>6.5792512120101</description><description>6.58306069716279</description><description>-0.00380948515268464</description><description>-0.000579015001848632</description></item>
		/// <item><term>(7, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(7, 1)</term><description>1.94591014905531</description><description>1.94595532954353</description><description>-4.51804882193318E-05</description><description>-2.32181780033707E-05</description></item>
		/// <item><term>(7, 2)</term><description>3.73766961828337</description><description>3.7377879368209</description><description>-0.000118318537530637</description><description>-3.16556971627092E-05</description></item>
		/// <item><term>(7, 3)</term><description>5.34710753071747</description><description>5.34735574432031</description><description>-0.000248213602842107</description><description>-4.64201629415898E-05</description></item>
		/// <item><term>(7, 4)</term><description>6.73340189183736</description><description>6.73391474564643</description><description>-0.000512853809067515</description><description>-7.616563177214E-05</description></item>
		/// <item><term>(7, 5)</term><description>7.83201418050547</description><description>7.83320206138332</description><description>-0.00118788087784694</description><description>-0.000151669908974842</description></item>
		/// <item><term>(7, 6)</term><description>8.52516136106541</description><description>8.52901602670632</description><description>-0.00385466564090287</description><description>-0.000452151634162281</description></item>
		/// <item><term>(8, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(8, 1)</term><description>2.07944154167984</description><description>2.07947138126003</description><description>-2.98395801903162E-05</description><description>-1.43498047875926E-05</description></item>
		/// <item><term>(8, 2)</term><description>4.02535169073515</description><description>4.02542671080356</description><description>-7.50200684080937E-05</description><description>-1.86368978841679E-05</description></item>
		/// <item><term>(8, 3)</term><description>5.8171111599632</description><description>5.81725931808093</description><description>-0.000148158117720953</description><description>-2.54693633397734E-05</description></item>
		/// <item><term>(8, 4)</term><description>7.4265490723973</description><description>7.42682712558034</description><description>-0.000278053183033755</description><description>-3.74404289695212E-05</description></item>
		/// <item><term>(8, 5)</term><description>8.81284343351719</description><description>8.81338612690645</description><description>-0.000542693389256499</description><description>-6.15798287295694E-05</description></item>
		/// <item><term>(8, 6)</term><description>9.91145572218531</description><description>9.91267344264334</description><description>-0.00121772045803681</description><description>-0.000122859899914714</description></item>
		/// <item><term>(8, 7)</term><description>10.6046029027453</description><description>10.6084874079663</description><description>-0.00388450522109274</description><description>-0.000366303694416237</description></item>
		/// <item><term>(9, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(9, 1)</term><description>2.19722457733622</description><description>2.19724530614821</description><description>-2.07288119891125E-05</description><description>-9.43408889693142E-06</description></item>
		/// <item><term>(9, 2)</term><description>4.27666611901606</description><description>4.27671668740824</description><description>-5.05683921803168E-05</description><description>-1.18242553365263E-05</description></item>
		/// <item><term>(9, 3)</term><description>6.22257626807137</description><description>6.22267201695177</description><description>-9.57488804003148E-05</description><description>-1.53873373785086E-05</description></item>
		/// <item><term>(9, 4)</term><description>8.01433573729942</description><description>8.01450462422913</description><description>-0.00016888692971051</description><description>-2.10731039036081E-05</description></item>
		/// <item><term>(9, 5)</term><description>9.62377364973352</description><description>9.62407243172855</description><description>-0.0002987819950242</description><description>-3.10462408924666E-05</description></item>
		/// <item><term>(9, 6)</term><description>11.0100680108534</description><description>11.0106314330547</description><description>-0.000563422201247832</description><description>-5.11733624799071E-05</description></item>
		/// <item><term>(9, 7)</term><description>12.1086802995215</description><description>12.1099187487916</description><description>-0.00123844927002992</description><description>-0.000102277807275072</description></item>
		/// <item><term>(9, 8)</term><description>12.8018274800815</description><description>12.8057327141146</description><description>-0.00390523403308585</description><description>-0.000305052855864684</description></item>
		/// <item><term>(10, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(10, 1)</term><description>2.30258509299405</description><description>2.3026000733849</description><description>-1.49803908575663E-05</description><description>-6.50590108619497E-06</description></item>
		/// <item><term>(10, 2)</term><description>4.49980967033027</description><description>4.49984537953311</description><description>-3.57092028462347E-05</description><description>-7.93571405512665E-06</description></item>
		/// <item><term>(10, 3)</term><description>6.5792512120101</description><description>6.57931676079314</description><description>-6.55487830361068E-05</description><description>-9.96295489013259E-06</description></item>
		/// <item><term>(10, 4)</term><description>8.52516136106541</description><description>8.52527209033667</description><description>-0.000110729271256105</description><description>-1.29885249752348E-05</description></item>
		/// <item><term>(10, 5)</term><description>10.3169208302935</description><description>10.317104697614</description><description>-0.000183867320568964</description><description>-1.78219183410884E-05</description></item>
		/// <item><term>(10, 6)</term><description>11.9263587427276</description><description>11.9266725051135</description><description>-0.000313762385882654</description><description>-2.63083135977257E-05</description></item>
		/// <item><term>(10, 7)</term><description>13.3126531038475</description><description>13.3132315064396</description><description>-0.00057840259210451</description><description>-4.34475823558677E-05</description></item>
		/// <item><term>(10, 8)</term><description>14.4112653925156</description><description>14.4125188221765</description><description>-0.00125342966088482</description><description>-8.6975683726967E-05</description></item>
		/// <item><term>(10, 9)</term><description>15.1044125730755</description><description>15.1083327874995</description><description>-0.0039202144239443</description><description>-0.000259541005317367</description></item>
		/// <item><term>(11, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(11, 1)</term><description>2.39789527279837</description><description>2.39790644845045</description><description>-1.11756520770889E-05</description><description>-4.66060891143374E-06</description></item>
		/// <item><term>(11, 2)</term><description>4.70048036579242</description><description>4.70050652183535</description><description>-2.61560429342111E-05</description><description>-5.56454679069842E-06</description></item>
		/// <item><term>(11, 3)</term><description>6.89770494312864</description><description>6.89775182798356</description><description>-4.68848549211032E-05</description><description>-6.79716736329944E-06</description></item>
		/// <item><term>(11, 4)</term><description>8.97714648480847</description><description>8.97722320924358</description><description>-7.67244351109753E-05</description><description>-8.54663954083982E-06</description></item>
		/// <item><term>(11, 5)</term><description>10.9230566338638</description><description>10.9231785387871</description><description>-0.000121904923330973</description><description>-1.11603306123162E-05</description></item>
		/// <item><term>(11, 6)</term><description>12.7148161030918</description><description>12.7150111460645</description><description>-0.000195042972643833</description><description>-1.53398186070819E-05</description></item>
		/// <item><term>(11, 7)</term><description>14.3242540155259</description><description>14.3245789535639</description><description>-0.00032493803795397</description><description>-2.26844649363082E-05</description></item>
		/// <item><term>(11, 8)</term><description>15.7105483766458</description><description>15.71113795489</description><description>-0.000589578244182931</description><description>-3.75275407355835E-05</description></item>
		/// <item><term>(11, 9)</term><description>16.8091606653139</description><description>16.8104252706269</description><description>-0.00126460531296146</description><description>-7.52331028384424E-05</description></item>
		/// <item><term>(11, 10)</term><description>17.5023078458739</description><description>17.5062392359499</description><description>-0.00393139007601562</description><description>-0.000224621239132326</description></item>
		/// <item><term>(12, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(12, 1)</term><description>2.484906649788</description><description>2.48491520755264</description><description>-8.55776463959401E-06</description><description>-3.44389783830476E-06</description></item>
		/// <item><term>(12, 2)</term><description>4.88280192258637</description><description>4.88282165600309</description><description>-1.97334167157948E-05</description><description>-4.04141249812201E-06</description></item>
		/// <item><term>(12, 3)</term><description>7.18538701558042</description><description>7.18542172938799</description><description>-3.47138075733611E-05</description><description>-4.83116740936702E-06</description></item>
		/// <item><term>(12, 4)</term><description>9.38261159291664</description><description>9.3826670355362</description><description>-5.54426195620294E-05</description><description>-5.90908181725071E-06</description></item>
		/// <item><term>(12, 5)</term><description>11.4620531345965</description><description>11.4621384167962</description><description>-8.52821997501252E-05</description><description>-7.44039473109E-06</description></item>
		/// <item><term>(12, 6)</term><description>13.4079632836518</description><description>13.4080937463398</description><description>-0.000130462687973676</description><description>-9.73023905373816E-06</description></item>
		/// <item><term>(12, 7)</term><description>15.1997227528798</description><description>15.1999263536171</description><description>-0.000203600737282983</description><description>-1.33950296721305E-05</description></item>
		/// <item><term>(12, 8)</term><description>16.8091606653139</description><description>16.8094941611165</description><description>-0.000333495802596673</description><description>-1.9840122254578E-05</description></item>
		/// <item><term>(12, 9)</term><description>18.1954550264338</description><description>18.1960531624426</description><description>-0.000598136008818528</description><description>-3.28728249966584E-05</description></item>
		/// <item><term>(12, 10)</term><description>19.2940673151019</description><description>19.2953404781795</description><description>-0.00127316307760239</description><description>-6.59872828683382E-05</description></item>
		/// <item><term>(12, 11)</term><description>19.9872144956619</description><description>19.9911544435025</description><description>-0.0039399478406601</description><description>-0.000197123408142502</description></item>
		/// <item><term>(13, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(13, 1)</term><description>2.56494935746154</description><description>2.56495605520329</description><description>-6.697741751438E-06</description><description>-2.61125691700462E-06</description></item>
		/// <item><term>(13, 2)</term><description>5.04985600724954</description><description>5.04987126275593</description><description>-1.52555063932525E-05</description><description>-3.02097849351581E-06</description></item>
		/// <item><term>(13, 3)</term><description>7.44775128004791</description><description>7.44777771120638</description><description>-2.64311584698973E-05</description><description>-3.54887770496644E-06</description></item>
		/// <item><term>(13, 4)</term><description>9.75033637304195</description><description>9.75037778459128</description><description>-4.14115493256872E-05</description><description>-4.24719186511178E-06</description></item>
		/// <item><term>(13, 5)</term><description>11.9475609503782</description><description>11.9476230907395</description><description>-6.21403613152438E-05</description><description>-5.20109180219557E-06</description></item>
		/// <item><term>(13, 6)</term><description>14.027002492058</description><description>14.0270944719995</description><description>-9.19799415068923E-05</description><description>-6.55734834002993E-06</description></item>
		/// <item><term>(13, 7)</term><description>15.9729126411133</description><description>15.973049801543</description><description>-0.000137160429723338</description><description>-8.58706441368087E-06</description></item>
		/// <item><term>(13, 8)</term><description>17.7646721103414</description><description>17.7648824088204</description><description>-0.000210298479036197</description><description>-1.18380163579701E-05</description></item>
		/// <item><term>(13, 9)</term><description>19.3741100227755</description><description>19.3744502163198</description><description>-0.000340193544349887</description><description>-1.75591830515037E-05</description></item>
		/// <item><term>(13, 10)</term><description>20.7604043838954</description><description>20.7610092176459</description><description>-0.000604833750575295</description><description>-2.91340062260294E-05</description></item>
		/// <item><term>(13, 11)</term><description>21.8590166725635</description><description>21.8602965333828</description><description>-0.00127986081935205</description><description>-5.85507042024667E-05</description></item>
		/// <item><term>(13, 12)</term><description>22.5521638531234</description><description>22.5561104987058</description><description>-0.00394664558240976</description><description>-0.000175000749733519</description></item>
		/// <item><term>(14, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(14, 1)</term><description>2.63905732961526</description><description>2.63906266962529</description><description>-5.34001003105189E-06</description><description>-2.02345359122244E-06</description></item>
		/// <item><term>(14, 2)</term><description>5.2040066870768</description><description>5.20401872482858</description><description>-1.20377517855985E-05</description><description>-2.31316993029469E-06</description></item>
		/// <item><term>(14, 3)</term><description>7.6889133368648</description><description>7.68893393238122</description><description>-2.05955164238603E-05</description><description>-2.67859911037289E-06</description></item>
		/// <item><term>(14, 4)</term><description>10.0868086096632</description><description>10.0868403808317</description><description>-3.17711685013933E-05</description><description>-3.14977409910965E-06</description></item>
		/// <item><term>(14, 5)</term><description>12.3893937026572</description><description>12.3894404542166</description><description>-4.6751559356295E-05</description><description>-3.77351470768646E-06</description></item>
		/// <item><term>(14, 6)</term><description>14.5866182799934</description><description>14.5866857603648</description><description>-6.74803713458516E-05</description><description>-4.62618339978127E-06</description></item>
		/// <item><term>(14, 7)</term><description>16.6660598216733</description><description>16.6661571416248</description><description>-9.7319951532171E-05</description><description>-5.83940970892304E-06</description></item>
		/// <item><term>(14, 8)</term><description>18.6119699707286</description><description>18.6121124711683</description><description>-0.000142500439757498</description><description>-7.65638672217994E-06</description></item>
		/// <item><term>(14, 9)</term><description>20.4037294399566</description><description>20.4039450784457</description><description>-0.000215638489066805</description><description>-1.05685820673803E-05</description></item>
		/// <item><term>(14, 10)</term><description>22.0131673523907</description><description>22.0135128859451</description><description>-0.000345533554384048</description><description>-1.56966759418436E-05</description></item>
		/// <item><term>(14, 11)</term><description>23.3994617135106</description><description>23.4000718872712</description><description>-0.000610173760605903</description><description>-2.60764015889132E-05</description></item>
		/// <item><term>(14, 12)</term><description>24.4980740021787</description><description>24.4993592030081</description><description>-0.00128520082938266</description><description>-5.24613008054576E-05</description></item>
		/// <item><term>(14, 13)</term><description>25.1912211827387</description><description>25.1951731683311</description><description>-0.00395198559243681</description><description>-0.000156879476535451</description></item>
		/// <item><term>(15, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(15, 1)</term><description>2.70805020110221</description><description>2.7080545269328</description><description>-4.32583058662672E-06</description><description>-1.59739674872573E-06</description></item>
		/// <item><term>(15, 2)</term><description>5.34710753071747</description><description>5.34711719655809</description><description>-9.66584061767861E-06</description><description>-1.80767649839682E-06</description></item>
		/// <item><term>(15, 3)</term><description>7.91205688817901</description><description>7.91207325176138</description><description>-1.63635823717811E-05</description><description>-2.06818310371721E-06</description></item>
		/// <item><term>(15, 4)</term><description>10.396963537967</description><description>10.396988459314</description><description>-2.49213470109311E-05</description><description>-2.39698320763796E-06</description></item>
		/// <item><term>(15, 5)</term><description>12.7948588107654</description><description>12.7948949077645</description><description>-3.60969990893523E-05</description><description>-2.82121120859738E-06</description></item>
		/// <item><term>(15, 6)</term><description>15.0974439037594</description><description>15.0974949811494</description><description>-5.10773899460304E-05</description><description>-3.38318130351268E-06</description></item>
		/// <item><term>(15, 7)</term><description>17.2946684810956</description><description>17.2947402872976</description><description>-7.18062019302579E-05</description><description>-4.15192705247559E-06</description></item>
		/// <item><term>(15, 8)</term><description>19.3741100227755</description><description>19.3742116685576</description><description>-0.000101645782123683</description><description>-5.24647490925734E-06</description></item>
		/// <item><term>(15, 9)</term><description>21.3200201718308</description><description>21.3201669981011</description><description>-0.000146826270341904</description><description>-6.88677914741842E-06</description></item>
		/// <item><term>(15, 10)</term><description>23.1117796410588</description><description>23.1119996053785</description><description>-0.000219964319654764</description><description>-9.51741160009981E-06</description></item>
		/// <item><term>(15, 11)</term><description>24.7212175534929</description><description>24.7215674128779</description><description>-0.000349859384972007</description><description>-1.41521906926697E-05</description></item>
		/// <item><term>(15, 12)</term><description>26.1075119146128</description><description>26.108126414204</description><description>-0.000614499591186757</description><description>-2.35372713108985E-05</description></item>
		/// <item><term>(15, 13)</term><description>27.2061242032809</description><description>27.2074137299409</description><description>-0.00128952665997062</description><description>-4.73983964174914E-05</description></item>
		/// <item><term>(15, 14)</term><description>27.8992713838409</description><description>27.9032276952639</description><description>-0.00395631142303188</description><description>-0.000141806980139394</description></item>
		/// <item><term>(16, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(16, 1)</term><description>2.77258872223978</description><description>2.77259227529662</description><description>-3.55305683408247E-06</description><description>-1.28149436863185E-06</description></item>
		/// <item><term>(16, 2)</term><description>5.48063892334199</description><description>5.48064680222941</description><description>-7.87888741982101E-06</description><description>-1.4375855680375E-06</description></item>
		/// <item><term>(16, 3)</term><description>8.11969625295725</description><description>8.1197094718547</description><description>-1.32188974557579E-05</description><description>-1.62800393560824E-06</description></item>
		/// <item><term>(16, 4)</term><description>10.6846456104188</description><description>10.684665527058</description><description>-1.99166392036432E-05</description><description>-1.86404303238866E-06</description></item>
		/// <item><term>(16, 5)</term><description>13.1695522602068</description><description>13.1695807346106</description><description>-2.84744038463458E-05</description><description>-2.16213909810619E-06</description></item>
		/// <item><term>(16, 6)</term><description>15.5674475330052</description><description>15.5674871830611</description><description>-3.9650055924767E-05</description><description>-2.54698503660946E-06</description></item>
		/// <item><term>(16, 7)</term><description>17.8700326259992</description><description>17.870087256446</description><description>-5.46304467761161E-05</description><description>-3.05709832318011E-06</description></item>
		/// <item><term>(16, 8)</term><description>20.0672572033354</description><description>20.0673325625942</description><description>-7.53592587656726E-05</description><description>-3.75533427423988E-06</description></item>
		/// <item><term>(16, 9)</term><description>22.1466987450153</description><description>22.1468039438542</description><description>-0.000105198838959097</description><description>-4.75009120638241E-06</description></item>
		/// <item><term>(16, 10)</term><description>24.0926088940706</description><description>24.0927592733977</description><description>-0.000150379327173766</description><description>-6.24172034813449E-06</description></item>
		/// <item><term>(16, 11)</term><description>25.8843683632986</description><description>25.8845918806751</description><description>-0.000223517376490179</description><description>-8.63522622429927E-06</description></item>
		/// <item><term>(16, 12)</term><description>27.4938062757327</description><description>27.4941596881745</description><description>-0.000353412441800316</description><description>-1.28542566371486E-05</description></item>
		/// <item><term>(16, 13)</term><description>28.8801006368526</description><description>28.8807186895006</description><description>-0.000618052648022172</description><description>-2.14006403853559E-05</description></item>
		/// <item><term>(16, 14)</term><description>29.9787129255207</description><description>29.9800060052375</description><description>-0.00129307971679893</description><description>-4.31332632595489E-05</description></item>
		/// <item><term>(16, 15)</term><description>30.6718601060807</description><description>30.6758199705605</description><description>-0.00395986447986019</description><description>-0.000129104151693596</description></item>
		/// <item><term>(17, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(17, 1)</term><description>2.83321334405622</description><description>2.8332162979823</description><description>-2.95392608373746E-06</description><description>-1.04260630069899E-06</description></item>
		/// <item><term>(17, 2)</term><description>5.605802066296</description><description>5.60580857327892</description><description>-6.50698291781993E-06</description><description>-1.16075859276983E-06</description></item>
		/// <item><term>(17, 3)</term><description>8.31385226739821</description><description>8.31386310021171</description><description>-1.08328135031144E-05</description><description>-1.30298364160186E-06</description></item>
		/// <item><term>(17, 4)</term><description>10.9529095970135</description><description>10.952925769837</description><description>-1.61728235372749E-05</description><description>-1.47657783477778E-06</description></item>
		/// <item><term>(17, 5)</term><description>13.517858954475</description><description>13.5178818250403</description><description>-2.28705652887129E-05</description><description>-1.69187778669208E-06</description></item>
		/// <item><term>(17, 6)</term><description>16.002765604263</description><description>16.0027970325929</description><description>-3.14283299260865E-05</description><description>-1.96393115435711E-06</description></item>
		/// <item><term>(17, 7)</term><description>18.4006608770614</description><description>18.4007034810434</description><description>-4.2603982006284E-05</description><description>-2.31535064370405E-06</description></item>
		/// <item><term>(17, 8)</term><description>20.7032459700554</description><description>20.7033035544283</description><description>-5.75843728611858E-05</description><description>-2.78141760690445E-06</description></item>
		/// <item><term>(17, 9)</term><description>22.9004705473916</description><description>22.9005488605765</description><description>-7.83131848507423E-05</description><description>-3.41971946334798E-06</description></item>
		/// <item><term>(17, 10)</term><description>24.9799120890715</description><description>24.9800202418365</description><description>-0.000108152765037062</description><description>-4.32958949781003E-06</description></item>
		/// <item><term>(17, 11)</term><description>26.9258222381268</description><description>26.9259755713801</description><description>-0.000153333253262389</description><description>-5.69465444383978E-06</description></item>
		/// <item><term>(17, 12)</term><description>28.7175817073548</description><description>28.7178081786574</description><description>-0.000226471302571696</description><description>-7.88615506972491E-06</description></item>
		/// <item><term>(17, 13)</term><description>30.3270196197889</description><description>30.3273759861568</description><description>-0.000356366367881833</description><description>-1.17507876589791E-05</description></item>
		/// <item><term>(17, 14)</term><description>31.7133139809088</description><description>31.7139349874829</description><description>-0.000621006574110794</description><description>-1.95818883666537E-05</description></item>
		/// <item><term>(17, 15)</term><description>32.8119262695769</description><description>32.8132223032198</description><description>-0.00129603364288755</description><description>-3.94988588063855E-05</description></item>
		/// <item><term>(17, 16)</term><description>33.5050734501369</description><description>33.5090362685428</description><description>-0.00396281840594526</description><description>-0.000118275174410312</description></item>
		/// <item><term>(18, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(18, 1)</term><description>2.89037175789616</description><description>2.89037424017505</description><description>-2.48227888111074E-06</description><description>-8.5880955428292E-07</description></item>
		/// <item><term>(18, 2)</term><description>5.72358510195238</description><description>5.72359053815734</description><description>-5.43620496351593E-06</description><description>-9.49790186864101E-07</description></item>
		/// <item><term>(18, 3)</term><description>8.49617382419216</description><description>8.49618281345396</description><description>-8.98926179715431E-06</description><description>-1.05803647420185E-06</description></item>
		/// <item><term>(18, 4)</term><description>11.2042240252944</description><description>11.2042373403868</description><description>-1.33150923851133E-05</description><description>-1.18839933538043E-06</description></item>
		/// <item><term>(18, 5)</term><description>13.8432813549096</description><description>13.843300010012</description><description>-1.86551024174975E-05</description><description>-1.34759252082103E-06</description></item>
		/// <item><term>(18, 6)</term><description>16.4082307123712</description><description>16.4082560652153</description><description>-2.53528441724882E-05</description><description>-1.54512967405883E-06</description></item>
		/// <item><term>(18, 7)</term><description>18.8931373621592</description><description>18.893171272768</description><description>-3.39106088134145E-05</description><description>-1.7948638261284E-06</description></item>
		/// <item><term>(18, 8)</term><description>21.2910326349575</description><description>21.2910777212184</description><description>-4.50862608865066E-05</description><description>-2.11761738660246E-06</description></item>
		/// <item><term>(18, 9)</term><description>23.5936177279516</description><description>23.5936777946033</description><description>-6.00666517378556E-05</description><description>-2.54588560476226E-06</description></item>
		/// <item><term>(18, 10)</term><description>25.7908423052878</description><description>25.7909231007515</description><description>-8.07954637309649E-05</description><description>-3.13271907813572E-06</description></item>
		/// <item><term>(18, 11)</term><description>27.8702838469676</description><description>27.8703944820116</description><description>-0.00011063504392439</description><description>-3.96964180673126E-06</description></item>
		/// <item><term>(18, 12)</term><description>29.816193996023</description><description>29.8163498115551</description><description>-0.000155815532146164</description><description>-5.22586927650617E-06</description></item>
		/// <item><term>(18, 13)</term><description>31.607953465251</description><description>31.6081824188325</description><description>-0.000228953581448366</description><description>-7.24354336006194E-06</description></item>
		/// <item><term>(18, 14)</term><description>33.2173913776851</description><description>33.2177502263319</description><description>-0.000358848646762056</description><description>-1.08030351535408E-05</description></item>
		/// <item><term>(18, 15)</term><description>34.603685738805</description><description>34.604309227658</description><description>-0.000623488852987464</description><description>-1.80179896931695E-05</description></item>
		/// <item><term>(18, 16)</term><description>35.7022980274731</description><description>35.7035965433949</description><description>-0.00129851592177488</description><description>-3.63706538098938E-05</description></item>
		/// <item><term>(18, 17)</term><description>36.3954452080331</description><description>36.3994105087179</description><description>-0.00396530068482548</description><description>-0.000108950465151894</description></item>
		/// <item><term>(19, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(19, 1)</term><description>2.94443897916644</description><description>2.94444108509846</description><description>-2.10593201721565E-06</description><description>-7.15223522075445E-07</description></item>
		/// <item><term>(19, 2)</term><description>5.8348107370626</description><description>5.8348153252735</description><description>-4.58821089832639E-06</description><description>-7.86351281144762E-07</description></item>
		/// <item><term>(19, 3)</term><description>8.66802408111882</description><description>8.6680316232558</description><description>-7.54213698073158E-06</description><description>-8.70110293897348E-07</description></item>
		/// <item><term>(19, 4)</term><description>11.4406128033586</description><description>11.4406238985524</description><description>-1.109519381437E-05</description><description>-9.69807649736452E-07</description></item>
		/// <item><term>(19, 5)</term><description>14.1486630044608</description><description>14.1486784254852</description><description>-1.5421024402329E-05</description><description>-1.08992803047659E-06</description></item>
		/// <item><term>(19, 6)</term><description>16.7877203340761</description><description>16.7877410951105</description><description>-2.07610344347131E-05</description><description>-1.23667978865313E-06</description></item>
		/// <item><term>(19, 7)</term><description>19.3526696915376</description><description>19.3526971503138</description><description>-2.74587761879275E-05</description><description>-1.41886244252567E-06</description></item>
		/// <item><term>(19, 8)</term><description>21.8375763413256</description><description>21.8376123578664</description><description>-3.60165408288537E-05</description><description>-1.64929203982659E-06</description></item>
		/// <item><term>(19, 9)</term><description>24.235471614124</description><description>24.2355188063169</description><description>-4.71921929054986E-05</description><description>-1.94723641680634E-06</description></item>
		/// <item><term>(19, 10)</term><description>26.538056707118</description><description>26.5381188797018</description><description>-6.21725837675058E-05</description><description>-2.34277077834527E-06</description></item>
		/// <item><term>(19, 11)</term><description>28.7352812844542</description><description>28.73536418585</description><description>-8.29013957428515E-05</description><description>-2.88500380150102E-06</description></item>
		/// <item><term>(19, 12)</term><description>30.8147228261341</description><description>30.81483556711</description><description>-0.000112740975943382</description><description>-3.6586724008358E-06</description></item>
		/// <item><term>(19, 13)</term><description>32.7606329751894</description><description>32.7607908966535</description><description>-0.000157921464150945</description><description>-4.82046437474343E-06</description></item>
		/// <item><term>(19, 14)</term><description>34.5523924444175</description><description>34.5526235039309</description><description>-0.000231059513467358</description><description>-6.68722184256996E-06</description></item>
		/// <item><term>(19, 15)</term><description>36.1618303568515</description><description>36.1621913114303</description><description>-0.000360954578788153</description><description>-9.98164570836673E-06</description></item>
		/// <item><term>(19, 16)</term><description>37.5481247179714</description><description>37.5487503127564</description><description>-0.00062559478499935</description><description>-1.66611459213548E-05</description></item>
		/// <item><term>(19, 17)</term><description>38.6467370066395</description><description>38.6480376284933</description><description>-0.00130062185379387</description><description>-3.36541181619142E-05</description></item>
		/// <item><term>(19, 18)</term><description>39.3398841871995</description><description>39.3438515938163</description><description>-0.00396740661683737</description><description>-0.000100849473728962</description></item>
		/// <item><term>(20, 0)</term><description>0</description><description>0</description><description>0</description><description>NaN</description></item>
		/// <item><term>(20, 1)</term><description>2.99573227355399</description><description>2.9957340755342</description><description>-1.80198020460765E-06</description><description>-6.01515769788688E-07</description></item>
		/// <item><term>(20, 2)</term><description>5.94017125272043</description><description>5.94017516063265</description><description>-3.90791222315556E-06</description><description>-6.57878713743455E-07</description></item>
		/// <item><term>(20, 3)</term><description>8.8305430106166</description><description>8.8305494008077</description><description>-6.39019110515449E-06</description><description>-7.23646450447251E-07</description></item>
		/// <item><term>(20, 4)</term><description>11.6637563546728</description><description>11.66376569879</description><description>-9.3441171866715E-06</description><description>-8.01124174968555E-07</description></item>
		/// <item><term>(20, 5)</term><description>14.4363450769126</description><description>14.4363579740866</description><description>-1.28971740185335E-05</description><description>-8.93382220348792E-07</description></item>
		/// <item><term>(20, 6)</term><description>17.1443952780148</description><description>17.1444125010194</description><description>-1.72230046047162E-05</description><description>-1.00458513265861E-06</description></item>
		/// <item><term>(20, 7)</term><description>19.7834526076301</description><description>19.7834751706447</description><description>-2.25630146424294E-05</description><description>-1.14049934002558E-06</description></item>
		/// <item><term>(20, 8)</term><description>22.3484019650916</description><description>22.348431225848</description><description>-2.92607563885383E-05</description><description>-1.30929971790573E-06</description></item>
		/// <item><term>(20, 9)</term><description>24.8333086148796</description><description>24.8333464334006</description><description>-3.781852103657E-05</description><description>-1.5228949804099E-06</description></item>
		/// <item><term>(20, 10)</term><description>27.231203887678</description><description>27.2312528818511</description><description>-4.89941731132149E-05</description><description>-1.79919232786416E-06</description></item>
		/// <item><term>(20, 11)</term><description>29.533788980672</description><description>29.533852955236</description><description>-6.39745639716693E-05</description><description>-2.16614820446969E-06</description></item>
		/// <item><term>(20, 12)</term><description>31.7310135580082</description><description>31.7310982613842</description><description>-8.47033759541205E-05</description><description>-2.66941917248474E-06</description></item>
		/// <item><term>(20, 13)</term><description>33.8104550996881</description><description>33.8105696426442</description><description>-0.00011454295614044</description><description>-3.38779693449014E-06</description></item>
		/// <item><term>(20, 14)</term><description>35.7563652487434</description><description>35.7565249721877</description><description>-0.000159723444362214</description><description>-4.46699331017231E-06</description></item>
		/// <item><term>(20, 15)</term><description>37.5481247179714</description><description>37.5483575794651</description><description>-0.000232861493678627</description><description>-6.20168105405204E-06</description></item>
		/// <item><term>(20, 16)</term><description>39.1575626304055</description><description>39.1579253869645</description><description>-0.000362756558985211</description><description>-9.26402295283654E-06</description></item>
		/// <item><term>(20, 17)</term><description>40.5438569915254</description><description>40.5444843882906</description><description>-0.000627396765210619</description><description>-1.54745209697676E-05</description></item>
		/// <item><term>(20, 18)</term><description>41.6424692801935</description><description>41.6437717040275</description><description>-0.00130242383399803</description><description>-3.12763353497269E-05</description></item>
		/// <item><term>(20, 19)</term><description>42.3356164607535</description><description>42.3395856693505</description><description>-0.00396920859704153</description><description>-9.37557765509596E-05</description></item>
		/// </list>
		/// </para>
		/// </remarks>
		public static double LogFactorialDivGosper (int n, int k) {
			int nk = n - k;
			return Math.Log (Math.Sqrt ((double)(0x06 * n + 0x01) / (0x06 * nk + 0x01))) + n * Math.Log ((double)n / nk) + k * Math.Log (nk) - k;
		}
		#endregion
		#region Tests
		/// <summary>
		/// Checks if the two given values <paramref name="a"/> and <paramref name="b"/> are equal with a maximum
		/// difference of <paramref name="epsilon"/>.
		/// </summary>
		/// <returns><c>true</c>, if the two given values are aproximately equal, <c>false</c> otherwise.</returns>
		/// <param name="a">The first value to check.</param>
		/// <param name="b">The second value to check.</param>
		/// <param name="epsilon">The maximum allowed difference, optional, by default <c>1e-6</c>.</param>
		public static bool EqualEpsilon (double a, double b, double epsilon = 1e-6) {
			return (Math.Abs (a - b) <= epsilon);
		}
		#endregion
	}
}