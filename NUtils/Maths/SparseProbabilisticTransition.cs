//
//  SparseProbabilisticTransition.cs
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
using NUtils.Collections;

namespace NUtils.Maths {
	/// <summary>
	/// An implementation of the <see cref="IProbabilisticTransition"/> interface
	/// used when the number of transitions is limited (linear with the number
	/// of indices).
	/// </summary>
	public class SparseProbabilisticTransition : EnumerableBase<Tuple<int,int,double>>, IProbabilisticTransition {

		#region Fields
		private readonly int[] indices;
		private readonly Arrow[] arrows;
		//TODO: use insert blocks
		#endregion
		#region ILength implementation
		public int Length {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="SparseProbabilisticTransition"/> class.
		/// </summary>
		/// <param name="nIndex">The number of indices in the transition.</param>
		/// <param name="transitions">A list of transitions that are stored in this <see cref="SparseProbabilisticTransition"/>.</param>
		/// <remarks>
		/// <para>
		/// Transitions from and to indices larger than or eqaul to <paramref name="nIndex"/> will be ignored
		/// as well as transitions with indices less than zero.
		/// </para>
		/// </remarks>
		public SparseProbabilisticTransition (int nIndex, IEnumerable<Tuple<int,int,double>> transitions) {
			this.indices = new int[nIndex];
			foreach (Tuple<int,int,double> t in transitions) {

			}
		}
		#endregion
		#region internal structures
		private struct Arrow {

			#region Fields
			/// <summary>
			/// The target index of the arrow.
			/// </summary>
			public readonly int Target;
			/// <summary>
			/// The probability of the transition.
			/// </summary>
			public readonly double Probability;
			#endregion
			#region Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="SparseProbabilisticTransition+Arrow"/> struct.
			/// </summary>
			/// <param name="target">The target index of the arrow.</param>
			/// <param name="probability">The probability of the represented transition.</param>
			public Arrow (int target, double probability = 0.0d) {
				this.Target = target;
				this.Probability = probability;
			}
			#endregion
		}
		#endregion
		#region IProbabilisticTransition implementation
		public IEnumerable<Tuple<int, double>> GetTransitionOfIndex (int index) {
			throw new NotImplementedException ();
		}

		public double GetTransitionProbability (int frm, int to) {
			throw new NotImplementedException ();
		}
		#endregion
		#region implemented abstract members of EnumerableBase
		/// <summary>
		/// Enumerate all tuples of the probabilistic transition. An element is a <see cref="T:Tuple`3"/> containing
		/// the original index, the target index and the probability. Probabilities of zero are possible (but not
		/// necessary).
		/// </summary>
		/// <returns>A <see cref="T:IEnumerator`1"/> containing the probabilistic transitions
		/// stored in this <see cref="SparseProbabilisticTransition"/>.</returns>
		public override IEnumerator<Tuple<int, int, double>> GetEnumerator () {
			throw new NotImplementedException ();
		}
		#endregion
	}
}

