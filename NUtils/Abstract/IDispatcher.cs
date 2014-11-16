//
//  IDispatcher.cs
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
	/// An interface desciribing a dispatcher: an instance that provides values.
	/// </summary>
	/// <remarks>
	/// <para>The values should be unique in a weak sense: it is unlikely that in a lifetime
	/// the same values will be enumerated.</para>
	/// </remarks>
	public interface IDispatcher<out T> {

		/// <summary>
		/// Generate the next item.
		/// </summary>
		/// <returns>The next item in the given sequence.</returns>
		/// <remarks>
		/// <para>The values should be unique in a weak sense: it is unlikely that in a lifetime
		/// the same values will be enumerated.</para>
		/// </remarks>
		T Next ();
	}
}

