//
//  ExplicitHiddenMarkovModel.cs
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

namespace NUtils.ArtificialIntelligence {
	/// <summary>
	/// An implementation of the <see cref="IHiddenMarkovModel"/> interface
	/// where the probabilities are stored as 
	/// </summary>
	public class ExplicitHiddenMarkovModel : IHiddenMarkovModel {

		#region Fields
		/// <summary>
		/// The initial state probability distribution.
		/// </summary>
		private readonly double[] p;
		/// <summary>
		/// The transition distribution matrix.
		/// </summary>
		private readonly double[,] a;
		/// <summary>
		/// The emission distribution matrix.
		/// </summary>
		private readonly double[,] b;
		#endregion
		#region IValidateable implementation
		public bool IsValid {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
		#region ILength implementation
		/// <summary>
		/// Gets the number of hidden states of the hidden Markov model.
		/// </summary>
		/// <value>The number of hidden states of the hidden Markov model.</value>
		public int Length {
			get {
				return this.p.Length;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitHiddenMarkovModel"/> class with a given
		/// number of hidden states and output characters.
		/// </summary>
		/// <param name="nHiddenStates">The number of hidden states of the Hidden Markov Model.</param>
		/// <param name="nOutputs">The number of output characters of the Hidden Markov Model.</param>
		public ExplicitHiddenMarkovModel (int nHiddenStates, int nOutputs) {
			this.p = new double[nHiddenStates];
			this.a = new double[nHiddenStates, nHiddenStates];
			this.b = new double[nHiddenStates, nOutputs];
		}
		#endregion
		#region IFiniteDistribution implementation
		/// <summary>
		/// Get the distribution of the given <paramref name="index"/>.
		/// </summary>
		/// <returns>The distribution value of the given <paramref name="index"/>.</returns>
		/// <param name="index">The given index.</param>
		/// <remarks>
		/// <para>The returned value is always positive.</para>
		/// </remarks>
		public double GetDistributionValue (int index) {
			return this.p [index];
		}
		#endregion
	}
}

