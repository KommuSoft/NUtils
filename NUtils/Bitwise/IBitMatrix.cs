//
//  IBitMatrix.cs
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
using NUtils.Bitwise;

namespace NUtils.Bitwise {
	/// <summary>
	/// An interface specifying a twodimensionally aranged array of bits. A bitmatrix does not have
	/// to be fully modifyiable: sometimes only the upper right corner of the matrix can be set, etc.
	/// </summary>
	public interface IBitMatrix : IConflictMiner {

		/// <summary>
		/// Get the truth value at the given row and column.
		/// </summary>
		/// <returns>A <see cref="bool"/> specifying whether the value at the given <paramref name="row"/> and <paramref name="column"/> is true.</returns>
		/// <param name="row">The given row.</param>
		/// <param name="column">The given column.</param>
		bool GetTruthValue (int row, int column);

		/// <summary>
		/// Copies the row with the given index into the given vector.
		/// </summary>
		/// <param name="row">The index of the row to load.</param>
		/// <param name="vector">The given <see cref="IBitVector"/> to load the row in.</param>
		/// <param name="columnOffset">The given column at which loading begins, zero (<c>0</c>) by default.</param>
		void LoadRow (int row, IBitVector vector, int columnOffset = 0x00);
	}
}

