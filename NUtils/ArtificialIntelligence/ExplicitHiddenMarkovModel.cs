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
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Factorization;
using Mono.Security.X509;

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
		#region IHiddenMarkovModel implementation
		/// <summary>
		/// Get the number of output characters of the hidden Markov model.
		/// </summary>
		/// <value>The number of output characters of the hidden Markov model.</value>
		public int OutputSize {
			get {
				return this.b.GetLength (0x01);
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitHiddenMarkovModel"/> class with a given
		/// intial state probability distribution, a transition matrix and an emission matrix.
		/// </summary>
		/// <param name="p">The given initial state distribution.</param>
		/// <param name="a">The given initial transition matrix.</param>
		/// <param name="b">The given initial emission matrix.</param>
		/// <remarks>
		/// <para>The values are not copied: modifying the given values after construction
		/// will have an impact on the hidden Markov model.</para>
		/// </remarks>
		public ExplicitHiddenMarkovModel (double[] p, double[,] a, double[,] b) {
			this.p = p;
			this.a = a;
			this.b = b;
		}

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
		/// Generates the influence matrix based on a given <see cref="T:IFiniteStateMachine`1"/> and an index
		/// that is part of a strongly connected group in the finite state machine.
		/// </summary>
		/// <returns>A 2d-matrix that contains the influence matrix of the strongly connected group.</returns>
		/// <param name="fsm">The given finite state machine to generate the influence matrix from.</param>
		/// <param name="strongIndex">The initial index to generate an influence matrix from.</param>
		/// <remarks>
		/// <para>The given initial index must be part of a strongly connected group. If the index is not part,
		/// the algorithm will loop.</para>
		/// </remarks>
		public void GenerateInfluenceMatrix (IFiniteStateMachine<int> fsm, int strongIndex) {
			int n = fsm.Length;
			int s = this.Length;
			int o = this.OutputSize;
			double[] trans = new double[s * s];
			double[,] a = this.a, b = this.b;
			double[] ch1 = new double[s], ch2 = new double[s], cht;
			int idx, otp, j, i;
			double sum;
			for (int ori = 0x00; ori < s; ori++) {
				for (j = 0x00; j < ori; j++) {
					ch2 [j] = 0.0d;
				}
				ch2 [j] = 1.0d;
				for (j++; j < s; j++) {
					ch2 [j] = 0.0d;
				}
				idx = strongIndex;
				do {
					cht = ch1;
					ch1 = ch2;
					ch2 = cht;
					otp = fsm.GetOutput (idx);
					for (j = 0x00; j < s; j++) {
						sum = 0.0d;
						for (i = 0x00; i < s; i++) {
							sum += ch1 [i] * a [i, j];
						}
						sum *= b [j, otp];
						ch2 [j] = sum;
					}
					idx = fsm.GetTransitionOfIndex (idx);
				} while(idx != strongIndex);
				for (j = 0x00, i = ori; j < s; i += s, j++) {
					trans [i] = ch2 [j];
				}
			}
			DenseMatrix m = new DenseMatrix (s, s, trans);
			MathNet.Numerics.LinearAlgebra.Matrix<double> eigen = m.Evd ().EigenVectors;
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
			int n = fsm.Length;
			int s = this.Length;
			int o = this.OutputSize;
			double[] p = this.p;
			double[,] a = this.a, b = this.b;
			int abs = (n * s) << 0x01, i, j, k, p0o, p0, p1, t;
			double[] alphabetar = new double[abs];
			for (i = 0x00; i < s; i++) {
				alphabetar [i] = p [i];
			}
			for (i = abs-s; i < abs; i--) {
				alphabetar [i] = 1.0d;
			}
			double norm, sum, ssum;
			for (k = 0x00; k < n; k++) {//TODO: extremely optimize this
				if (initialDistribution [k] > 0x00) {
					norm = 1.0d;
					p0o = 0x00;
					#region Forward algorithm
					for (t = 0x00; t < sampleLength; t++) {
						for (j = 0x00; j < s; j++) {
							sum = 0.0d;
							for (i = 0x00; i < s; i++, p0++) {
								sum += a [i, j] * alphabetar [p0];
							}
						}
					}
					#endregion
					#region Backward algorithm
					#endregion
					#region Gamma/Xi values
					#endregion
					#region Updates
					#endregion
				}
			}
			/*int[] dist, tour, init;
			double[,] trans = new double[s, s], emms = new double[s, o];
			fsm.GetStronglyConnectedGroupsDistanceTour (out dist, out tour, out init);
			int maxt = 0x00;
			for (int i = 0x00; i < n; i++) {
				if (initialDistribution [i] > 0x00) {
					maxt = Math.Max (maxt, dist [i] + tour [i]);
				}
			}
			for (int i = 0x00; i < n; i++) {
				if (initialDistribution [i] > 0x00) {

				}
			}
			double[,] alpha = new double[maxt, s], beta = new double[maxt, s];*/
		}
		#endregion
	}
}

