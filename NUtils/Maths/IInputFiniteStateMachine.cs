//
//  IFiniteStateMachine.cs
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
	/// An interface specifying a finite state machine. A formalism of a machine that has a limited
	/// number of representable states and deterministic transitions between those states. States
	/// also have an output (in order to classify output). A regular expression can be converted
	/// to a finite state machine with booleans as output.
	/// </summary>
	public interface IInputFiniteStateMachine<in TInput,out TOutput> : ILength, IFiniteStateOutput<TOutput> {

		/// <summary>
		/// Retrieve the transition function for the given <paramref name="input"/>.
		/// </summary>
		/// <returns>The transition function for the given <paramref name="input"/>.</returns>
		/// <param name="input">The given input.</param>
		ITransition GetTransitionFunction (TInput input);
	}
}