//
//  ICloneable.cs
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
	/// An interface describing that the instance can be cloned. This is a more generic version of
	/// the <see cref="M:Object.Clone"/> method with typed output.
	/// </summary>
	/// <typeparam name='TClone'>The type of the resulting clone, in many cases the class itself or a super class.</typeparam>
	public interface ICloneable<out TClone> : ICloneable {

		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		new TClone Clone ();
	}
}

