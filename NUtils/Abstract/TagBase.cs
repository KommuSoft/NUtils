//
//  TagBase.cs
//
//  Author:
//       Willem Van Onsem <Willem.VanOnsem@cs.kuleuven.be>
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

namespace ZincOxide.Utils.Abstract {

	/// <summary>
	/// An implementation class of a <see cref="T:ITag`1"/> interface.
	/// </summary>
	/// <typeparam name='TTag'>The type of the tag object of this instance.</typeparam>
	public abstract class TagBase<TTag>  : ITag<TTag> {

		#region ITag implementation
		/// <summary>
		/// Gets the tag of the object.
		/// </summary>
		/// <value>The tag of the object.</value>
		public TTag Tag {
			get;
			protected set;
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TagBase`1"/> class with a given tag.
		/// </summary>
		/// <param name="tag">The initial tag of the object.</param>
		protected TagBase (TTag tag = default(TTag)) {
			this.Tag = tag;
		}
		#endregion
	}
}

