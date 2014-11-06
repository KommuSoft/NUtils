//
//  CloneBase.cs
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
	/// A basic implementation of the <see cref="T:ICloneable`1"/> interface.
	/// </summary>
	public abstract class CloneBase<TClone> : ICloneable<TClone> {
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:CloneBase`1"/> class.
		/// </summary>
		protected CloneBase () {
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
		public abstract TClone Clone ();
		#endregion
		#region ICloneable implementation
		/// <summary>
		/// Creates a new object that is a copy of the current instance.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		object ICloneable.Clone () {
			return this.Clone ();
		}
		#endregion
	}
}

