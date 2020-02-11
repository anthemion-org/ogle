// MgrSearch.cs
// ------------
// Copyright ©2011 Jeremy Kelly
// Distributed under the terms of the GNU General Public License
// www.anthemion.org
// -----------------
// This file is part of Ogle.
//
// Ogle is free software: you can redistribute it and/or modify it under the
// terms of the GNU General Public License as published by the Free Software
// Foundation, either version 3 of the License, or (at your option) any later
// version.
//
// Ogle is distributed in the hope that it will be useful, but WITHOUT ANY
// WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
//
// You should have received a copy of the GNU General Public License along
// with Ogle. If not, see <http://www.gnu.org/licenses/>.
// -----------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Manages a work thread that finds and stores all die sequences in some
	/// board matching word entries with a given erudition level.
	/// </summary>
	/// <remarks>
	/// If the same word appears more than once in the board, it will be
	/// recorded more than once.
	///
	/// Earlier versions of the Ogle lacked the density setting, so boards were
	/// searched while the user played, not before. Because they are now
	/// searched in advance, the worker thread is not needed. It is retained,
	/// however, in case future versions pre-select and cache boards in the
	/// background.
	/// </remarks>
	public class tqMgrSearch: IEnumerable<tqSel> {
		/// <summary>
		/// The board being searched.
		/// </summary>
		readonly tqBoard eqBoard;
		/// <summary>
		/// The lexicon used to match words during the search.
		/// </summary>
		readonly tqLex eqLex;
		/// <summary>
		/// The maximum allowable erudition level. Lexicon entries with greater
		/// values are ignored.
		/// </summary>
		readonly Int32 eErud;
		
		/// <summary>
		/// The word entries identified by this instance.
		/// </summary>
		readonly List<tqSel> eqSels = new List<tqSel>();
		
		/// <summary>
		/// The instance thread lock.
		/// </summary>
		readonly object eqLock = new object();
		/// <summary>
		/// Set to 'true' if a request has been made to stop the search thread.
		/// </summary>
		bool eReqStop = false;
		/// <summary>
		/// The word thread.
		/// </summary>
		Thread eqThr;
		
		/// <summary>
		/// Returns 'true' when the work thread is complete.
		/// </summary>
		public bool Compl { get; private set; }
		
		/// <summary>
		/// Creates an instance to search aqBoard for words in aqLex with the
		/// specified erudition level.
		/// </summary>
		public tqMgrSearch(tqBoard aqBoard, tqLex aqLex, Int32 aErud) {
			aqBoard.sThrowNull("aqBoard");
			aqLex.sThrowNull("aqLex");
			
			eqBoard = aqBoard;
			eqLex = aqLex;
			eErud = aErud;
			Compl = false;
		}
		
		/// <summary>
		/// Starts the work thread.
		/// </summary>
		public void Exec() {
			eqThr = new Thread(new ThreadStart(eExec));
			eqThr.Start();
		}
		
		/// <summary>
		/// Requests that the work thread stop.
		/// </summary>
		public void Stop() {
			eReqStop = true;
		}
		
		/// <summary>
		/// Returns the number of entries found, regardless of their validity or
		/// even their uniqueness.
		/// </summary>
		public Int32 CtRaw() {
			return eqSels.Count;
		}
		
		/// <summary>
		/// Enumerates all die sequences in the board and adds word matches to the
		/// entry list.
		/// <summary>
		void eExec() {
			try {
				lock (eqLock) {
					foreach (Point oSq in tqMain.sSqsGrid()) {
						if (eReqStop) return;
						
						var oqEnum = new tqEnum(eqBoard, oSq);
						var oLook = new tLook(eqLex, eErud, oqEnum.qTextSel);
						// At this point, the board enumeration contains only one die, so
						// it cannot generate a valid entry. Lookup instances borrow
						// search window values from their predecessors, however, so
						// starting the search here will save a few slices as this part of
						// the board is enumerated:
						oLook.Exec(true);
						eExec(oqEnum, oLook);
					}
					Compl = true;
				}
			}
			catch (Exception aq) {
				tqMain.sExcept_Rep(aq);
			}
		}
		
		/// <summary>
		/// Enumerates all die sequences that follow the specified enumerator,
		/// and adds word matches to the entry list.
		/// </summary>
		/// <param name="aqEnum">
		/// The enumerator from which this search depends.
		/// </param>
		/// <param name="aLook">
		/// The lookup state used by the preceding enumerator. Reusing this state
		/// allows many search slices to be skipped as the die sequence grows.
		/// </param>
		void eExec(tqEnum aqEnum, tLook aLook) {
			aqEnum.sThrowNull("aqEnum");
			
			while (true) {
				if (eReqStop) return;
				
				tqEnum oqNext = aqEnum.Next();
				// All die sequences following this enumerator have been checked:
				if (oqNext == null) return;
				
				oqNext.qTextSel.sThrowNull("oqNext.qTextSel");
				
				tLook oLook = new tLook(aLook, oqNext.qTextSel);
				tLook.tOut oOut = oLook.Exec(true);
				
				// No words can follow this descendant of aLook, so continue with the
				// next descendant:
				if (oOut == tLook.tOut.Miss) continue;
				
				if (oqNext.qTextSel.Length >= tqMain.sLenEntMin) {
					if (oOut == tLook.tOut.Match) eAdd(oqNext);
					else {
						// A fragment was identified. The search normally stops when this
						// happens to prevent the search window from narrowing so far that
						// sequences following the fragment cannot be identified. A
						// fragment might also be a match, however, so check again without
						// stopping:
						tLook oLookFrag = new tLook(aLook, oqNext.qTextSel);
						if (oLookFrag.Exec(false) == tLook.tOut.Match) eAdd(oqNext);
					}
				}
				
				// Check the descendants of this enumerator:
				eExec(oqNext, oLook);
			}
		}
		
		/// <summary>
		/// Stores the entry covered by the specified enumeration.
		/// </summary>
		void eAdd(tqEnum aqEnum) {
			aqEnum.sThrowNull("aqEnum");
			
			var oqSel = new tqSel(eqBoard, aqEnum.Sqs());
			eqSels.Add(oqSel);
		}
		
		// IEnumerable<tqSel>
		// ------------------
		
		/// <summary>
		/// Returns an enumerator for the entry list.
		/// </summary>
		public IEnumerator<tqSel> GetEnumerator() {
			lock (eqLock) return eqSels.GetEnumerator();
		}
		
		/// <summary>
		/// Returns an enumerator for the entry list.
		/// </summary>
		//
		// Because IEnumerator<> inherits from IEnumerator, this must be
		// implemented as well:
		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
