//
//  EnumerableBase.cs
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
using System.Collections.Generic;

namespace NUtils.Collections {
	/// <summary>
	/// A basic implementation of <see cref="T:IEnumerable`1"/> where the <see cref="M:System.Collections.IEnumerable.GetEnumerator"/>
	/// is implemented in function of <see cref="M:IEnumerable`1.GetEnumerator"/>.
	/// </summary>
	public abstract class EnumerableBase<TElement> : IEnumerable<TElement> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EnumerableBase`1"/> class with no arguments, used to shield
		/// the constructor from unauthorized calls.
		/// </summary>
		protected EnumerableBase () {
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Get the <see cref="T:IEnumerator`1"/> that enumerates all the items stored in this <see cref="T:IEumerable`1"/>
		/// instance.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerator`1"/> emmitting all items stored in this instance.</returns>
		public abstract IEnumerator<TElement> GetEnumerator ();
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Enumerate all the items, accessed through the <see cref="T:System.Collections.IEnumerable"/> interface.
		/// </summary>
		/// <returns>All the items enumerated by <see cref="M:GetEnumerator"/>, but ungeneric.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
	}
}

