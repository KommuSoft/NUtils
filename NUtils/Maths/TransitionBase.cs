//
//  TransitionBase.cs
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
using System.Text;

namespace NUtils.Maths {
	/// <summary>
	/// A basic implementation of the <see cref="ITransition"/> interface that provides basic implementations
	/// of some methods to ease subclassing.
	/// </summary>
	public abstract class TransitionBase : ITransition {

		#region ILength implementation
		/// <summary>
		/// Gets the number of subelements.
		/// </summary>
		/// <value>The length.</value>
		public abstract int Length {
			get;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TransitionBase"/> class.
		/// </summary>
		protected TransitionBase () {
		}
		#endregion
		#region ITransition implementation
		/// <summary>
		/// Gets the index on which the given index maps.
		/// </summary>
		/// <returns>The target index of the given source <paramref name="index"/>.</returns>
		/// <param name="index">The given index from which the transition originates.</param>
		public abstract int GetTransitionOfIndex (int index);
		#endregion
		#region IEnumerable`1 implementation
		/// <summary>
		/// Gets the enumerator that enumerates the target indices contained in this transition function.
		/// </summary>
		/// <returns>The enumerator that enumerates the target indices contained in this transition function.</returns>
		public abstract IEnumerator<int> GetEnumerator ();
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
		#region IDigraph implementation
		public virtual IEnumerable<int> GetConnectedNodes (int node) {
			throw new NotImplementedException ();
		}

		public virtual bool IsImmediatelyDirectedConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}

		public virtual bool IsDirectedConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IGraph implementation
		public virtual IEnumerable<Tuple<int, int>> GetEdges () {
			throw new NotImplementedException ();
		}

		public virtual IEnumerable<int> GetNeighbors (int node) {
			throw new NotImplementedException ();
		}

		public virtual bool ContainsLoop (int node) {
			throw new NotImplementedException ();
		}

		public virtual bool IsImmediatelyConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}

		public virtual bool IsConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="ExplicitTransition"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="ExplicitTransition"/>.</returns>
		public override string ToString () {
			StringBuilder sb = new StringBuilder ("{");
			int i = 0x00;
			foreach (int j in this) {
				sb.AppendFormat (" {0}>{1}", i, j);
				i++;
			}
			sb.Append (" }");
			return sb.ToString ();
		}
		#endregion
	}
}

