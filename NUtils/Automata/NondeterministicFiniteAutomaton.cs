//
//  NondeterministicFiniteAutomaton.cs
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
using NUtils.Abstract;
using NUtils.Automata;
using NUtils.Collections;
using NUtils.Functional;
using System.IO;
using System.CodeDom.Compiler;
using NUtils.Visual.GraphViz;

namespace NUtils {

	/// <summary>
	/// An implementation of the <see cref="T:INondeterministicFiniteAutomaton`2"/> interface that uses a number
	/// of nodes that are linked together.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	/// <typeparam name='TCollection'>The type of collection used to store different states given they share the same tag.</typeparam>
	public class NondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection> : CloneBase<NondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection>>, INondeterministicFiniteAutomaton<TStateTag,TEdgeTag>, IDotVisual
	    where TCollection : ICollection<IState<TStateTag,TEdgeTag>>, new() {

		#region Fields
		/// <summary>
		/// A <see cref="T:Register`2"/> that maps the <typeparamref name="TStateTag"/> instances on the <see cref="T:IState`2"/> instances.
		/// </summary>
		private readonly Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> stateDictionary = new Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> (x => x.Tag);
		/// <summary>
		/// The initial <see cref="T:IState`2"/> of this non-deterministic finite automaton.
		/// </summary>
		private IState<TStateTag,TEdgeTag> initialState;
		/// <summary>
		/// A <see cref="T:Register`2"/> that maps the <typeparamref name="TStateTag"/> instances on the accepting <see cref="T:IState`2"/> instances of this non-deterministic finite automaton.
		/// </summary>
		private readonly Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> acceptingStateDictionary = new Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> (x => x.Tag);
		#endregion
		#region INondeterministicFiniteAutomaton implementation
		/// <summary>
		/// Get the number of states in the nondeterminstic finite state automaton.
		/// </summary>
		/// <value>The number of nodes in the nondeterminstic finite state automaton.</value>
		public int NumberOfStates {
			get {
				return this.stateDictionary.Count;
			}
		}

		/// <summary>
		/// Get the number of edges in the nondeterministic finite state automaton.
		/// </summary>
		/// <value>The number of edges in the nondeterministic finite state automaton.</value>
		public int NumberOfEdges {
			get {
				return this.stateDictionary.Values.Sum (x => x.NumberOfEdges);
			}
		}

		/// <summary>
		/// Gets the tag of the initial state of this nondeterministic finite state automaton.
		/// </summary>
		/// <value>The tag corresponding with the initial state of this nondeterministic finite state automaton.</value>
		public TStateTag InitialStateTag {
			get {
				return this.initialState.Tag;
			}
		}

		/// <summary>
		/// Get the initial state of the non-deterministic finite automaton.
		/// </summary>
		/// <value>The initial <see cref="T:IState`2"/> of this non-deterministic finite automaton.</value>
		public IState<TStateTag, TEdgeTag> InitalState {
			get {
				return this.initialState;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NondeterministicFiniteAutomaton`3"/> class based on tags with a given list of <paramref name="edges"/>, the tag of the initial state (<paramref name="initialStateTag"/>) and a list of accepting states (<paramref name="acceptingStateTags"/>).
		/// </summary>
		/// <param name="edges">The list of <see cref="T:Tuple`3"/> instances representing edges to be added to the new <see cref="T:NondeterministicFiniteAutomaton`3"/>.</param>
		/// <param name="initialStateTag">The tag of the state that is the initial state.</param>
		/// <param name="acceptingStateTags">The list of tags representing the accepting states.</param>
		/// <remarks>
		/// <para>Edges with tags that are not associated with any state will result in the creation of new <see cref="T:IState`2"/> instances in this <see cref="T:NondeterministicFiniteAutomaton`3"/>.</para>
		/// <para>If the given <paramref name="initialStateTag"/> does not correspond with any registered state, a <see cref="T:IState`2"/> instance will be created.</para>
		/// <para>For each tag in the list of <paramref name="acceptingStateTags"/>, the "first" <see cref="T:IState`2"/> that matches the tag will be picked, if no <see cref="T:IState`2"/> instance matches the given tag, the tag is ignored.</para>
		/// </remarks>
		public NondeterministicFiniteAutomaton (IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialStateTag, IEnumerable<TStateTag> acceptingStateTags) : this(null,edges,initialStateTag,acceptingStateTags) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NondeterministicFiniteAutomaton`3"/> class based on tags with a given list of <paramref name="states"/>, <paramref name="edges"/>, the tag of the initial state (<paramref name="initialStateTag"/>) and a list of accepting states (<paramref name="acceptingStateTags"/>).
		/// </summary>
		/// <param name="states">The list of <see cref="T:IState`2"/> instances to be added to the new <see cref="T:NondeterministicFiniteAutomaton`3"/>.</param>
		/// <param name="edges">The list of <see cref="T:Tuple`3"/> instances representing edges to be added to the new <see cref="T:NondeterministicFiniteAutomaton`3"/>.</param>
		/// <param name="initialStateTag">The tag of the state that is the initial state.</param>
		/// <param name="acceptingStateTags">The list of tags representing the accepting states.</param>
		/// <remarks>
		/// <para>If the given list of <paramref name="states"/> is not effective, then no states are added.</para>
		/// <para>Non-effective <paramref name="states"/> will not be added to the list of states.</para>
		/// <para>Edges with tags that are not associated with any state will result in the creation of new <see cref="T:IState`2"/> instances in this <see cref="T:NondeterministicFiniteAutomaton`3"/>.</para>
		/// <para>If the given <paramref name="initialStateTag"/> does not correspond with any registered state, a <see cref="T:IState`2"/> instance will be created.</para>
		/// <para>For each tag in the list of <paramref name="acceptingStateTags"/>, the "first" <see cref="T:IState`2"/> that matches the tag will be picked, if no <see cref="T:IState`2"/> instance matches the given tag, the tag is ignored.</para>
		/// </remarks>
		public NondeterministicFiniteAutomaton (IEnumerable<IState<TStateTag,TEdgeTag>> states, IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialStateTag, IEnumerable<TStateTag> acceptingStateTags) {
			this.RegisterStates (states);
			this.RegisterEdges (edges);
			this.RegisterAcceptingStates (acceptingStateTags);
			this.initialState = this.RegisterState (initialStateTag);
		}
		#endregion
		#region INondeterministicFiniteAutomaton implementation
		/// <summary>
		/// Register a state for the given <paramref name="sateTag"/>. The new state doesn't contain any
		/// edges.
		/// </summary>
		/// <param name="fromStateTag">The tag of the state from which the edge originates.</param>
		/// <param name="edgeTag">The tag of the edge that is registered.</param>
		/// <param name="toStateTag">The tag of the state to which the edge maps.</param>
		/// <returns>A <see cref="T:IEdge`2"/> instance that is either an already registered
		/// state with the given <paramref name="stateTag"/> or a new state created.</returns>
		/// <remarks>
		/// <para>If there is no state associated with the <paramref name="fromStateTag"/> or <paramref name="toStateTag"/>,
		/// states are registered (using the <see cref="M:RegisterState"/> method) for these tags.</para>
		/// <para>If there already exists an edge between the two given states with the given tag, no additional
		/// edge is registered.</para>
		/// <para>This operation is not completely specific to the given automaton: if the state is shared with another
		/// automaton, the edge will be added to all the automata.</para>
		/// </remarks>
		public IEdge<TStateTag,TEdgeTag> RegisterEdge (TStateTag fromStateTag, TEdgeTag edgeTag, TStateTag toStateTag) {
			IState<TStateTag,TEdgeTag> frm = this.RegisterState (fromStateTag);
			IState<TStateTag,TEdgeTag> tos = this.RegisterState (toStateTag);
			foreach (IEdge<TStateTag,TEdgeTag> edge in frm.TaggedEdges (edgeTag)) {
				if (edge.Contains (tos)) {
					return edge;
				}
			}
			Edge<TStateTag,TEdgeTag> cedge = new Edge<TStateTag,TEdgeTag> (edgeTag, tos);
			frm.AddEdge (cedge);
			return cedge;
		}

		/// <summary>
		/// Register the given <paramref name="state"/> including any edges.
		/// </summary>
		/// <param name="state">The given <see cref="T:IState`2"/> that must be added.</param>
		/// <remarks>
		/// <para>If the given <paramref name="state"/> is not effective, nothing happens.</para>
		/// </remarks>
		public void RegisterState (IState<TStateTag,TEdgeTag> state) {
			if (state != null) {
				this.stateDictionary.Add (state);
			}
		}

		/// <summary>
		/// Register the given <paramref name="state"/> including any edges as an accepting state.
		/// </summary>
		/// <param name="state">The given <see cref="T:IState`2"/> that must be registered as an accepting state.</param>
		/// <returns><c>true</c> if the given <paramref name="state"/> is already registered as a state and the state
		/// is thus accepted as an accepting state; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>If the given <paramref name="state"/> is not effective, nothing happens.</para>
		/// <para>If the given <paramref name="state"/> is not registered as a state of this
		/// finite automaton, nothing happens and <c>false</c> is returned.</para>
		/// </remarks>
		public bool RegisterAcceptingState (IState<TStateTag,TEdgeTag> state) {
			if (state != null && this.stateDictionary.Contains (state)) {
				this.acceptingStateDictionary.Add (state);
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Register the first matching registered state with the given <paramref name="stateTag"/> including any edges as an accepting state.
		/// </summary>
		/// <param name="stateTag">The given tag of the state that must be registered as an accepting state.</param>
		/// <returns><c>true</c> if there is a state already registered with the given <paramref name="stateTag"/> as a
		/// state and the state is thus accepted as an accepting state; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>If no state is registered with the given <paramref name="stateTag"/> as a state of this finite automaton,
		/// nothing happens and <c>false</c> is returned.</para>
		/// </remarks>
		public bool RegisterAcceptingState (TStateTag stateTag) {
			IState<TStateTag,TEdgeTag> state;
			if (stateTag != null && this.stateDictionary.TryGetValue (stateTag, out state)) {
				this.acceptingStateDictionary.Add (state);
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Register a state for the given <paramref name="sateTag"/>. The new state doesn't contain any
		/// edges.
		/// </summary>
		/// <param name="stateTag">The tag of the new state to create.</param>
		/// <returns>A <see cref="T:IState`2"/> instance that is either an already registered
		/// state with the given <paramref name="stateTag"/> or a new state created.</returns>
		/// <remarks>
		/// <para>If there exists already a state for the given <paramref name="tag"/>, no new state is initialized.</para>
		/// </remarks>
		public IState<TStateTag,TEdgeTag> RegisterState (TStateTag stateTag) {
			IState<TStateTag,TEdgeTag> state;
			if (!this.stateDictionary.TryGetValue (stateTag, out state)) {
				state = new State<TStateTag,TEdgeTag> (stateTag);
				this.stateDictionary.Add (state);
			}
			return state;
		}

		/// <summary>
		/// Enumerate all the tags associated with the states in this nondeterministic finite state automaton.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing the tags of all the states in this nondeterministic finite state automaton.</returns>
		/// <remarks>
		/// <para>If two states share the same tag, duplicates will be enumerated.</para>
		/// </remarks>
		public IEnumerable<TStateTag> StateTags () {
			return this.stateDictionary.Values.Select (x => x.Tag);
		}

		/// <summary>
		/// Enumerate all the tags associated with the accepting states in this nondeterministic finite state automaton.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing the tags of all the accepting states in this nondeterministic finite state automaton.</returns>
		public IEnumerable<TStateTag> AcceptingStateTags () {
			return this.acceptingStateDictionary.Values.Select (x => x.Tag);
		}

		/// <summary>
		/// Enumerate all the tags of the associated edges originating form the state(s) with the given <paramref name="statetag"/>
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains the tags of all edges originating from the state(s) associated with the given state tag.</returns>
		/// <param name="statetag">The given state tag.</param>
		public IEnumerable<TEdgeTag> GetEdgeTags (TStateTag statetag) {
			return this.stateDictionary.Values.SelectMany (x => x.Edges).Select (x => x.Tag);
		}

		/// <summary>
		/// Checks if there exists at least one state in this nondeterministic finite state automaton that corresponds with
		/// the given state tag that is accepting.
		/// </summary>
		/// <returns><c>true</c> if this instance is accepting the specified statetag; otherwise, <c>false</c>.</returns>
		/// <param name="statetag">Statetag.</param>
		public bool IsAccepting (TStateTag statetag) {
			return acceptingStateDictionary.ContainsKey (statetag);
		}

		/// <summary>
		/// Check if the given <see cref="T:IState`2"/> is accepted by this non-deterministic finite automaton.
		/// </summary>
		/// <returns><c>true</c> if the given <see cref="T:IState`2"/> instance is accepting; otherwise, <c>false</c>.</returns>
		/// <param name="state">The given state to check for.</param>
		public bool IsAccepting (IState<TStateTag, TEdgeTag> state) {
			return (state != null && this.acceptingStateDictionary.Contains (new KeyValuePair<TStateTag,IState<TStateTag,TEdgeTag>> (state.Tag, state)));
		}

		/// <summary>
		/// Checks if this non-deterministic finite automaton contains the given <see cref="T:IState`2"/> instance.
		/// </summary>
		/// <returns><c>true</c>, if the given <paramref name="state"/> is part of this non-deterministic finite automaton, <c>false</c> otherwise.</returns>
		/// <param name="state">The given <see cref="T:IState`2"/> to check for.</param>
		public bool ContainsState (IState<TStateTag, TEdgeTag> state) {
			return (state != null && this.stateDictionary.Contains (new KeyValuePair<TStateTag,IState<TStateTag,TEdgeTag>> (state.Tag, state)));
		}
		#endregion
		#region IDotVisual implementation
		/// <summary>
		/// Write a GraphViz DOT Graph stream to the given <paramref name="textWriter"/> visualizing this instance.
		/// </summary>
		/// <param name="textWriter">The <see cref="T:TextWriter"/> to write this instance to.</param>
		public void WriteDotText (TextWriter textWriter) {
			DotTextWriter dtw = new DotTextWriter (textWriter);
			dtw.AddGraph ();
			int id = 0x00, idi, idj;
			Dictionary<IState<TStateTag,TEdgeTag>,int> identifiers = new Dictionary<IState<TStateTag,TEdgeTag>,int> ();
			ShapeDotAttribute sda = new ShapeDotAttribute (DoubleCircleDotShape.Instance);
			LabelDotAttribute lda;
			ICollection<IState<TStateTag,TEdgeTag>> states = (ICollection<IState<TStateTag,TEdgeTag>>)this.stateDictionary;
			string sidi, sidj;
			foreach (IState<TStateTag,TEdgeTag> state in states) {
				if (!identifiers.TryGetValue (state, out idi)) {
					idi = id++;
					identifiers.Add (state, idi);
				}
				sidi = string.Format ("n{0}", idi);
				lda = new LabelDotAttribute (state.Tag);
				if (this.acceptingStateDictionary.Contains (state)) {
					dtw.AddNode (sidi, lda, sda);
				} else {
					dtw.AddNode (sidi, lda);
				}
			}
			foreach (IState<TStateTag,TEdgeTag> fstate in states) {
				idi = identifiers [fstate];
				sidi = string.Format ("n{0}", idi);
				foreach (IEdge<TStateTag,TEdgeTag> edge in fstate.Edges) {
					lda = new LabelDotAttribute (edge.Tag);
					foreach (IState<TStateTag,TEdgeTag> tstate in edge) {
						if (identifiers.TryGetValue (tstate, out idj)) {
							sidj = string.Format ("n{0}", idj);
							dtw.AddDirectedEdge (sidi, sidj, lda);
						}
					}
				}
			}
			dtw.Close ();
		}
		#endregion
		#region INondeterministicFiniteAutomaton implementation
		/// <summary>
		/// Concatenate this nondeterministic finite automaton with the given one into a new one such that
		/// the resulting one accepts a sequence of data if and only if it can be subdivded into two parts such
		/// that the first part is accepted by this automaton and the second by the <paramref name="other"/> automaton.
		/// </summary>
		/// <param name="nullTag">An edge tag used for transitions without the need to consume (or "eat") any characters.</param>
		/// <param name="other">The second <see cref="T:INondeterministicFiniteAutomaton`2"/> in the concatenation process.</param>
		/// <remarks>
		/// <para>For some implementations, the <paramref name="nullTag"/> might be optional, in that case, any value can be passed.</para>
		/// <para>If the second automaton is not effective, this automaton will be cloned (not deeply, with the same <see cref="T:IState`2"/> instances).</para>
		/// </remarks>
		public INondeterministicFiniteAutomaton<TStateTag, TEdgeTag> Concatenate (TEdgeTag nullTag, INondeterministicFiniteAutomaton<TStateTag, TEdgeTag> other) {
			throw new NotImplementedException ();
		}
		#endregion
		#region ICloneable implementation
		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		public override NondeterministicFiniteAutomaton<TStateTag, TEdgeTag,TCollection> Clone () {
			throw new NotImplementedException ();
		}
		#endregion
		#region ICloneable implementation
		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		INondeterministicFiniteAutomaton<TStateTag, TEdgeTag> ICloneable<INondeterministicFiniteAutomaton<TStateTag, TEdgeTag>>.Clone () {
			return this.Clone ();
		}
		#endregion
	}

	/// <summary>
	/// A variant of the <see cref="T:NondeterministicFiniteAutomaton`3"/> class, but with
	/// a default type for the type of collection to hold multiple states for the same tag.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	public class NondeterministicFiniteAutomaton<TStateTag,TEdgeTag> : NondeterministicFiniteAutomaton<TStateTag,TEdgeTag,List<IState<TStateTag,TEdgeTag>>> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NondeterministicFiniteAutomaton`2"/> class based on tags with a given list of <paramref name="edges"/>, the tag of the initial state (<paramref name="initialStateTag"/>) and a list of accepting states (<paramref name="acceptingStateTags"/>).
		/// </summary>
		/// <param name="edges">The list of <see cref="T:Tuple`3"/> instances representing edges to be added to the new <see cref="T:NondeterministicFiniteAutomaton`2"/>.</param>
		/// <param name="initialStateTag">The tag of the state that is the initial state.</param>
		/// <param name="acceptingStateTags">The list of tags representing the accepting states.</param>
		/// <remarks>
		/// <para>Edges with tags that are not associated with any state will result in the creation of new <see cref="T:IState`2"/> instances in this <see cref="T:NondeterministicFiniteAutomaton`2"/>.</para>
		/// <para>If the given <paramref name="initialStateTag"/> does not correspond with any registered state, a <see cref="T:IState`2"/> instance will be created.</para>
		/// <para>For each tag in the list of <paramref name="acceptingStateTags"/>, the "first" <see cref="T:IState`2"/> that matches the tag will be picked, if no <see cref="T:IState`2"/> instance matches the given tag, the tag is ignored.</para>
		/// </remarks>
		public NondeterministicFiniteAutomaton (IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialStateTag, IEnumerable<TStateTag> acceptingStateTags) : base(edges,initialStateTag,acceptingStateTags) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NondeterministicFiniteAutomaton`2"/> class based on tags with a given list of <paramref name="states"/>, <paramref name="edges"/>, the tag of the initial state (<paramref name="initialStateTag"/>) and a list of accepting states (<paramref name="acceptingStateTags"/>).
		/// </summary>
		/// <param name="states">The list of <see cref="T:IState`2"/> instances to be added to the new <see cref="T:NondeterministicFiniteAutomaton`2"/>.</param>
		/// <param name="edges">The list of <see cref="T:Tuple`3"/> instances representing edges to be added to the new <see cref="T:NondeterministicFiniteAutomaton`2"/>.</param>
		/// <param name="initialStateTag">The tag of the state that is the initial state.</param>
		/// <param name="acceptingStateTags">The list of tags representing the accepting states.</param>
		/// <remarks>
		/// <para>Non-effective <paramref name="states"/> will not be added to the list of states.</para>
		/// <para>Edges with tags that are not associated with any state will result in the creation of new <see cref="T:IState`2"/> instances in this <see cref="T:NondeterministicFiniteAutomaton`2"/>.</para>
		/// <para>If the given <paramref name="initialStateTag"/> does not correspond with any registered state, a <see cref="T:IState`2"/> instance will be created.</para>
		/// <para>For each tag in the list of <paramref name="acceptingStateTags"/>, the "first" <see cref="T:IState`2"/> that matches the tag will be picked, if no <see cref="T:IState`2"/> instance matches the given tag, the tag is ignored.</para>
		/// </remarks>
		public NondeterministicFiniteAutomaton (IEnumerable<IState<TStateTag,TEdgeTag>> states, IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialStateTag, IEnumerable<TStateTag> acceptingStateTags) : base(states,edges, initialStateTag, acceptingStateTags) {
		}
		#endregion
	}
}

