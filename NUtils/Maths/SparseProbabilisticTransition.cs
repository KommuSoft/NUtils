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
using Microsoft.FSharp.Math;
using NUtils.Abstract;

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
		/// Initializes a new instance of the <see cref="NUtils.Maths.SparseProbabilisticTransition"/> class.
		/// </summary>
		/// <param name="transitions">The given list of transactions to store in the <see cref="SparseProbabilisticTransition"/>.</param>
		/// <remarks>
		/// <para>Transitions from and to indices less than zero will be ignored.</para>
		/// <para>The given <paramref name="transitions"/> are not required to be ordered.</para>
		/// </remarks>
		public SparseProbabilisticTransition (IEnumerable<Tuple<int,int,double>> transitions) : this(Math.Max (transitions.Max(x => x.Item1),transitions.Max(x => x.Item2))+0x01,transitions) {
		}

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
			int[] idcs = new int[nIndex];
			Predicate<int> pred = PredicateUtils.RangePredicate (0x00, nIndex - 0x01);
			List<Tuple<int,int,double>> ts = transitions.Where (t => t.Item3 > 0.0d && pred (t.Item1) && pred (t.Item2)).OrderBy (TransitionComparator.Instance).ToList ();
			int i = 0x00, j = 0x00, k = ts.Count;
			Arrow[] arws = new Arrow[k];
			idcs [nIndex - 0x01] = k;
			foreach (Tuple<int,int,double> t in ts) {
				k = t.Item1;
				arws [i++] = new Arrow (t);
				while (j < k) {
					idcs [j++] = i;
				}
			}
			this.indices = idcs;
			this.arrows = arws;
		}
		#endregion
		#region Target expand comparer
		private class TargetExpandComparer : IExpandComparer<int,Arrow> {
			#region Static fields
			/// <summary>
			/// The one instance of the comparer generated, used to compare the target with the arrow.
			/// </summary>
			public static readonly TargetExpandComparer Instance = new TargetExpandComparer ();
			#endregion
			#region Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="T:SparseProbabilisticTransition+TargetExpandComparer"/> class.
			/// </summary>
			/// <remarks>
			/// <para>The constructor is protected such that only one instance is generated.</para>
			/// </remarks>
			private TargetExpandComparer () {
			}
			#endregion
			#region IExpandComparer implementation
			/// <summary>
			/// Compares the given key with the given target.
			/// </summary>
			/// <returns>A value larger than zero if the given <paramref name="key"/> is larger than the
			/// given <paramref name="target"/>. Zero if the given <paramref name="key"/> is equal to the given
			/// <paramref name="target"/>. A value less than zero if the given <paramref name="key"/> is smaller
			/// than the given <paramref name="target"/>.</returns>
			/// <param name="key">The given key to compare.</param>
			/// <param name="target">The given target to compare.</param>
			public int Compare (int key, Arrow target) {
				return key.CompareTo (target.Target);
			}
			#endregion
		}
		#endregion
		#region Transition comparator
		private class TransitionComparator : IComparer<Tuple<int,int,double>> {
			#region Static fields
			/// <summary>
			/// The one instance of the comparer generated, used to compare the transitions.
			/// </summary>
			public static readonly TransitionComparator Instance = new TransitionComparator ();
			#endregion
			#region Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="T:SparseProbabilisticTransition+TransitionComparator"/> class.
			/// </summary>
			/// <remarks>
			/// <para>The constructor is protected such that only one instance is generated.</para>
			/// </remarks>
			private TransitionComparator () {
			}
			#endregion
			#region IComparer implementation
			/// <summary>
			/// Compare the two given transitions.
			/// </summary>
			/// <returns>A value larger than zero if the first element is larger than the second one, zero
			/// if both values are equal and a value less zero if the first element is smaller than the second one.</returns>
			/// <param name="t1">The first transition to compare.</param>
			/// <param name="t2">The second transition to compare.</param>
			public int Compare (Tuple<int, int, double> t1, Tuple<int, int, double> t2) {
				int ia = t1.Item1;
				int ib = t2.Item1;
				if (ia != ib) {
					return ia - ib;
				} else {
					return t1.Item2 - t2.Item2;
				}
			}
			#endregion
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
		/// <summary>
		/// Gets the target indices of transitions from the given <paramref name="index"/> annotated
		/// with the probability of the transition.
		/// </summary>
		/// <returns>A list of tuples containing the target index and the probability of a transition
		/// from the given <paramref name="index"/> to the target index. Transitions with a probability
		/// of zero can be left out.
		/// </returns>
		/// <remarks>
		/// <para>
		/// All returned probabilities for a given index are larger than or equal to zero and sum
		/// up to one.
		/// </para>
		/// </remarks>
		public IEnumerable<Tuple<int, double>> GetTransitionOfIndex (int index) {
			int[] idcs = this.indices;
			Arrow[] arws = this.arrows;
			int j = 0x00, bnd = idcs [index];
			if (index > 0x00) {
				j = idcs [index - 0x01];
			}
			Arrow a;
			for (; j < bnd; j++) {
				a = arws [j];
				yield return new Tuple<int,double> (a.Target, a.Probability);
			}
		}

		/// <summary>
		/// Gets the probability of the transition from the given index <paramref name="frm"/> to the
		/// given index <paramref name="to"/>
		/// </summary>
		/// <returns>The transition probability from <paramref name="frm"/> to <paramref name="to"/>.</returns>
		/// <param name="frm">The intial index of the transition.</param>
		/// <param name="to">The final index of the transition.</param>
		/// <remarks>
		/// <para>
		/// If no transition exists from the given index <paramref name="frm"/> to the given index
		/// <paramref name="to"/>, zero (<c>0.0</c>) is returned.
		/// </para>
		/// </remarks>
		public double GetTransitionProbability (int frm, int to) {
			int[] idcs = this.indices;
			Arrow[] arws = this.arrows;
			int j = 0x00, bnd = idcs [frm] - 0x01;
			if (frm > 0x00) {
				j = idcs [frm - 0x01];
			}
			j = arrows.BinarySearch (to, TargetExpandComparer.Instance, j, bnd);
			if (j >= 0x00) {
				return arws [j].Probability;
			} else {
				return 0.0d;
			}
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
			int[] idcs = this.indices;
			Arrow[] arws = this.arrows;
			int ni = idcs.Length;
			int j = 0x00, bnd;
			Arrow a;
			for (int i = 0x00; i < ni; i++) {
				bnd = idcs [i];
				for (; j < bnd; j++) {
					a = arws [j];
					yield return new Tuple<int,int,double> (i, a.Target, a.Probability);
				}
			}
		}
		#endregion
	}
}

