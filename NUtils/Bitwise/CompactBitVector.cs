//
//  CompactBitVector.cs
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
using NUtils.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace NUtils.Bitwise {

	/// <summary>
	/// An implementation of an <see cref="IBitVector"/>, the implementation uses <see cref="ulong"/> data in an array fashion.
	/// </summary>
	public class CompactBitVector : IBitVector {

		#region Fields
		private readonly int n;
		private readonly ulong[] data;
		#endregion
		#region Constants
		/// <summary>
		/// The number of bits stored in a single block.
		/// </summary>
		public const int BlockSize = 0x40;
		/// <summary>
		/// The 2-log of <see cref="P:BlockSize"/>, used to shift data efficiently.
		/// </summary>
		/// <remarks>
		/// <para>Given a real index, by shifting the value to the right with <see cref="P:BlockShift"/>, one gets the block index.</para>
		/// </remarks>
		public const int BlockShift = 0x06;
		/// <summary>
		/// A mask used to get the index inside a block.
		/// </summary>
		/// <remarks>Given a real index, by masking the index with <see cref="P:BlockIndexMask"/>, one gets the index of the bit in the corresponding block.</remarks>
		public const int BlockIndexMask = 0x3f;
		#endregion
		#region Protected Fields
		/// <summary>
		/// Gets the number of bits stored in the last block of the bit vector.
		/// </summary>
		/// <value>The number of bits stored in the last block of the bit vector.</value>
		public int LastN {
			get {
				return this.n & 0x3F;
			}
		}

		/// <summary>
		/// Gets a mask that only passes the relevant bits of the last block.
		/// </summary>
		/// <value>A bit mask that only passes the relevant bits of the last block.</value>
		public ulong LastMask {
			get {
				return (BitUtils.L64ULong >> (0x40 - (this.n & 0x3F)));
			}
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:System.Collections.ICollection" />.</value>
		public int Count {
			get {
				ulong[] dc = this.data;
				int dl1 = dc.Length - 0x01;
				int c = 0x00;
				for (int i = 0x00; i < dl1; i++) {
					c += BitUtils.CountBits (dc [i]);
				}
				c += BitUtils.CountBits (dc [dl1] & this.LastMask);
				return c;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region IBitVector implementation
		/// <summary>
		/// Gets or sets the value of a single bit in the bitvector.
		/// </summary>
		/// <param name="index">The given index of the bit to get or set.</param>
		public bool this [int index] {
			get {
				return (this.data [index >> 0x06] & (0x01UL << (index & 0x3f))) != 0x00;
			}
			set {
				int ii = index >> 0x06;
				ulong mask = 0x01UL << (index & 0x3f);
				if (value) {
					this.data [ii] |= mask;
				} else {
					this.data [ii] &= ~mask;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether all the bits in this <see cref="IBitVector"/> instance are set.
		/// </summary>
		/// <value><c>true</c> if all bits are set; otherwise, <c>false</c>.</value>
		public bool AllSet {
			get {
				ulong[] d = this.data;
				int l1 = d.Length - 0x01;
				int i = 0x00;
				for (; i < l1 && ~d[i] == 0x00; i++)
					;
				ulong lm = this.LastMask;
				return (i == l1 && (d [l1] & lm) == lm);
			}
		}
		#endregion
		#region ILength implementation
		/// <summary>
		/// Gets the number of subelements.
		/// </summary>
		/// <value>The length.</value>
		public int Length {
			get {
				return this.n;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CompactBitVector"/> class with a given length and a
		/// list of indices that are set.
		/// </summary>
		/// <param name="n">The number of elements in the vector.</param>
		/// <param name="indices">The list of indices to set.</param>
		/// <remarks>
		/// <para>Values lower than zero or greater than or equal to <paramref name="n"/> are ignored.</para>
		/// <para>This compact bit vector can (!) be a shadow copy, in case the <paramref name="indices"/> turn out
		/// to be a <see cref="T:CompactBitVector"/> the reference to the data is copied.</para>
		/// </remarks>
		private CompactBitVector (int n, IEnumerable<int> indices) {
			this.n = n;
			CompactBitVector cbv = indices as CompactBitVector;
			if (cbv != null) {
				this.data = cbv.data;
				return;
			}
			int nb = (n + BlockIndexMask) >> BlockShift;
			ulong[] dat = new ulong[nb];
			this.data = dat;
			IBitVector ibv = indices as IBitVector;
			if (ibv != null) {
				nb = Math.Min (nb, ibv.Length);
				for (int i = 0x00; i < nb; i++) {
					dat [i] = ibv.GetBlock64 (i);
				}
				return;
			}
			foreach (int index in indices) {
				int bi = index >> BlockShift;
				if (bi < nb) {
					dat [nb] |= 0x01UL << (index & BlockIndexMask);
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompactBitVector"/> class with a given number
		/// of items and initial data.
		/// </summary>
		/// <param name="n">The given number of elements.</param>
		/// <param name="data">The given internal data for the bit-vector.</param>
		/// <remarks>
		/// <para>The values are not copied: when modifyin the bit array, the <see cref="data"/> will be modified
		/// as well and vice versa.</para>
		/// </remarks>
		public CompactBitVector (int n, ulong[] data) {
			this.n = n;
			this.data = data;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompactBitVector"/> class with a given number
		/// of items and initial truth values.
		/// </summary>
		/// <param name="n">The given number of elements.</param>
		/// <param name="values">The initial truth values for the bit vector.</param>
		public CompactBitVector (int n, params bool[] values) : this(n,(IEnumerable<bool>) values) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompactBitVector"/> class with a given number
		/// of items and initial truth values.
		/// </summary>
		/// <param name="n">The given number of elements.</param>
		/// <param name="values">The initial truth values for the bit vector.</param>
		public CompactBitVector (int n, IEnumerable<bool> values) {
			this.n = n;
			this.data = new ulong[(n + 0x3f) >> 0x06];
			int i = 0x00;
			foreach (ulong pack in BitUtils.PackUlong (values)) {
				this.data [i++] = pack;
			}
		}
		#endregion
		#region Private utility methods
		/// <summary>
		/// Get the number of blocks required to store <paramref name="n"/> bits.
		/// </summary>
		/// <param name="n">The number of bits to store.</param>
		/// <returns>The number of blocks required to store <paramref name="n"/> bits.</returns>
		private static int GetNumberOfBlocks (int n) {
			return (n + BlockIndexMask) >> BlockShift;
		}

		/// <summary>
		/// Set the tailing bits of the last block all to zero. This is useful for some computations.
		/// </summary>
		/// <remarks>
		/// <para>Clearing the tail before any operation should not have any effect.</para>
		/// </remarks>
		private void clearTail () {
			this.data [this.data.Length - 0x01] &= this.LastMask;
		}
		#endregion
		#region Operations
		/// <summary>
		/// Computes the AND-operator of this <see cref="CompactBitVector"/> and the given <see cref="CompactBitVector"/>.
		/// </summary>
		/// <returns>An <see cref="CompactBitVector"/> instance with length the maximum of both vectors
		/// where bit <c>i</c> is the AND-operation applied to the <c>i</c>-th bit
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="CompactBitVector"/> to perform the AND-operation with.</param>
		public CompactBitVector And (CompactBitVector other) {
			ulong[] da = this.data;
			ulong[] db = other.data;
			int dal = da.Length;
			int dbl = db.Length;
			int nm = Math.Min (dal, dbl);
			ulong[] dc = new ulong[Math.Max (dal, dbl)];
			for (int i = 0x00; i < nm; i++) {
				dc [i] = da [i] & db [i];
			}
			return new CompactBitVector (Math.Max (this.n, other.n), dc);
		}

		/// <summary>
		/// Computes the OR-operator of this <see cref="CompactBitVector"/> and the given <see cref="CompactBitVector"/>.
		/// </summary>
		/// <returns>An <see cref="CompactBitVector"/> instance with length the maximum of both vectors
		/// where bit <c>i</c> is the OR-operation applied to the <c>i</c>-th bit
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="CompactBitVector"/> to perform the OR-operation with.</param>
		public CompactBitVector Or (CompactBitVector other) {
			ulong[] da = this.data;
			ulong[] db = other.data;
			int dal = da.Length;
			int dbl = db.Length;
			int nm = Math.Min (dal, dbl);
			ulong[] dc = new ulong[Math.Max (dal, dbl)];
			for (int i = 0x00; i < nm; i++) {
				dc [i] = da [i] | db [i];
			}
			return new CompactBitVector (Math.Max (this.n, other.n), dc);
		}

		/// <summary>
		/// Computes the XOR-operator of this <see cref="CompactBitVector"/> and the given <see cref="CompactBitVector"/>.
		/// </summary>
		/// <returns>An <see cref="CompactBitVector"/> instance with length the maximum of both vectors
		/// where bit <c>i</c> is the XOR-operation applied to the <c>i</c>-th bit
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="CompactBitVector"/> to perform the XOR-operation with.</param>
		public CompactBitVector Xor (CompactBitVector other) {
			ulong[] da = this.data;
			ulong[] db = other.data;
			int dal = da.Length;
			int dbl = db.Length;
			int nm = Math.Min (dal, dbl);
			ulong[] dc = new ulong[Math.Max (dal, dbl)];
			for (int i = 0x00; i < nm; i++) {
				dc [i] = da [i] ^ db [i];//TODO: end
			}
			return new CompactBitVector (Math.Max (this.n, other.n), dc);
		}

		/// <summary>
		/// Computes the NOT-operator of this <see cref="CompactBitVector"/>.
		/// </summary>
		/// <returns>An <see cref="CompactBitVector"/> instance with the same length length
		/// where bit <c>i</c> is the NOT-operation applied to the <c>i</c>-th bit
		/// of this instance.</returns>
		public CompactBitVector Not () {
			ulong[] da = this.data;
			int dal = da.Length;
			ulong[] db = new ulong[dal];
			for (int i = 0x00; i < dal; i++) {
				db [i] = ~da [i];
			}
			return new CompactBitVector (this.n, db);
		}

		/// <summary>
		/// Applies the AND-operator on this <see cref="CompactBitVector"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="CompactBitVector"/> instance to apply the AND-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:And"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		public void AndLocal (CompactBitVector other) {
			ulong[] da = this.data;
			ulong[] db = other.data;
			int dal = da.Length;
			int dbl = db.Length;
			int nm = Math.Min (dal, dbl);
			for (int i = 0x00; i < nm; i++) {
				da [i] &= db [i];//TODO: special case last item
			}
		}

		/// <summary>
		/// Applies the OR-operator on this <see cref="CompactBitVector"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="CompactBitVector"/> instance to apply the OR-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:Or"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		public void OrLocal (CompactBitVector other) {
			ulong[] da = this.data;
			ulong[] db = other.data;
			int dal = da.Length;
			int dbl = db.Length;
			int nm = Math.Min (dal, dbl);
			for (int i = 0x00; i < nm; i++) {
				da [i] |= db [i];//TODO: special case last item
			}
		}

		/// <summary>
		/// Applies the XOR-operator on this <see cref="CompactBitVector"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="CompactBitVector"/> instance to apply the XOR-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:Xor"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		public void XorLocal (CompactBitVector other) {
			ulong[] da = this.data;
			ulong[] db = other.data;
			int dal = da.Length;
			int dbl = db.Length;
			int nm = Math.Min (dal, dbl);
			for (int i = 0x00; i < nm; i++) {
				da [i] ^= db [i];//TODO: special case last item
			}
		}

		/// <summary>
		/// Applies the NOT-operator on this <see cref="CompactBitVector"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:Not"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		public void NotLocal () {
			ulong[] da = this.data;
			int dal = da.Length;
			for (int i = 0x00; i < dal; i++) {
				da [i] = ~da [i];
			}
		}
		#endregion
		#region IBitVector implementation
		IBitVector IBitwise<IBitVector>.And (IBitVector other) {
			ulong[] da = this.data;
			int dal = da.Length;
			int bn = other.Length;
			int dbl = (bn + 0x3f) >> 0x06;
			int nm = Math.Min (dal, dbl);
			ulong[] dc = new ulong[Math.Max (dal, dbl)];
			for (int i = 0x00; i < nm; i++) {
				dc [i] = da [i] & other.GetBlock64 (i);
			}
			return new CompactBitVector (Math.Max (this.n, bn), dc);
		}

		IBitVector IBitwise<IBitVector>.Or (IBitVector other) {
			ulong[] da = this.data;
			int dal = da.Length;
			int bn = other.Length;
			int dbl = (bn + 0x3f) >> 0x06;
			int nm = Math.Min (dal, dbl);
			ulong[] dc = new ulong[Math.Max (dal, dbl)];
			for (int i = 0x00; i < nm; i++) {
				dc [i] = da [i] | other.GetBlock64 (i);
			}
			return new CompactBitVector (Math.Max (this.n, bn), dc);
		}

		IBitVector IBitwise<IBitVector>.Xor (IBitVector other) {
			ulong[] da = this.data;
			int dal = da.Length;
			int bn = other.Length;
			int dbl = (bn + 0x3f) >> 0x06;
			int nm = Math.Min (dal, dbl);
			ulong[] dc = new ulong[Math.Max (dal, dbl)];
			for (int i = 0x00; i < nm; i++) {
				dc [i] = da [i] ^ other.GetBlock64 (i);
			}
			return new CompactBitVector (Math.Max (this.n, bn), dc);
		}

		IBitVector IBitwise<IBitVector>.Not () {
			return this.Not ();
		}
		#endregion
		#region ILocalBitwise implementation
		void ILocalBitwise<IBitVector>.AndLocal (IBitVector other) {
			ulong[] da = this.data;
			int dal = da.Length;
			int dbl = (other.Length + 0x3f) >> 0x06;
			int nm = Math.Min (dal, dbl);
			for (int i = 0x00; i < nm; i++) {
				da [i] &= other.GetBlock64 (i);//TODO: special case last item
			}
		}

		void ILocalBitwise<IBitVector>.OrLocal (IBitVector other) {
			ulong[] da = this.data;
			int dal = da.Length;
			int dbl = (other.Length + 0x3f) >> 0x06;
			int nm = Math.Min (dal, dbl);
			for (int i = 0x00; i < nm; i++) {
				da [i] |= other.GetBlock64 (i);//TODO: special case last item
			}
		}

		void ILocalBitwise<IBitVector>.XorLocal (IBitVector other) {
			ulong[] da = this.data;
			int dal = da.Length;
			int dbl = (other.Length + 0x3f) >> 0x06;
			int nm = Math.Min (dal, dbl);
			for (int i = 0x00; i < nm; i++) {
				da [i] ^= other.GetBlock64 (i);//TODO: special case last item
			}
		}

		void ILocalBitwise<IBitVector>.NotLocal () {
			this.NotLocal ();
		}

		/// <summary>
		/// Resets the given range of bits.
		/// </summary>
		/// <param name="lower">The lower bound of the range (inclusive).</param>
		/// <param name="upper">The upper bound of the range (inclusive).</param>
		public void ResetRange (int lower, int upper) {
			int bl = lower >> 0x06;
			int bu = upper >> 0x06;
			int il = lower & 0x3f;
			int iu = 0x3f - (upper & 0x3f);
			ulong[] d = this.data;
			ulong ml = BitUtils.L64ULong;
			ulong mu = ml;
			mu <<= il;
			ml >>= iu;
			if (bl != bu) {
				d [bl] &= ~ml;
				for (bl++; bl < bu; bl++) {//loop in between for better caching
					d [bl] = 0x00;
				}
				d [bu] &= ~mu;
			} else {
				d [bl] &= ~(ml & mu);
			}
		}

		/// <summary>
		/// Sets the given range of bits.
		/// </summary>
		/// <param name="lower">The lower bound of the range (inclusive).</param>
		/// <param name="upper">The upper bound of the range (inclusive).</param>
		public void SetRange (int lower, int upper) {
			int bl = lower >> 0x06;
			int bu = upper >> 0x06;
			int il = lower & 0x3f;
			int iu = 0x3f - (upper & 0x3f);
			ulong[] d = this.data;
			ulong mi = BitUtils.L64ULong;
			ulong ml = mi;
			ulong mu = mi;
			mu <<= il;
			ml >>= iu;
			if (bl != bu) {
				d [bl] |= ml;
				for (bl++; bl < bu; bl++) {//loop in between for better caching
					d [bl] = mi;
				}
				d [bu] |= mu;
			} else {
				d [bl] |= ml & mu;
			}
		}
		#endregion
		#region IBitVector implementation
		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Remove the specified other.
		/// </summary>
		/// <param name="other">Other.</param>
		public void Remove (IBitVector other) {
			ulong[] d = this.data;
			int k = Math.Min (d.Length, (other.Length + 0x3f) >> 0x06);
			for (int i = 0x00; i < k; i++) {
				d [i] &= ~other.GetBlock64 (i);
			}
		}

		/// <summary>
		/// Gets 64 bits all packed in one <see cref="ulong"/> of the given <paramref name="block"/> index.
		/// </summary>
		/// <returns>An <see cref="ulong"/> that contains 64 bits of the given <paramref name="block"/> index.</returns>
		/// <param name="block">The given block index.</param>
		public ulong GetBlock64 (int block) {
			return this.data [block];
		}

		/// <summary>
		/// Sets 64 bits all packed in one given <see cref="ulong"/> to the given <paramref name="block"/> index.
		/// </summary>
		/// <param name="block">The given block index.</param>
		/// <param name="data">A given 64 bit number representing the block to be set.</param>
		public void SetBlock64 (int block, ulong data) {
			this.data [block] = data;
		}

		/// <summary>
		/// Calculates the index of the lowest bit that is set with an index greater than or equal to the given
		/// <paramref name="lower"/> bound.
		/// </summary>
		/// <returns>The lowest index larger or equal than <paramref name="lower"/> of the lowest bit that is true.</returns>
		/// <param name="lower">The given lower bound on the index.</param>
		/// <remarks>
		/// <para>If no bit is set with an index larger than or equal to the given <paramref name="lower"/> bound, <c>-1</c> is returned.</para>
		/// </remarks>
		public int GetLowest (int lower = 0x00) {
			int block = lower >> 0x06;
			ulong[] data = this.data;
			int dl = data.Length, dl1 = dl - 0x01;
			ulong xi = data [block];
			ulong lm = this.LastMask;
			if (block == dl1) {
				xi &= lm;
			}
			int mod = lower & 0x3f;
			int idx = BitUtils.LowestBitIndex (xi >> mod);
			if (idx >= 0x00) {
				return mod + idx;
			}
			block++;
			while (idx != -0x01 && block < dl1) {
				idx = BitUtils.LowestBitIndex (data [block++]);
			}
			if (block == dl1) {
				idx = BitUtils.LowestBitIndex (data [block] & lm);
			}
			return idx;
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Gets the enumerator that iterates over all the indices that are set to <c>true</c>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerator`1"/> that emits the indices of the bits that are set to <c>true</c>.</returns>
		/// <remarks>
		/// <para>The behavior of the enumeration of set indices of a bitvector that is modified concurrently
		/// is unpredictable. Indices set before the currently enumerated index will not be enumerated
		/// as well as indices in the same block of the currently enumerated indices. Indices
		/// in future blocks however are somtimes enumerated.</para>
		/// </remarks>
		public IEnumerator<int> GetEnumerator () {
			ulong[] dc = this.data;
			int dl = dc.Length, dl1 = dl - 0x01, idx;
			ulong d;
			for (int i = 0x00; i < dl1; i++) {
				d = dc [i];
				if (d != 0x00) {
					idx = i << 0x06;
					do {
						if ((d & 0x01) != 0x00) {
							yield return idx;
						}
						d >>= 0x01;
						idx++;
					} while(d != 0x00);
				}
			}
			d = dc [dl1] & this.LastMask;
			idx = dl1 << 0x06;
			while (d != 0x00) {
				if ((d & 0x01) != 0x00) {
					yield return idx;
				}
				d >>= 0x01;
				idx++;
			}
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
		#region ISet implementation
		/// <summary>
		/// Add the given <paramref name="item"/> to this set.
		/// </summary>
		/// <param name="item">The item to be added.</param>
		/// <returns><c>true</c> if the given <paramref name="item"/> was not part of the <see cref="T:ISet`1"/> yet; otherwise <c>false</c>.</returns>
		bool ISet<int>.Add (int item) {
			int bi = item >> 0x06;
			ulong bo = this.data [bi];
			ulong bn = bo | (0x01UL << (item & 0x3F));
			this.data [bi] = bn;
			return (bo != bn);
		}
		#endregion
		#region ICollection implementation
		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="item">The given item to add.</param>
		public void Add (int item) {
			this.data [item >> 0x06] |= 0x01UL << (item & 0x3f);
		}

		/// <summary>
		/// Reset all bits in the <see cref="CompactBitVector"/> instance.
		/// </summary>
		public void Clear () {
			ulong[] dc = this.data;
			int nc = dc.Length;
			for (int i = 0x00; i < nc; i++) {
				dc [i] = 0x00UL;
			}
		}

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <param name="item">The given item to check for.</param>
		public bool Contains (int item) {
			return (this.data [item >> 0x06] & (0x01UL << (item & 0x3f))) != 0x00;
		}

		/// <summary>
		/// Copies all the elements of the current one-dimensional <see cref="T:System.Array" /> to the specified one-dimensional <see cref="T:System.Array" /> starting at the specified destination <see cref="T:System.Array" /> index. The index is specified as a 32-bit integer.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the current <see cref="T:System.Array" />.</param>
		/// <param name="arrayIndex">A 32-bit integer that represents the index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index" /> is less than the lower bound of <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArgumentException"><paramref name="array" /> is multidimensional.</exception>
		/// <exception cref="T:System.ArgumentException"><paramref name="index" /> is equal to or greater than the length of <paramref name="array" /> and the source <see cref="T:System.Array" /> has a length greater than 0.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the source <see cref="T:System.Array" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type of the source <see cref="T:System.Array" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		/// <exception cref="T:System.RankException">The source <see cref="T:System.Array" /> is multidimensional.</exception>
		/// <exception cref="T:System.InvalidCastException">At least one element in the source <see cref="T:System.Array" /> cannot be cast to the type of destination <paramref name="array" />.</exception>
		public void CopyTo (int[] array, int arrayIndex) {
			int na = array.Length;
			IEnumerator<int> en = this.GetEnumerator ();
			for (int j = arrayIndex; j < na && en.MoveNext (); j++) {
				array [j] = en.Current;
			}
		}

		/// <summary>
		/// Removes the first occurrence of an item from the current collection.
		/// </summary>
		/// <param name="item">The item to remove from the current collection.</param>
		/// <returns><c>true</c> if this collection contained the <paramref name="item"/>; otherwise <c>false</c>.</returns>
		public bool Remove (int item) {
			int ii = item >> 0x06;
			ulong[] da = this.data;
			ulong dat = da [ii];
			ulong dau = dat & (~(0x01UL << (item & 0x3f)));
			da [ii] = dau;
			return dat != dau;
		}
		#endregion
		#region Equals and GetHashCode method
		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="CompactBitVector"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="CompactBitVector"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="CompactBitVector"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (object obj) {
			IBitVector ibv = obj as IBitVector;
			if (this.Length == ibv.Length) {
				ulong[] d = this.data;
				int dl1 = d.Length - 0x01;
				for (int i = 0x00; i < dl1; i++) {
					if (d [i] != ibv.GetBlock64 (i)) {
						return false;
					}
				}
				ulong mask = this.LastMask;
				return ((d [dl1] & mask) == (ibv.GetBlock64 (dl1) & mask));
			} else {
				return false;
			}
		}

		/// <summary>
		/// Serves as a hash function for a <see cref="CompactBitVector"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode () {
			int hash = this.Length.GetHashCode ();
			ulong[] d = this.data;
			int dl = d.Length;
			for (int i = 0x00; i < dl; i++) {
				hash *= 0x03;
				hash ^= d [i].GetHashCode ();
			}
			return hash;
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="CompactBitVector"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="CompactBitVector"/>.</returns>
		/// <remarks>
		/// <para>A bit string is represented as a sequence of zeros (<c>0</c>) and ones (<c>1</c>). The bits are grouped in blocks of 64 bits
		/// separated by spaces.</para>
		/// </remarks>
		public override string ToString () {
			StringBuilder sb = new StringBuilder ();
			ulong[] d = this.data;
			int dl1 = d.Length - 0x01;
			for (int i = 0x00; i < dl1; i++) {
				BitUtils.PrintBitString (sb, d [i]);
				sb.Append (' ');
			}
			BitUtils.PrintBitString (sb, d [dl1], this.LastN);
			return sb.ToString ();
		}
		#endregion
		#region ILowest implementation
		/// <summary>
		/// Returns the lowest element contained in this instance.
		/// </summary>
		/// <returns>The lowest element contained in this instance.</returns>
		int ILowest<int>.GetLowest () {
			return this.GetLowest ();
		}
		#endregion
		#region ILowerEnumerable implementation
		/// <summary>
		/// Get an <see cref="T:IEnumerator`1"/> that enumerates all items that are larger than or equal to the given
		/// <paramref name="lower"/> bound.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerator`1"/> that enumerates all items larger than or equal to the given
		/// <paramref name="lower"/> bound.</returns>
		/// <param name="lower">The given lower bound of the items to be enumerated.</param>
		/// <remarks>
		/// <para>The items are ordered in ascending order.</para>
		/// </remarks>
		public IEnumerator<int> GetEnumeratorLower (int lower = 0x00) {
			yield break;//TODO
		}
		#endregion
		#region Static constructors
		/// <summary>
		/// Generate a <see cref="CompactBitVector"/> where all bits are set.
		/// </summary>
		/// <returns>A <see cref="CompactBitVector"/> of length <paramref name="n"/> with all bits set.</returns>
		/// <param name="n">The length of the <see cref="CompactBitVector"/> to generate.</param>
		public static CompactBitVector All (int n) {
			int l = (n + 0x3f) >> 0x06;
			ulong[] d = new ulong[l];
			ulong u = BitUtils.L64ULong;
			for (int i = 0x00; i < l; i++) {
				d [i] = u;
			}
			return new CompactBitVector (n, d);
		}
		#endregion
		#region ISet implementation
		/// <summary>
		/// Removes all elements in the specified collection from the current set.
		/// </summary>
		/// <param name="other">A <see cref="T:IEnumerable`1"/> of values that must be removed.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public void ExceptWith (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			ulong[] dat = this.data;
			CompactBitVector cbv = new CompactBitVector (this.n, other);
			int nd = dat.Length;
			ulong[] dbt = cbv.data;
			for (int i = 0x00; i < nd; i++) {
				dat [i] &= ~dbt [i];
			}
		}

		/// <summary>
		/// Modifies the current set so that it contains only elements that are also in a specified collection.
		/// </summary>
		/// <param name="other">A <see cref="T:IEnumerable`1"/> of items to calculate the intersection with.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public void IntersectWith (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			ulong[] dat = this.data;
			CompactBitVector cbv = new CompactBitVector (this.n, other);
			int nd = dat.Length;
			ulong[] dbt = cbv.data;
			for (int i = 0x00; i < nd; i++) {
				dat [i] &= dbt [i];
			}
		}

		/// <summary>
		/// Determines whether the current set is a proper (strict) subset of a specified collection.
		/// </summary>
		/// <returns><c>true</c> if this instance is proper subset of the specified other; otherwise, <c>false</c>.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public bool IsProperSubsetOf (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Determines whether the current set is a proper (strict) superset of a specified collection.
		/// </summary>
		/// <returns><c>true</c> if this instance is proper superset of the specified other; otherwise, <c>false</c>.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public bool IsProperSupersetOf (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Determines whether a set is a subset of a specified collection.
		/// </summary>
		/// <returns><c>true</c> if this instance is subset of the specified other; otherwise, <c>false</c>.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public bool IsSubsetOf (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			this.clearTail ();
			ulong[] dat = this.data;
			CompactBitVector cbv = new CompactBitVector (this.n, other);
			int nd = dat.Length;
			ulong[] dbt = cbv.data;
			for (int i = 0x00; i < nd; i++) {
				if ((dat [i] & dbt [i]) != dat [i]) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Determines whether the current set is a superset of a specified collection.
		/// </summary>
		/// <returns><c>true</c> if this instance is superset of the specified other; otherwise, <c>false</c>.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public bool IsSupersetOf (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			this.clearTail ();
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Determines whether the current set overlaps with the specified collection.
		/// </summary>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public bool Overlaps (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			ulong[] dat = this.data;
			CompactBitVector cbv = new CompactBitVector (this.n, other);
			int nd = dat.Length;
			this.clearTail ();
			ulong[] dbt = cbv.data;
			for (int i = 0x00; i < nd; i++) {
				if ((dat [i] & dbt [i]) != 0x00) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Determines whether the current set and the specified collection contain the same elements.
		/// </summary>
		/// <returns><c>true</c>, if equals was set, <c>false</c> otherwise.</returns>
		/// <param name="other">The collection to compare to the current set.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public bool SetEquals (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both. 
		/// </summary>
		/// <param name="other">Other.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public void SymmetricExceptWith (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			ulong[] dat = this.data;
			CompactBitVector cbv = new CompactBitVector (this.n, other);
			int nd = dat.Length;
			ulong[] dbt = cbv.data;
			for (int i = 0x00; i < nd; i++) {
				dat [i] ^= dbt [i];
			}
		}

		/// <summary>
		/// Modifies the current set so that it contains all elements that are present in the current set, in the specified collection, or in both.
		/// </summary>
		/// <param name="other">Other.</param>
		/// <exception cref="ArgumentNullException">If <paramref name="other"/> is not effective.</exception>
		/// <remarks>
		/// <para>The operation scales linear with the number of elements in <paramref name="other"/>.</para>
		/// <para>This method ignores any duplicate elements in <paramref name="other"/>.</para>
		/// <para>If <paramref name="other"/> is a <see cref="CompactBitVector"/> or an <see cref="IBitVector"/> as well, performance will increase.</para>
		/// </remarks>
		public void UnionWith (IEnumerable<int> other) {
			if (other == null) {
				throw new ArgumentNullException ("other", "other is null");
			}
			Contract.EndContractBlock ();
			ulong[] dat = this.data;
			CompactBitVector cbv = new CompactBitVector (this.n, other);
			int nd = dat.Length;
			ulong[] dbt = cbv.data;
			for (int i = 0x00; i < nd; i++) {
				dat [i] |= dbt [i];
			}
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Adds the given <paramref name="item"/> to the current collection.
		/// </summary>
		/// <param name="item">The given item to add.</param>
		void ICollection<int>.Add (int item) {
			this.data [item >> 0x06] |= 0x01UL << (item & 0x3F);
		}
		#endregion
	}
}

