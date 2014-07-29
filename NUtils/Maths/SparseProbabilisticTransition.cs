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
using System.Linq;
using NUtils.Collections;
using NUtils.Functional;

namespace NUtils.Maths {
	/// <summary>
	/// An implementation of the <see cref="IProbabilisticTransition"/> interface
	/// used when the number of transitions is limited (linear with the number
	/// of indices).
	/// </summary>
	public class SparseProbabilisticTransition : EnumerableBase<Tuple<int,int,double>>, IProbabilisticTransition {

		#region Fields
		/// <summary>
		/// The index array providing the locations where the arrows are stored
		/// </summary>
		private readonly int[] indices;
		/// <summary>
		/// The array of arrows that store the transition values.
		/// </summary>
		private readonly Arrow[] arrows;
		//TODO: use insert blocks
		#endregion
		#region ILength implementation
		/// <summary>
		/// Gets the number of subelements.
		/// </summary>
		/// <value>The number of indices of the transition.</value>
		public int Length {
			get {
				return this.indices.Length;
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
		/// <para>
		/// The given <paramref name="transitions"/> are not required to be ordered.
		/// </para>
		/// </remarks>
		public SparseProbabilisticTransition (int nIndex, IEnumerable<Tuple<int,int,double>> transitions) {
			int[] idcs = new int[nIndex + 0x01];
			Predicate<int> pred = PredicateUtils.RangePredicate (0x00, nIndex - 0x01);
			List<Tuple<int,int,double>> ts = transitions.Where (t => pred (t.Item1) && pred (t.Item2)).OrderBy (FunctionUtils.Identity<Tuple<int,int,double>>).ToList ();
			foreach (Tuple<int,int,double> t in ts) {
				idcs [t.Item1]++;
			}
			int na = 0x00, tmp;
			for (int i = 0x00; i < nIndex; i++) {
				tmp = idcs [i];
				idcs [i] = na;
				na += tmp;
			}
			idcs [nIndex] = na;
			Arrow[] arws = new Arrow[na];
			//TODO inject arrows
			/*foreach() {

			}*/
			this.indices = idcs;
			this.arrows = arws;
		}
		#endregion
		#region internal structures
		private struct Arrow : IComparable<Arrow> {

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
			/// Initializes a new instance of the <see cref="T:SparseProbabilisticTransition+Arrow"/> struct.
			/// </summary>
			/// <param name="target">The target index of the arrow.</param>
			/// <param name="probability">The probability of the represented transition.</param>
			public Arrow (int target, double probability = 0.0d) {
				this.Target = target;
				this.Probability = probability;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="T:SparseProbabilisticTransition+Arrow"/> struct based on
			/// a <see cref="T:Tuple`2"/> with arrow target and probability.
			/// </summary>
			/// <param name="tuple">A tuple containing the target index and the probability.</param>
			public Arrow (Tuple<int,double> tuple) : this(tuple.Item1,tuple.Item2) {
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="T:SparseProbabilisticTransition+Arrow"/> struct based
			/// on a <see cref="T:Tuple`3"/> with arrow target and probability.
			/// </summary>
			/// <param name="tuple">A tuple containing the source, target and probability index.</param>
			/// <remarks>
			/// <para>The source index is ignored.</para>
			/// </remarks>
			public Arrow (Tuple<int,int,double> tuple) : this(tuple.Item2,tuple.Item3) {
			}
			#endregion
			#region IComparable implementation
			/// <Docs>To be added.</Docs>
			/// <para>Returns the sort order of the current instance compared to the specified object.</para>
			/// <summary>
			/// Compares this instance with the given instance.
			/// </summary>
			/// <returns>A value less than zero if this instance is ordered before the given instance, a value equal
			/// to zero if both instances are equal and a value larger than zero if this instance is ordered
			/// after the given instance.</returns>
			/// <param name="other">The given object to compare this instance with.</param>
			public int CompareTo (Arrow other) {
				return this.Target - other.Target;
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

