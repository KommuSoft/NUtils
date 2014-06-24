//
//  IConflictMiner.cs
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

namespace NUtils.Bitwise {
	/// <summary>
	/// An interface specifying a miner for conflicts. Such miner has the ability to
	/// detect conficts between indices and can return the conflict classes.
	/// </summary>
	public interface IConflictMiner {

		/// <summary>
		/// Counts the number conflict classes.
		/// </summary>
		/// <returns>The number of conflict classes.</returns>
		int CountConflictClasses ();

		/// <summary>
		/// Enumerate all the conflict classes.
		/// </summary>
		/// <returns>A list of conflict classes.</returns>
		/// <remarks>
		/// <para>A conflict class is a list of indices that have no internal conflicts.</para>
		/// <para>Modifying the resulting conflict classes is not allowed: this might
		/// result in incorrect behavior.</para>
		/// </remarks>
		IEnumerable<IEnumerable<int>> GetConflictClasses ();
	}
}

