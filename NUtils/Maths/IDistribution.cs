//
//  IDistribution.cs
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
	/// An interface specifying an distribution. A distribution describes the probability
	/// of certain events/values. The total probability must sum/integrate up to one.
	/// And the probability of every item is greater than or equal to zero.
	/// </summary>
	/// <typeparam name='TItem'>The type of elements over which the distribution is defined.</typeparam>
	public interface IDistribution<TItem> {

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
		double GetDistributionValue (TItem item);
	}
}

