// Pool.cs
// -------
// Copyright ©2007-2022 Jeremy Kelly
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
using System.Collections.Generic;
using System.Linq;
using nDir;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Represents a pool from which letters are drawn when populating the letter
	/// grid.
	/// </summary>
	/// <remarks>
	/// Consonants, vowels, and combinations are stored in separate sub-pools to
	/// allow their relative populations in the grid to be strictly maintained.
	/// </remarks>
	public class tqPool {
		/// <summary>
		/// The desired ratio of vowels to grid spaces.
		/// </summary>
		/// <remarks>
		/// Two-fifths seems to be the most productive ratio. More consonants might
		/// produce more interesting words, however.
		/// </remarks>
		const float eRatioVow = 9F / 25F;
		/// <summary>
		/// The desired ratio of combinations to grid spaces.
		/// </summary>
		const float eRatioComb = 0F / 25F;
		
		/// <summary>
		/// The number of vowels left to be drawn.
		/// </summary>
		Int32 eCtVow;
		/// <summary>
		/// The number of combinations left to be drawn.
		/// </summary>
		Int32 eCtComb;
		/// <summary>
		/// The number of consonants left to be drawn.
		/// </summary>
		Int32 eCtCons;
		
		/// <summary>
		/// The vowel sub-pool.
		/// </summary>
		readonly List<tqEl> eqVows = new List<tqEl>();
		/// <summary>
		/// The combination sub-pool.
		/// </summary>
		readonly List<tqEl> eqCombs = new List<tqEl>();
		/// <summary>
		/// The consonant sub-pool.
		/// </summary>
		readonly List<tqEl> eqConsi = new List<tqEl>();
		
		/// <summary>
		/// Creates a new letter pool.
		/// </summary>
		/// <param name="aCeilPool">
		/// By default, letters within the pool are distributed according to their
		/// relative frequency in the language. Setting aCeilPool to a value less
		/// than one defines a set of frequency caps which no letter in a given
		/// sub-pool may surpass. This flattens the letter distributions, creating
		/// letter grids that are less productive, but perhaps more interesting.
		/// The lower aCeilPool is set, the flatter the distributions will be.
		/// </param>
		public tqPool(float aCeilPool) {
			Int32 oCtDie = tqMain.sCtDie;
			eCtVow = (Int32)Math.Round((float)oCtDie * eRatioVow);
			eCtComb = (Int32)Math.Round((float)oCtDie * eRatioComb);
			eCtCons = oCtDie - eCtVow - eCtComb;
			
			// Relatively large pools are needed to articulate letter frequency
			// differences. Large pools make letter repetition more likely, however.
			
			eqVows.Add(new tqEl("E", 6));
			eqVows.Add(new tqEl("A", 4));
			eqVows.Add(new tqEl("O", 4));
			eqVows.Add(new tqEl("I", 3));
			eqVows.Add(new tqEl("U", 1));
			eqVows.Add(new tqEl("Y", 1));
			eFlat(eqVows, aCeilPool);
			
			eqCombs.Add(new tqEl("Ck", 1));
			eqCombs.Add(new tqEl("Nd", 1));
			eqCombs.Add(new tqEl("Ng", 1));
			eqCombs.Add(new tqEl("Th", 1));
			eFlat(eqCombs, aCeilPool);
			
			eqConsi.Add(new tqEl("T", 7));
			eqConsi.Add(new tqEl("N", 7));
			eqConsi.Add(new tqEl("S", 6));
			eqConsi.Add(new tqEl("H", 6));
			eqConsi.Add(new tqEl("R", 6));
			eqConsi.Add(new tqEl("D", 4));
			eqConsi.Add(new tqEl("L", 4));
			eqConsi.Add(new tqEl("C", 3));
			eqConsi.Add(new tqEl("M", 2));
			eqConsi.Add(new tqEl("W", 2));
			eqConsi.Add(new tqEl("F", 2));
			eqConsi.Add(new tqEl("G", 2));
			eqConsi.Add(new tqEl("P", 2));
			eqConsi.Add(new tqEl("B", 1));
			eqConsi.Add(new tqEl("V", 1));
			eqConsi.Add(new tqEl("K", 1));
			eqConsi.Add(new tqEl("J", 1));
			eqConsi.Add(new tqEl("X", 1));
			eqConsi.Add(new tqEl("Z", 1));
			// Since the combination pool is not being used, 'Qu' must be added
			// here:
			eqConsi.Add(new tqEl("Qu", 1));
			eFlat(eqConsi, aCeilPool);
		}
		
		/// <summary>
		/// Flattens the specified distribution, ensuring that no element has a
		/// count greater than the greatest count in the pool multiplied by the
		/// specified ceiling. Set aCeil to one to produce no flattening.
		/// <summary>
		void eFlat(IEnumerable<tqEl> aqEls, float aCeil) {
			aqEls.sThrowNull("aqEls");
			
			if ((aCeil <= 0) || (aCeil > 1.0F))
				throw new Exception("Ceiling out-of-range");
			
			Int32 oCtMax = aqEls.Max(oq => oq.Ct);
			oCtMax = (Int32)Math.Round((float)oCtMax * aCeil);
			foreach (tqEl oq in aqEls)
				if (oq.Ct > oCtMax) oq.Ct = oCtMax;
		}
		
		/// <summary>
		/// Removes a random entry from the specified pool and returns its text.
		/// </summary>
		string eDraw(IEnumerable<tqEl> aqEls) {
			aqEls.sThrowNull("aqEls");
			
			Int32 oCt = aqEls.Sum(oq => oq.Ct);
			if (oCt < 1) throw new Exception("No text left to draw");
			
			Int32 oIdx = tqMain.sqRnd.Next(oCt);
			foreach (tqEl oq in aqEls) {
				oIdx -= oq.Ct;
				if (oIdx < 0) {
					--oq.Ct;
					return oq.qText;
				}
			}
			throw new Exception("No text drawn");
		}
		
		/// <summary>
		/// Creates and returns a random die.
		/// </summary>
		public tDie Die() {
			// Determine whether a vowel or consonant should be drawn, ensuring that
			// the desired ratio is always obtained:
			Int32 oCtAvail = eCtVow + eCtComb + eCtCons;
			if (oCtAvail < 1) throw new Exception("No dice left to be drawn");
			
			Int32 oRoll = tqMain.sqRnd.Next(oCtAvail);
			bool oVow = (oRoll < eCtVow);
			bool oComb = (oRoll < (eCtVow + eCtComb));
			
			string oqText;
			if (oVow) {
				oqText = eDraw(eqVows);
				--eCtVow;
			}
			else if (oComb) {
				oqText = eDraw(eqCombs);
				--eCtComb;
			}
			else {
				oqText = eDraw(eqConsi);
				--eCtCons;
			}
			tDir4 oOrient = (tDir4)(tqMain.sqRnd.Next(4));
			return new tDie(oqText, oOrient);
		}
		
		/// <summary>
		/// Represents the population of one letter within some pool.
		/// </summary>
		class tqEl {
			public string qText;
			public Int32 Ct;
			
			public tqEl(string aqText, Int32 aCt) {
				qText = aqText;
				Ct = aCt;
			}
		}
	}
}
