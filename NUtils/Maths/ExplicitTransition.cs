//
//  ExplicitTransition.cs
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
using System.Linq;
using System.Text;

namespace NUtils.Maths {
	/// <summary>
	/// A basic implementation of the <see cref="ITransition"/> interface that stores the permutation explicitly as
	/// an array. This is useful for fast item access but takes much memory.
	/// </summary>
	public class ExplicitTransition : ITransition {

		#region Fields
		/// <summary>
		/// The array containing the indices that describe the transition.
		/// </summary>
		protected readonly int[] Indices;
		#endregion
		#region ILength implementation
		/// <summary>
		/// Gets the number of subelements.
		/// </summary>
		/// <value>The length.</value>
		public int Length {
			get {
				return this.Indices.Length;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitTransition"/> class.
		/// </summary>
		/// <param name="n">The number of items over which the transition is defined.</param>
		public ExplicitTransition (int n) {
			this.Indices = new int[n];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitTransition"/> class with a given list of initial permutations.
		/// </summary>
		/// <param name="indices">The initial indices.</param>
		public ExplicitTransition (IEnumerable<int> indices) : this(indices.ToArray ()) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitTransition"/> class with a given initial permutation.
		/// </summary>
		/// <param name="indices">The initial indices.</param>
		/// <remarks>
		/// <para>The values of the given indices are not copied: modifications made to the <paramref name="indices"/>
		/// array will have effect on the permutations as well.</para>
		/// <para>Consistency is not checked: it is possible that the described permutation is not possible. The user
		/// should check this.</para>
		/// </remarks>
		public ExplicitTransition (params int[] indices) {
			this.Indices = indices;
		}
		#endregion
		#region ITransition implementation
		/// <summary>
		/// Gets the index on which the given index maps.
		/// </summary>
		/// <returns>The target index of the given source <paramref name="index"/>.</returns>
		/// <param name="index">The given index.</param>
		public int GetTransitionOfIndex (int index) {
			return this.Indices [index];
		}
		#endregion
		#region IEnumerable`1 implementation
		/// <summary>
		/// Gets the enumerator that enumerates the target indices contained in this transition function.
		/// </summary>
		/// <returns>The enumerator that enumerates the target indices contained in this transition function.</returns>
		public IEnumerator<int> GetEnumerator () {
			foreach (int i in this.Indices) {
				yield return i;
			}
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Gets the enumerator that enumerates the target indices contained in this transition function.
		/// </summary>
		/// <returns>The enumerator that enumerates the target indices contained in this transition function.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="ExplicitTransition"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="ExplicitTransition"/>.</returns>
		public override string ToString () {
			StringBuilder sb = new StringBuilder ("{");
			int[] d = this.Indices;
			int dl = d.Length;
			for (int i = 0x00; i < dl; i++) {
				sb.AppendFormat (" {0}>{1}", i, d [i]);
			}
			sb.Append (" }");
			return sb.ToString ();
		}
		#endregion
	}
}
