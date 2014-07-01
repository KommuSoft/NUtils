//
//  IFiniteStateOutput.cs
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

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying that the instance has a number of states and that each state has an output value
	/// attached to it.
	/// </summary>
	/// <typeparam name='TOutput'>
	/// The type of values attached to each state.
	/// </typeparam>
	public interface IFiniteStateOutput<out TOutput> {

		/// <summary>
		/// Get the output token for the given <paramref name="state"/> index.
		/// </summary>
		/// <returns>The output toke associated with the given <paramref name="state"/> index.</returns>
		/// <param name="state">The given state index.</param>
		TOutput GetOutput (int state);
	}
}

