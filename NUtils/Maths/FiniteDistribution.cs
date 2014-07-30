//
//  FiniteDistribution.cs
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
	/// A basic implementation of the <see cref="T:IFiniteDistribution`1"/> interface that implements some
	/// basic functionality.
	/// </summary>
	/// <typeparam name='TItem'>The type of items over which the distribution is defined. The type
	/// must only allow a finite number of values.</typeparam>
	public abstract class FiniteDistribution<TItem> : IFiniteDistribution<TItem> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:FiniteDistribution`1"/> class.
		/// </summary>
		protected FiniteDistribution () {
		}
		#endregion
		#region IFiniteDistribution implementation
		/// <summary>
		/// Enumerates a list of <see cref="T:Tuple`2"/> instances where a value is associated with the probability
		/// of that item.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> of <see cref="T:Tuple`2"/> instances containing a value-probability
		/// couple.</returns>
		public virtual IEnumerable<Tuple<TItem, double>> GetDistributionValues () {
			foreach (TItem item in this.EnumerateDomain ()) {
				yield return new Tuple<TItem,double> (item, this.GetDistributionValue (item));
			}
		}
		#endregion
		#region IFinite implementation
		/// <summary>
		/// Enumerates the possible values of the domain of this instance/variable.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing all the possible values of the domain.</returns>
		public abstract IEnumerable<TItem> EnumerateDomain ();
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
		public abstract double GetDistributionValue (TItem item);
		#endregion
	}
}

