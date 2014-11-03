//
//  OrdBase.cs
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
using System.Diagnostics.Contracts;

namespace NUtils.Functional {

	/// <summary>
	/// An basic implementation of the <see cref="T:IOrd`2"/> interface that enables some
	/// programming convenience and furthermore provides a overridable non-null comparer.
	/// </summary>
	public abstract class OrdBase<TA,TB> : IOrd<TA,TB> {

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="T:OrdBase`2"/> class, used for inheritance only.
		/// </summary>
		protected OrdBase () {
		}
		#endregion
		#region Introduced abstract protected methods
		/// <summary>
		/// Compares the two effective items.
		/// </summary>
		/// <returns>The result of the comparsion of the two effective items.</returns>
		/// <param name="a">The first item to compare.</param>
		/// <param name="b">The second item to compare.</param>
		protected abstract Ordering CompareEffective (TA a, TB b);
		#endregion
		#region IOrd implementation
		/// <summary>
		/// A basic implementation of the <see cref="M:IOrd`2.Compare(TA,TB)"/> method
		/// that filters out <c>null</c> values by assuming these are greater than any
		/// other value and in case no <c>null</c> values were given, passes the argument to
		/// <see cref="M:CompareEffective"/>
		/// </summary>
		/// <param name="a">The first item to compare.</param>
		/// <param name="b">The second item to compare.</param>
		public virtual Ordering Compare (TA a, TB b) {
			if (a == null) {
				if (b == null) {
					return Ordering.EQ;
				} else {
					return Ordering.GT;
				}
			} else if (b == null) {
				return Ordering.LT;
			} else {
				return CompareEffective (a, b);
			}
		}

		/// <summary>
		/// Curry the <see cref="M:Compare(TA,TB)"/> method by providing the first element and return a function
		/// that maps the second item to the 
		/// </summary>
		/// <param name="a">The first item that will be curried.</param>
		public Func<TB, Ordering> Compare (TA a) {
			return x => this.Compare (a, x);
		}
		#endregion
	}

	/// <summary>
	/// An basic implementation of the <see cref="T:IOrd`2"/> interface that enables some
	/// programming convenience and furthermore provides a overridable non-null comparer.
	/// </summary>
	public abstract class OrdBase<TA> : OrdBase<TA,TA>, IOrd<TA> {

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="T:OrdBase`1"/> class, used for inheritance only.
		/// </summary>
		protected OrdBase () {
		}
		#endregion
	}
}

