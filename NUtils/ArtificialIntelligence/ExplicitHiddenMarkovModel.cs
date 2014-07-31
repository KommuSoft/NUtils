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
using System.IO;
using NUtils.Maths;
using MathNet.Numerics.LinearAlgebra.Double;
using NUtils.IO;
using NUtils.Collections;

namespace NUtils.ArtificialIntelligence {
	/// <summary>
	/// An implementation of the <see cref="IHiddenMarkovModel"/> interface
	/// where the probabilities are stored as 
	/// </summary>
	public class ExplicitHiddenMarkovModel : FiniteIndexDistribution, IHiddenMarkovModel, IWriteable {

		#region Fields
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
		public override bool IsValid {
			get {
				throw new NotImplementedException ();//TODO
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
				return this.Probabilities.Length;
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
		public ExplicitHiddenMarkovModel (double[] p, double[,] a, double[,] b) : base(p) {
			this.a = a;
			this.b = b;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitHiddenMarkovModel"/> class with a given
		/// number of hidden states and output characters.
		/// </summary>
		/// <param name="nHiddenStates">The number of hidden states of the Hidden Markov Model.</param>
		/// <param name="nOutputs">The number of output characters of the Hidden Markov Model.</param>
		public ExplicitHiddenMarkovModel (int nHiddenStates, int nOutputs) : base(new double[nHiddenStates]) {
			this.a = new double[nHiddenStates, nHiddenStates];
			this.b = new double[nHiddenStates, nOutputs];
			MathUtils.NextScaledDistribution (this.Probabilities);
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
		public override double GetDistributionValue (int index) {
			return this.Probabilities [index];
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
		/// <returns>The error on the training. In other words how different the original Hidden Markov model is from the
		/// given finite state machine.</returns>
		/// <param name="fsm">The finite state machine from which the <see cref="IHiddenMarkovModel"/> learns.</param>
		/// <param name="initialDistribution"> the initial distribution for the states of the finite state machine.</param>
		/// <param name="sampleLength">The length of the samples, has impact on the trained model.</param>
		public double Train (IFiniteStateMachine<int> fsm, IList<int> initialDistribution, int sampleLength) {
			int N = fsm.Length;
			int S = this.Length;
			int O = this.OutputSize;
			int T = sampleLength;
			int T1 = T - 0x01;
			double[] p = this.Probabilities;
			double[,] a = this.a, b = this.b;
			double[,] alpha = new double[T, S], beta = new double[T, S], gamma = new double[T, S], bhat = new double[S, O], ahat = new double[S, S];
			double[,,] epsilon = new double[T, S, S];
			double[] asc = new double[S], bsc = new double[S], phat = new double[S];
			for (int si = 0x00; si < S; si++) {
				beta [T1, si] = 1.0d;
			}
			double norm, sum, ssum, tmp;
			int[] os = new int[sampleLength];
			double sprob = 0.0d, prob;
			int stok = 0x00;
			for (int ni = 0x00; ni < N; ni++) {
				stok += initialDistribution [ni];
			}
			for (int ni = 0x00; ni < N; ni++) {//TODO: extremely optimize this
				int tok = initialDistribution [ni];
				if (tok > 0x00) {
					int n = ni;
					#region Forward algorithm
					prob = 1.0d;
					int o = fsm.GetOutput (n);
					os [0x00] = o;
					ssum = 0.0d;
					for (int si = 0x00; si < S; si++) {
						sum = p [si] * b [si, o];
						alpha [0x00, si] = sum;
						ssum += sum;
					}
					n = fsm.GetTransitionOfIndex (n);
					norm = 1.0d / ssum;
					for (int t = 0x00; t < T1; t++) {
						o = fsm.GetOutput (n);
						os [t + 0x01] = o;
						ssum = 0.0d;
						for (int sj = 0x00; sj < S; sj++) {
							sum = 0.0d;
							for (int si = 0x00; si < S; si++) {
								sum += a [si, sj] * alpha [t, si];
							}
							sum *= norm * b [sj, o];
							ssum += sum;
							alpha [t + 0x01, sj] = sum;
						}
						n = fsm.GetTransitionOfIndex (n);
						prob *= ssum;
						norm = 1.0d / ssum;
					}
					sprob += Math.Abs ((double)tok / stok - prob);
					#endregion
					#region Backward algorithm
					norm = 1.0d;
					for (int t = sampleLength-0x01; t > 0x00; t--) {
						o = os [t];//OST: soundtrack
						ssum = 0.0d;
						for (int si = S-0x01; si >= 0x00; si--) {
							sum = 0.0d;
							for (int sj = S-0x01; sj >= 0x00; sj--) {
								sum += a [si, sj] * beta [t, sj] * b [sj, o];
							}
							sum *= norm;
							ssum += sum;
							beta [t - 0x01, si] = sum;
						}
						norm = 1.0d / ssum;
					}
					#endregion
					#region Gamma/Xi values
					for (int t = 0x00; t < T; t++) {
						sum = 0.0d;
						o = os [t];
						for (int si = 0x00; si < S; si++) {
							tmp = alpha [t, si] * beta [t, si];
							sum += tmp;
							gamma [t, si] = tmp;
						}
						sum = 1.0d / sum;
						for (int si = 0x00; si < S; si++) {
							tmp = gamma [t, si];
							tmp *= sum;
							gamma [t, si] = tmp;
							asc [si] += tok * tmp;
							bhat [si, o] += tok * tmp;
						}
					}
					for (int si = 0x00; si < S; si++) {
						phat [si] = tok * gamma [0x00, si];
						bsc [si] += tok * gamma [T1, si];
					}
					for (int t = 0x00; t < T1; t++) {
						o = os [t + 0x01];
						for (int si = 0x00; si < S; si++) {
							sum = 0.0d;
							for (int sj = 0x00; sj < S; sj++) {
								tmp = alpha [t, si] * beta [t + 0x01, sj] * a [si, sj] * b [sj, o];
								sum += tmp;
								epsilon [t, si, sj] = tmp;
							}
							sum = gamma [t, si] / sum;
							for (int sj = 0x00; sj < S; sj++) {
								tmp = epsilon [t, si, sj];
								tmp *= sum;
								ahat [si, sj] += tok * tmp;
								epsilon [t, si, sj] = tmp;
							}
						}

					}
					#endregion
				}
			}
			#region Updates (should be moved after tokenselect)
			sum = 1.0d / stok;
			for (int si = 0x00; si < S; si++) {
				p [si] = phat [si] * stok;
				tmp = 1.0d / asc [si];
				for (int o = 0x00; o < O; o++) {
					b [si, o] = bhat [si, o] * tmp;
				}
				tmp = 1.0d / (asc [si] - bsc [si]);
				for (int sj = 0x00; sj < S; sj++) {
					a [si, sj] = ahat [si, sj] * tmp;
				}
			}
			#endregion
			return sprob / stok;
		}
		#endregion
		#region implemented abstract members of FiniteDistribution
		/// <summary>
		/// Enumerates the possible values of the domain of this instance/variable.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing all the possible values of the domain.</returns>
		public override IEnumerable<int> EnumerateDomain () {
			return EnumerableCollection.RangeEnumerable (this.Length);
		}
		#endregion
		#region IWriteable implementation
		/// <summary>
		/// Writes the content of the instance to the the given <see cref="TextWriter"/>.
		/// </summary>
		/// <param name="sw">The <see cref="TextWriter"/> to write the data to.</param>
		public void WriteToStream (TextWriter sw) {
			throw new NotImplementedException ();
		}
		#endregion
	}
}

