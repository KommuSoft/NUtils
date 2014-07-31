//
//  FiniteIndexDistribution.cs
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
using System.Linq;
using System.Collections.Generic;
using NUtils.Collections;

namespace NUtils.Maths {
	/// <summary>
	/// A basic implementation of the <see cref="IFiniteIndexDistribution"/> interface. A distribution over some
	/// integers ranging from zero up to a number.
	/// </summary>
	public class FiniteIndexDistribution : FiniteDistribution<int>, IFiniteIndexDistribution {

		#region Fields
		/// <summary>
		/// The stored list of the probability values, used in this <see cref="FiniteIndexDistribution"/>.
		/// </summary>
		protected readonly double[] Probabilities;
		#endregion
		#region IValidateable implementation
		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c>, if this instance is valid, <c>false</c> otherwise.</value>
		/// <remarks>
		/// <para>
		/// A <see cref="FiniteIndexDistribution"/> is valid if all elements have a probability larger or equal to zero
		/// and sum up to one.
		/// </para>
		/// </remarks>
		public virtual bool IsValid {
			get {
				double[] val = this.Probabilities;
				int n = val.Length;
				double tmp, sum = 0.0d;
				for (int i = 0x00; i < n; i++) {
					tmp = val [i];
					if (tmp < 0.0d) {
						return false;
					}
					sum += tmp;
				}
				return MathUtils.EqualEpsilon (sum, 1.0d);
			}
		}
		#endregion
		#region ILength implementation
		/// <summary>
		/// Get the number of elements over which the <see cref="FiniteIndexDistribution"/> is defined.
		/// </summary>
		/// <value>The number of elements over which the distribtion is defined.</value>
		public int Length {
			get {
				return this.Probabilities.Length;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="FiniteIndexDistribution"/> class with a given list
		/// of initial probability values.
		/// </summary>
		/// <param name="probabilities">The given list of initial probability values.</param>
		/// <remarks>
		/// <para>The given list of values is used: modifications to the given list of values can have
		/// impact on this instance.</para>
		/// </remarks>
		public FiniteIndexDistribution (double[] probabilities) {
			this.Probabilities = probabilities;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FiniteIndexDistribution"/> class with a given list
		/// of initial probability values.
		/// </summary>
		/// <param name="probabilities">The given list of initial probability values.</param>
		public FiniteIndexDistribution (IEnumerable<double> probabilities) {
			this.Probabilities = probabilities.ToArray ();
		}
		#endregion
		#region IFinite implementation
		/// <summary>
		/// Enumerates the possible values of the domain of this instance/variable.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing all the possible values of the domain.</returns>
		public override IEnumerable<int> EnumerateDomain () {
			return EnumerableCollection.RangeEnumerable (this.Length);
		}
		#endregion
		#region IDistribution implementation
		/// <summary>
		/// Gets the probability (density) of the given item according to this <see cref="T:IDistribution`1"/>.
		/// </summary>
		/// <returns>The probability of the given <paramref name="item"/> in the total set
		/// of events/values.</returns>
		/// <param name="item">The item for which the probability (density) is calculated.</param>
		/// <remarks>
		/// <para>All values are greater than or equal to zero.</para>
		/// <para>Over all possible values, the sum/integral is equal to one.</para>
		/// </remarks>
		public override double GetDistributionValue (int item) {
			return this.Probabilities [item];
		}
		#endregion
	}
}

