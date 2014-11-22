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
		/// Calculate the factorial for the given <paramref name="n"/>
		/// </summary>
		/// <param name="n">The value to calculate the factorial from.</param>
		/// <returns>The factorial of the given value.</returns>
		/// <remarks>
		/// <para>This function will only work for values up to 20, because of <see cref="ulong"/> overflow.</para>
		/// <para>If <paramref name="n"/> is less than or equal to zero, one is returned.</para>
		/// </remarks>
		public static ulong Factorial (int n) {
			ulong r = 0x01;
			for (uint i = 0x02; i <= n; r*= i, i++)
				;
			return r;
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
		/// 
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