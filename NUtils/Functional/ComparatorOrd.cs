//
//  ComparatorOrd.cs
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
using System.Diagnostics.Contracts;

namespace NUtils.Functional {

	/// <summary>
	/// A class that generates a <see cref="T:IOrd`1"/> given a <see cref="T:IComparer`1"/> that mimics its
	/// behavior.
	/// </summary>
	public class ComparatorOrd<TA> : OrdBase<TA> {

		#region private fields
		private readonly IComparer<TA> comparer;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ComparatorOrd`1"/> class with a given <see cref="T:IComparer`1"/>.
		/// </summary>
		/// <param name="comparer">The given comparer that describes the ordering relation, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given comparer is not effective.</exception>
		public ComparatorOrd (IComparer<TA> comparer) {
			if (comparer == null) {
				throw new ArgumentNullException ("comparer");
			}
			Contract.EndContractBlock ();
			this.comparer = comparer;
		}
		#endregion
		#region implemented abstract members of OrdBase
		/// <summary>
		/// Compares the two effective items.
		/// </summary>
		/// <returns>The result of the comparsion of the two effective items.</returns>
		/// <param name="a">The first item to compare.</param>
		/// <param name="b">The second item to compare.</param>
		protected override Ordering CompareEffective (TA a, TA b) {
			int c = this.comparer.Compare (a, b);
			if (c < 0x00) {
				return Ordering.LT;
			} else if (c > 0x00) {
				return Ordering.GT;
			} else {
				return Ordering.EQ;
			}
		}
		#endregion
	}
}

