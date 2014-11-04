//
//  ICompositionUtils.cs
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
using System;
using System.Diagnostics.Contracts;
using NUtils.Abstract;
using NUtils.Collections;

namespace NUtils.Designpatterns {

	/// <summary>
	/// A set of utility methods related to the composite pattern.
	/// </summary>
	public static class ICompositionUtils {

		/// <summary>
		/// Enumerate all the descendants of the given <paramref name="root"/> item of the composite pattern.
		/// </summary>
		/// <param name="root">The given root to generate the decendants from.</param>
		/// <typeparam name="TChildren">The type of the children and descendants of the <paramref name="root"/>.</typeparam>
		/// <remarks>
		/// <para>One is not considered to be a descendant from oneself. The given <paramref name="root"/> is not enumerated.</para>
		/// <para>Descendants are enumerated left-to-right and top-to-bottom: a child is generated before the enumeration
		/// of its children.</para>
		/// </remarks>
		public static IEnumerable<TChildren> Descendants<TChildren> (this TChildren root) where TChildren : IComposition<TChildren> {
			Contract.Requires (root != null);
			Contract.Ensures (Contract.Result<IEnumerable<TChildren>> () != null);
			Contract.Ensures (Contract.ForAll (Contract.Result<IEnumerable<TChildren>> (), g => g != null));
			Stack<IEnumerator<TChildren>> generationStack = new Stack<IEnumerator<TChildren>> ();
			IEnumerator<TChildren> cur;
			TChildren child;
			generationStack.Push (root.Children ().GetEnumerator ());
			do {
				cur = generationStack.Peek ();
				if (cur.MoveNext ()) {
					child = cur.Current;
					yield return child;
					child.Children ().OrNull (x => x.GetEnumerator ()).IfEffective (generationStack.Push);
				} else {
					generationStack.Pop ();
				}
			} while (generationStack.Count > 0x00);
		}

		/// <summary>
		/// Enumerate all the unique descendants of the given <paramref name="root"/> item of the composite pattern.
		/// </summary>
		/// <param name="root">The given root to generate the decendants from.</param>
		/// <typeparam name="TChildren">The type of the children and descendants of the <paramref name="root"/>.</typeparam>
		/// <remarks>
		/// <para>One is not considered to be a descendant from oneself. The given <paramref name="root"/> is not enumerated.</para>
		/// <para>Descendants are enumerated left-to-right and top-to-bottom: a child is generated before the enumeration
		/// of its children.</para>
		/// <para>All items are enumerated at their first occurence.</para>
		/// <para>This method provides additional speedup because already enumerated items are not expanded anymore.
		/// This method is thus more efficient than putting a unique constraint on the output of <see cref="M:Descendants"/></para>
		/// </remarks>
		public static IEnumerable<TChildren> UniqueDescendants<TChildren> (this TChildren root) where TChildren : IComposition<TChildren> {
			Contract.Requires (root != null);
			HashSet<TChildren> enumerated = new HashSet<TChildren> ();
			enumerated.Add (root);
			Stack<IEnumerator<TChildren>> generationStack = new Stack<IEnumerator<TChildren>> ();
			IEnumerator<TChildren> cur;
			TChildren child;
			generationStack.Push (root.Children ().GetEnumerator ());
			do {
				cur = generationStack.Peek ();
				if (cur.MoveNext ()) {
					child = cur.Current;
					if (enumerated.Add (child)) {
						yield return child;
						child.Children ().OrNull (x => x.GetEnumerator ()).IfEffective (generationStack.Push);
					}
				} else {
					generationStack.Pop ();
				}
			} while (generationStack.Count > 0x00);
		}

		/// <summary>
		/// Enumerate the closests descendants of <paramref name="root"/> that match the the given <paramref name="predicate"/>.
		/// </summary>
		/// <param name="root">The given root that provides the descendants.</param>
		/// <param name="predicate">The predicate that should be satisfied in order to return a descendant.</param>
		/// <typeparam name="TChildren">The type of elements over which the composite pattern enumerates.</typeparam>
		public static IEnumerable<TChildren> Blanket<TChildren> (this TChildren root, Predicate<TChildren> predicate) where TChildren : IComposition<TChildren> {
			Contract.Requires (root != null);
			Contract.Requires (predicate != null);
			Contract.Ensures (Contract.Result<IEnumerable<TChildren>> () != null);
			Contract.Ensures (Contract.ForAll (Contract.Result<IEnumerable<TChildren>> (), g => g != null));
			HashSet<TChildren> enumerated = new HashSet<TChildren> ();
			enumerated.Add (root);
			Stack<IEnumerator<TChildren>> generationStack = new Stack<IEnumerator<TChildren>> ();
			IEnumerator<TChildren> cur;
			TChildren child;
			generationStack.Push (root.Children ().GetEnumerator ());
			do {
				cur = generationStack.Peek ();
				if (cur.MoveNext ()) {
					child = cur.Current;
					if (enumerated.Add (child)) {
						if (predicate (child)) {
							yield return child;
						} else {
							child.Children ().OrNull (x => x.GetEnumerator ()).IfEffective (generationStack.Push);
						}
					}
				} else {
					generationStack.Pop ();
				}
			} while (generationStack.Count > 0x00);
		}

		/// <summary>
		/// Enumerate the closests descendants of <paramref name="root"/> that are of the given <paramref name="TType"/>.
		/// </summary>
		/// <param name="root">The given root that provides the descendants.</param>
		/// <typeparam name="TChildren">The type of elements over which the composite pattern enumerates.</typeparam>
		/// <typeparam name="TType">The type of elements that should be returned, specifies the type predicate.</typeparam>
		public static IEnumerable<TType> TypeBlanket<TChildren,TType> (this TChildren root)
		where TChildren : IComposition<TChildren>
		where TType : TChildren {
			Contract.Requires (root != null);
			Contract.Ensures (Contract.Result<IEnumerable<TChildren>> () != null);
			Contract.Ensures (Contract.ForAll (Contract.Result<IEnumerable<TChildren>> (), g => g != null));
			HashSet<TChildren> enumerated = new HashSet<TChildren> ();
			enumerated.Add (root);
			Stack<IEnumerator<TChildren>> generationStack = new Stack<IEnumerator<TChildren>> ();
			IEnumerator<TChildren> cur;
			TChildren child;
			generationStack.Push (root.Children ().GetEnumerator ());
			do {
				cur = generationStack.Peek ();
				if (cur.MoveNext ()) {
					child = cur.Current;
					if (child != null) {
						if (enumerated.Add (child)) {
							if (child is TType) {
								yield return (TType)child;
							} else {
								child.Children ().OrNull (x => x.GetEnumerator ()).IfEffective (generationStack.Push);
							}
						}
					}
				} else {
					generationStack.Pop ();
				}
			} while (generationStack.Count > 0x00);
		}

		/// <summary>
		/// Enumerate the closests descendants of <paramref name="root"/> that match the the given <paramref name="enumerate"/> predicate,
		/// but nodes are only expanded if the given <paramref name="expand"/> predicate.
		/// </summary>
		/// <param name="root">The given root that provides the descendants.</param>
		/// <param name="expand">A predicate thet determines whether a node will be expanded in the search of descendants.</param>
		/// <param name="enumerate">A predicate that determines whether a node wukk be ebumerates (given it is searched for).</param>
		/// <typeparam name="TChildren">The type of elements over which the composite pattern enumerates.</typeparam>
		public static IEnumerable<TChildren> Blanket<TChildren> (this TChildren root, Predicate<TChildren> expand, Predicate<TChildren> enumerate)
		where TChildren : IComposition<TChildren> {
			Contract.Requires (root != null);
			Contract.Requires (expand != null);
			Contract.Requires (enumerate != null);
			Contract.Ensures (Contract.Result<IEnumerable<TChildren>> () != null);
			Contract.Ensures (Contract.ForAll (Contract.Result<IEnumerable<TChildren>> (), g => g != null));
			HashSet<TChildren> enumerated = new HashSet<TChildren> ();
			enumerated.Add (root);
			Stack<IEnumerator<TChildren>> generationStack = new Stack<IEnumerator<TChildren>> ();
			IEnumerator<TChildren> cur;
			TChildren child;
			generationStack.Push (root.Children ().GetEnumerator ());
			do {
				cur = generationStack.Peek ();
				if (cur.MoveNext ()) {
					child = cur.Current;
					if (enumerated.Add (child) && expand (child)) {
						if (enumerate (child)) {
							yield return child;
						} else {
							child.Children ().OrNull (x => x.GetEnumerator ()).IfEffective (generationStack.Push);
						}
					}
				} else {
					generationStack.Pop ();
				}
			} while (generationStack.Count > 0x00);
		}

		/// <summary>
		/// Enumerate the closests descendants of <paramref name="root"/> that match the the given <paramref name="enumerate"/> predicate,
		/// but nodes are only expanded if the given <paramref name="expand"/> predicate. Descendants are enumerated with an "environment":
		/// the closest descendant of type <typeparamref name="TEnv"/> that satisfies the <paramref name="environment"/> predicate.
		/// </summary>
		/// <returns>A list of tuples of the environment instance and the associated descendant to be enumerated.</returns>
		/// <param name="root">The root from which the double blanket is calculated.</param>
		/// <param name="expand">A predicate that determines which node must be expanded.</param>
		/// <param name="enumerate">A predicate that determines which nodes must be enumerated.</param>
		/// <param name="environment">A predicate that determines whether an inode is selected as the closest environment.</param>
		/// <typeparam name="TEnv">The type of the environment, used to determine the environment "inodes".</typeparam>
		/// <typeparam name="TChild">The type of the elements to be enumerated.</typeparam>
		/// <remarks>
		/// <para>The default value of <typeparamref name="TEnv"/> is used for items where no environment is yet defined.</para>
		/// <para>The root is tested first as a potential environment.</para>
		/// <para>It is possible that an element is enumerated with an environment that is equal to itself.</para>
		/// </remarks>
		public static IEnumerable<Tuple<TEnv,TChild>> DoubleBlanket<TEnv,TChild> (this TChild root, Predicate<TChild> expand, Predicate<TChild> enumerate, Predicate<TEnv> environment)
		where TChild : IComposition<TChild>
		where TEnv : class {
			Contract.Requires (root != null);
			Contract.Requires (expand != null);
			Contract.Requires (enumerate != null);
			Contract.Requires (environment != null);
			Contract.Ensures (Contract.Result<IEnumerable<Tuple<TEnv,TChild>>> () != null);
			Contract.Ensures (Contract.ForAll (Contract.Result<IEnumerable<Tuple<TEnv,TChild>>> (), g => g != null && g.Item2 != null));
			HashSet<TChild> enumerated = new HashSet<TChild> ();
			enumerated.Add (root);
			Stack<TEnv,IEnumerator<TChild>> generationStack = new Stack<TEnv,IEnumerator<TChild>> ();
			IEnumerator<TChild> cur;
			TChild child;
			TEnv topenv = root as TEnv, env;
			if (topenv != null && !environment (topenv)) {
				topenv = null;
			}
			generationStack.Push (topenv, root.Children ().GetEnumerator ());
			do {
				cur = generationStack.Peek ().Item2;
				if (cur.MoveNext ()) {
					child = cur.Current;
					if (enumerated.Add (child) && expand (child)) {
						topenv = generationStack.SafePeek1 ();
						env = child as TEnv;
						if (env != null && environment (env)) {
							topenv = env;
						}
						if (enumerate (child)) {
							yield return null;
							yield return new Tuple<TEnv,TChild> (topenv, child);
						} else {
							child.Children ().OrNull (x => x.GetEnumerator ()).IfEffective (x => generationStack.Push (topenv, x));
						}
					}
				} else {
					generationStack.Pop ();
				}
			} while (generationStack.Count > 0x00);
		}
	}
}

