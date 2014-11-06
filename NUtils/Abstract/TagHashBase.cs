//
//  TagHashBase.cs
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

namespace NUtils.Abstract {

	/// <summary>
	/// A basic implementation of the <see cref="T:ITag`1"/> interface where the hash code is determined by
	/// the tag.
	/// </summary>
	/// <typeparam name='TTag'>The type of the tag object of this instance.</typeparam>
	public class TagHashBase<TTag> : TagBase<TTag> {

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TagHashBase`1"/> class.
		/// </summary>
		/// <param name="tag">The initial tag of the object.</param>
		protected TagHashBase (TTag tag = default(TTag)) : base(tag) {
		}
		#endregion
		#region GetHashCode method
		/// <summary>
		/// Serves as a hash function for a <see cref="T:TagHashBase`1"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode () {
			if (this.Tag != null) {
				return this.Tag.GetHashCode ();
			} else {
				return 0x00;
			}
		}
		#endregion
	}
}

