//
//  TagRegister.cs
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
using NUtils.Abstract;
using System.Collections.Generic;

namespace NUtils.Collections {

	/// <summary>
	/// A basic implementation of a <see cref="T:ITagRegister`2"/>. A register is a datastructure that
	/// stores items based on the tag of the values for fast lookups.
	/// </summary>
	/// <typeparam name='TKey'>The key on which the given items are stored, the type of tag associated with <typeparamref name="TValue"/>.</typeparam>
	/// <typeparam name='TValue'>The items to store in the register</typeparam>
	/// <typeparam name='TCollection'>The type of collection used to store values if multiple values map on the same key.</typeparam>
	public class TagRegister<TKey,TValue,TCollection> : Register<TKey,TValue,TCollection>, ITagRegister<TKey,TValue>, ICloneable<TagRegister<TKey,TValue,TCollection>>
	    where TValue : ITag<TKey>
	    where TCollection : ICollection<TValue>, new() {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TagRegister`3"/> class by cloning the given register.
		/// </summary>
		/// <param name='origin'>The given <see cref="T:TagRegister`3"/> instance that must be cloned, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="origin"/> is not effective.</exception>
		protected TagRegister (TagRegister<TKey,TValue,TCollection> origin) : base(origin) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TagRegister`3"/> class.
		/// </summary>
		public TagRegister () : base(x => x.Tag) {
		}
		#endregion
		#region ICloneable implementation
		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		ITagRegister<TKey, TValue> ICloneable<ITagRegister<TKey, TValue>>.Clone () {
			return new TagRegister<TKey,TValue,TCollection> (this);
		}
		#endregion
		#region ICloneable implementation
		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		TagRegister<TKey, TValue,TCollection> ICloneable<TagRegister<TKey, TValue,TCollection>>.Clone () {
			return new TagRegister<TKey,TValue,TCollection> (this);
		}
		#endregion
		#region Clone method override
		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		public override ListDictionary<TKey, TValue, TCollection> Clone () {
			return new TagRegister<TKey, TValue, TCollection> (this);
		}
		#endregion
	}

	/// <summary>
	/// A basic implementation of a <see cref="T:ITagRegister`2"/> where the collection is by default <see cref="T:List`1"/>.
	/// A register is a datastructure that stores items based on a <typeparamref name="TKey"/> for fast lookups.
	/// </summary>
	/// <typeparam name='TKey'>The key on which the given items are stored.</typeparam>
	/// <typeparam name='TValue'>The items to store in the register.</typeparam>
	public class TagRegister<TKey,TValue> : TagRegister<TKey,TValue,List<TValue>> where TValue : ITag<TKey> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TagRegister`2"/> class.
		/// </summary>
		public TagRegister () : base() {
		}
		#endregion
	}
}

