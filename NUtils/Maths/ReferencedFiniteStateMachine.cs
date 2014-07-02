//
//  ReferencedFiniteStateMachine.cs
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
using System.Collections.Generic;
using NUtils.Maths;

namespace NUtils.Maths {
	/// <summary>
	/// An implementation of a <see cref="T:IFiniteStateMachine`1"/> that uses a <see cref="ITransition"/> function and
	/// a <see cref="T:IList`1"/> to represent the finite state machine.
	/// </summary>
	/// <remarks>
	/// <para>The references given to the constructor are not cloned: modifications to these instances will
	/// have effect on the <see cref="T:ReferencedFiniteStatedMachine`1"/> behavior.</para>
	/// </remarks>
	/// <typeparam name='TOutput'>
	/// The type of values attached to each state.
	/// </typeparam>
	/// <typeparam name='TTransition'>
	/// The type of the stored transition function.
	/// </typeparam>
	public class ReferencedFiniteStateMachine<TOutput,TTransition> : IFiniteStateMachine<TOutput>, ITransitionSensitive<TTransition> where TTransition : ITransition {

		#region Fields
		/// <summary>
		/// A list of emmision values per state.
		/// </summary>
		private readonly IList<TOutput> emission;
		#endregion
		#region ITransitionSensitive implementation
		/// <summary>
		/// Gets the transition function that is stored in the <see cref="T:ReferencedFiniteStateMachine`2"/>.
		/// </summary>
		/// <value>The transition function that is stored in the <see cref="T:ReferencedFiniteStateMachine`2"/>.</value>
		public TTransition Transition {
			get;
			private set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ReferencedFiniteStateMachine`2"/> class with the given transition and emission values.
		/// </summary>
		/// <param name="transition">The given transition function.</param>
		/// <param name="emission">The given list of emission values per state.</param>
		/// <remarks>
		/// <para>The <paramref name="transition"/> and <paramref name="emission"/> values are not copied. Modifications
		/// to the given values will have an effect on the internal working of the <see cref="T:ReferencedFiniteStateMachine`2"/>.</para>
		/// </remarks>
		public ReferencedFiniteStateMachine (TTransition transition, IList<TOutput> emission) {
			this.Transition = transition;
			this.emission = emission;
		}
		#endregion
		#region IFiniteStateOutput implementation
		/// <summary>
		/// Get the output token for the given <paramref name="state"/> index.
		/// </summary>
		/// <returns>The output toke associated with the given <paramref name="state"/> index.</returns>
		/// <param name="state">The given state index.</param>
		public TOutput GetOutput (int state) {
			return this.emission [state];
		}
		#endregion
		#region ITransition implementation
		/// <summary>
		/// Gets the transition from the index of the given state.
		/// </summary>
		/// <returns>The state to which the the given state migrates after a time tick.</returns>
		/// <param name="index">The initial state of the finite state machine.</param>
		public int GetTransitionOfIndex (int index) {
			return this.Transition.GetTransitionOfIndex (index);
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Enumerates the values of the transition function of the finite state machine.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerator`1"/> that emits the transition values of the transition function
		/// of the finite state machine.</returns>
		public IEnumerator<int> GetEnumerator () {
			return this.Transition.GetEnumerator ();
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Enumerates the values of the transition function of the finite state machine.
		/// </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator"/> that emits the transition values of the transition function
		/// of the finite state machine.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.Transition.GetEnumerator ();
		}
		#endregion
	}
}

