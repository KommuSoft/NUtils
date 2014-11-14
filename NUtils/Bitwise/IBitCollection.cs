//
//  IBitCollection.cs
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
	/// An interface specifying a collection of bits. This can be a matrix, array or any other kind of collection.
	/// </summary>
	public interface IBitCollection : IEnumerable<bool> {

		/// <summary>
		/// Get the parity of the <see cref="T:IBitCollection"/>
		/// </summary>
		/// <value><c>true</c> if the number of ones is odd; <c>false</c> otherwise.</value>
		bool Parity {
			get;
		}

		/// <summary>
		/// Get the number of ones in this <see cref="T:IBitCollection"/>.
		/// </summary>
		/// <value>The number of ones in the bit collection.</value>
		int NumberOfOnes {
			get;
		}

		/// <summary>
		/// Get the number of zeros in this <see cref="T:IBitCollection"/>.
		/// </summary>
		/// <value>The number of zeros in the bit collection.</value>
		int NumberOfZeros {
			get;
		}
	}
}

