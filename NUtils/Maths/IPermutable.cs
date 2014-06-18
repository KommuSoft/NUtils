//
//  IPermutable.cs
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
using NUtils.Abstract;

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying that the content in the object can be permutated.
	/// </summary>
	public interface IPermutable : ILength {

		/// <summary>
		/// Swaps the content associated with the two given indices.
		/// </summary>
		/// <param name="i">The first index to swap.</param>
		/// <param name="j">The second index to swap.</param>
		void Swap (int i, int j);

		/// <summary>
		/// Swaps the content of the indices according to the given permutation.
		/// </summary>
		/// <param name="permutation">The given permutation that specifies how the content should be permutated.</param>
		void Swap (IPermutation permutation);
	}
}

