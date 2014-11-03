//
//  LexicographicalOrd.cs
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
using NUtils.Functional;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace NUtils.Functional {

	/// <summary>
	/// A class that generates a <see cref="T:LexicographicalOrd`2"/> based on a given
	/// <see cref="T:IOrd`2"/> on the items of the list.
	/// </summary>
	public class LexicographicalOrd<TA,TB> : OrdBase<IEnumerable<TA>,IEnumerable<TB>> {

		#region private fields
		/// <summary>
		/// The order relation defined on the order.
		/// </summary>
		private readonly IOrd<TA,TB> itemOrder;
		#endregion
		#region Costructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LexicographicalOrd`2"/> class.
		/// </summary>
		/// <param name="itemOrder">The order relation defined on the items of the list, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="itemOrder"/> is not effective.</exception>
		public LexicographicalOrd (IOrd<TA,TB> itemOrder) {
			if (itemOrder == null) {
				throw new ArgumentNullException ("itemOrder");
			}
			Contract.EndContractBlock ();
			this.itemOrder = itemOrder;
		}
		#endregion
		#region implemented abstract members of OrdBase
		/// <summary>
		/// Compares the two effective items.
		/// </summary>
		/// <returns>The result of the comparsion of the two effective items.</returns>
		/// <param name="a">The first item to compare.</param>
		/// <param name="b">The second item to compare.</param>
		protected override Ordering CompareEffective (IEnumerable<TA> a, IEnumerable<TB> b) {
			IEnumerator<TA> ea = a.GetEnumerator ();
			IEnumerator<TB> eb = b.GetEnumerator ();
			TA ca;
			TB cb;
			bool ma, mb;
			Ordering res;
			IOrd<TA,TB> itemOrder = this.itemOrder;
			if (ea == null || eb == null) {
				return Ordering.Unknown;
			} else {
				ma = ea.MoveNext ();
				mb = eb.MoveNext ();
				while (ma && mb) {
					ca = ea.Current;
					cb = eb.Current;
					res = itemOrder.Compare (ca, cb);
					if ((res & Ordering.EQ) != null) {
						return res;
					}
					ma = ea.MoveNext ();
					mb = eb.MoveNext ();
				}
				if (ma) {
					return Ordering.GT;
				} else if (mb) {
					return Ordering.LT;
				}
				return Ordering.EQ;
			}
		}
		#endregion
	}

	/// <summary>
	/// A class that generates a <see cref="T:LexicographicalOrd`1"/> based on a given
	/// <see cref="T:IOrd`1"/> on the items of the list.
	/// </summary>
	public class LexicographicalOrd<TA> : LexicographicalOrd<TA,TA> {

		#region Costructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LexicographicalOrd`1"/> class.
		/// </summary>
		/// <param name="itemOrder">The order relation defined on the items of the list, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="itemOrder"/> is not effective.</exception>
		public LexicographicalOrd (IOrd<TA> itemOrder) : base(itemOrder) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LexicographicalOrd`1"/> class.
		/// </summary>
		/// <param name="itemOrder">The order relation defined on the items of the list, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="itemOrder"/> is not effective.</exception>
		public LexicographicalOrd (IOrd<TA,TA> itemOrder) : base(itemOrder) {
		}
		#endregion
	}
}

