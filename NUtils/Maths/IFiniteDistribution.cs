//
//  IFiniteDistribution.cs
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
using NUtils.Abstract;

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying a distribution over a finite type (such as an integer range).
	/// </summary>
	/// <typeparam name='TItem'>The type of items over which the distribution is defined. The type
	/// must only allow a finite number of values.</typeparam>
	public interface IFiniteDistribution<TItem> : IDistribution<TItem>, IFinite<TItem> {

		/// <summary>
		/// Enumerates a list of <see cref="T:Tuple`2"/> instances where a value is associated with the probability
		/// of that item.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> of <see cref="T:Tuple`2"/> instances containing a value-probability
		/// couple.</returns>
		IEnumerable<Tuple<TItem,double>> GetDistributionValues ();
	}
}

