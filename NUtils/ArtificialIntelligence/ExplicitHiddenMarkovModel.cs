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
using System.Collections.Generic;
using NUtils.Maths;

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
		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
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
			MathUtils.NextScaledDistribution (this.p);
			MathUtils.NextScaledDistribution (this.a);
			MathUtils.NextScaledDistribution (this.b);
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
		#region IHiddenMarkovModel implementation
		/// <summary>
		/// Gets the transition probability of moving from a hidden state with <paramref name="index1"/> to a hidden
		/// state with <paramref name="index2"/>.
		/// </summary>
		/// <returns>The probability of a transition from the hidden state with the first index to the hidden
		/// state with the second index.</returns>
		/// <param name="index1">The index of the initial hidden state.</param>
		/// <param name="index2">The index of the final hidden state.</param>
		public double GetTransitionProbability (int index1, int index2) {
			return this.a [index1, index2];
		}

		/// <summary>
		/// Gets the emission probability of producing the given <paramref name="output"/> given the system
		/// is in the given <paramref name="state"/>.
		/// </summary>
		/// <returns>The probability of a transition from the hidden state with the first index to the hidden
		/// state with the second index.</returns>
		/// <param name="state">The given hidden state.</param>
		/// <param name="output">The given output character.</param>
		public double GetEmissionProbability (int state, int output) {
			return this.b [state, output];
		}

		/// <summary>
		/// Train this <see cref="IHiddenMarkovModel"/> by using the given <see cref="T:IFiniteStateMachine`1"/>
		/// model. Since such finite state machine generates infinite sequences of output, training that sequence
		/// would result in an underdetermined system. The sample length must thefore be specified.
		/// </summary>
		/// <param name="fsm">The finite state machine from which the <see cref="IHiddenMarkovModel"/> learns.</param>
		/// <param name="initialDistribution"> the initial distribution for the states of the finite state machine.</param>
		/// <param name="sampleLength">The length of the samples, has impact on the trained model.</param>
		public void Train (IFiniteStateMachine<int> fsm, IList<int> initialDistribution, int sampleLength) {

		}
		#endregion
	}
}

