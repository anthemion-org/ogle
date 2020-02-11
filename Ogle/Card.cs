// Card.cs
// -------
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
using System.Text;
using System.Drawing;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Represents a score card, storing the entries and point summary for one
	/// player.
	/// </summary>
	public class tqCard: IEnumerable<tqSel> {
		/// <summary>
		/// Set to 'true' if this card belongs to the player.
		/// </summary>
		readonly bool ePlay;
		/// <summary>
		/// All entries made by the card's owner, whether valid or not.
		/// </summary>
		readonly List<tqSel> eqSels;
		/// <summary>
		/// The valid entry count.
		/// </summary>
		Int32 eCtVal;
		/// <summary>
		/// The score earned by this card.
		/// </summary>
		Int32 eScore;
		
		/// <param name="aPlay">
		/// Set to 'true' if this card belongs to the player.
		/// </param>
		public tqCard(bool aPlay) {
			ePlay = aPlay;
			eqSels = new List<tqSel>();
			eCtVal = 0;
			eScore = 0;
		}
		
		/// <summary>
		/// Returns 'true' if the shorter string matches the longer string to the
		/// extent of its length.
		/// </summary>
		bool eMatch(string aq0, string aq1) {
			aq0.sThrowNull("aq0");
			aq1.sThrowNull("aq1");
			
			Int32 oLen = Math.Min(aq0.Length, aq1.Length);
			return string.Compare(aq0, 0, aq1, 0, oLen) == 0;
		}
		
		/// <summary>
		/// Adds an entry, if appropriate, and returns the number of valid letters
		/// added to the card. If the entry is redundant, this number will be
		/// zero. If the entry is new, it will be the number of letters in the
		/// entry. If the entry follows an existing entry, it will be the number
		/// of letters by which the existing entry is exceeded.
		/// </summary>
		//
		// Returning the added letter count is an awkward way to manage time
		// additions. It would be better to maintain a 'time added' total for the
		// entire card, as is done with the score, and have the play form use that
		// when checking for time expiration: [design]
		public Int32 Add(tqSel aq) {
			aq.sThrowNull("aq");
			aq.qText.sThrowNull("aq.qText");
			
			Int32 oCtAdd = 0;
			bool oMatch = false;
			foreach (tqSel oqOrig in eqSels) {
				oqOrig.sThrowNull("oqOrig");
				
				if (oqOrig.St != tqSel.tSt.Valid) continue;
				
				if (eMatch(aq.qText, oqOrig.qText)) {
					oqOrig.qText.sThrowNull("oqOrig.qText");
					
					if (aq.qText.Length > oqOrig.qText.Length) {
						// The new entry follows the original:
						oqOrig.St = tqSel.tSt.Follow;
						// Since the original entry was valid, it must also be the longest
						// entry followed by the new entry, and there is no need to check
						// further:
						oCtAdd = aq.qText.Length - oqOrig.qText.Length;
					}
					// The new entry is a duplicate:
					else if (aq.qText.Length == oqOrig.qText.Length)
						aq.St = tqSel.tSt.Dup;
					// The new entry is followed:
					else aq.St = tqSel.tSt.Follow;
					
					oMatch = true;
					break;
				}
			}
			eqSels.Add(aq);
			
			if (oMatch) eScore_Upd();
			else {
				++eCtVal;
				eScore += aq.Score(ePlay);
				oCtAdd = aq.qText.Length;
			}
			
			return oCtAdd;
		}
		
		/// <summary>
		/// Recalculates the score for this card by iterating the entry list.
		/// </summary>
		void eScore_Upd() {
			eScore = 0;
			foreach (tqSel oq in eqSels) {
				oq.sThrowNull("oq");
				eScore += oq.Score(ePlay);
			}
		}
		
		/// <summary>
		/// Returns the number of valid entries.
		/// </summary>
		public Int32 CtVal() {
			return eCtVal;
		}
		
		/// <summary>
		/// Returns the total score.
		/// </summary>
		public Int32 Score() {
			return eScore;
		}
		
		// IEnumerable<tqSel>
		// ------------------
		
		/// <summary>
		/// Returns an enumerator for the entry list.
		/// </summary>
		public IEnumerator<tqSel> GetEnumerator() {
			eqSels.sThrowNull("eqSels");
			return eqSels.GetEnumerator();
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
