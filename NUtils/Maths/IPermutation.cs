//
//  IPermutation.cs
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
	/// An interface representing a permutation. A permutation is permutable.
	/// </summary>
	public interface IPermutation : IPermutable, IResetable {

		/// <summary>
		/// Gets the index on which the given index maps.
		/// </summary>
		/// <param name="index">The given index.</param>
		int this [int index] {
			get;
		}

		/// <summary>
		/// Calculate a permuatation that is the oposite function of this <see cref="IPermutation"/>
		/// </summary>
		/// <returns>
		/// A <see cref="IPermutation"/> that represents the opposite permutation. In other words
		/// applying the resulting permutation on this permuation results in an identity permutation.
		/// </returns>
		IPermutation Reverse ();

		/// <summary>
		/// Calculates the reverse permutation and stores it in this <see cref="IPermutation"/> instance.
		/// </summary>
		void LocalReverse ();
	}
}

