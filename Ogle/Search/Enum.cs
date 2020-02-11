// Enum.cs
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
using System.Collections.Generic;
using System.Drawing;
using nDir;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Represents a square within a die sequence, and produces additional
	/// instances allowing the client to enumerate sequences depending from that
	/// square.
	///
	/// New enumerations are started with <see cref="tqEnum(tqBoard, Point)"/>.
	/// They are continued with <see cref="tqEnum.Next"/>.
	/// </summary>
	public class tqEnum {
		// Many instances are allocated as the board is enumerated. It would be
		// faster to make instances reusable, and allocate them within a fixed
		// pool; one instance per square would accommodate any enumeration. This
		// method seems fast enough for now, however. [optimize]
		
		/// <summary>
		/// The board being enumerated.
		/// </summary>
		readonly tqBoard eqBoard;
		
		/// <summary>
		/// The square covered by this instance.
		/// </summary>
		readonly Point eSq;
		
		/// <summary>
		/// The text selected by this instance.
		/// </summary>
		public string qTextDie { get; private set; }
		
		/// <summary>
		/// The text selected by this instance and its predecessors.
		/// </summary>
		public string qTextSel { get; private set; }
		
		/// <summary>
		/// The instance preceding this one, or 'null' if this is a top-most
		/// instance.
		/// </summary>
		readonly tqEnum eqPrev;
		
		/// <summary>
		/// The index of the following die to be covered by the next call to Next.
		/// </summary>
		Int32 eIdxNext;
		
		/// <summary>
		/// Set to 'true' for each square covered by this instance or one of its
		/// predecessors.
		/// </summary>
		readonly bool[,] eqCkUse = new bool[tqMain.sWthGrid, tqMain.sHgtGrid];
		
		/// <summary>
		/// Creates a top-level instance with no predecessors.
		/// </summary>
		public tqEnum(tqBoard aqBoard, Point aSq): this(aqBoard, aSq, null) {
		}
		
		/// <summary>
		/// Creates an instance that follows the specified instance.
		/// </summary>
		/// <remarks>
		/// This constructor should not be called except by <see cref="Next"/>.
		/// </remarks>
		tqEnum(tqBoard aqBoard, Point aSq, tqEnum aqPrev) {
			aqBoard.sThrowNull("aqBoard");
			
			eqBoard = aqBoard;
			eSq = aSq;
			
			qTextDie = aqBoard[eSq.X, eSq.Y].qText.ToLower();
			if (aqPrev == null) qTextSel = qTextDie;
			else {
				aqPrev.qTextSel.sThrowNull("aqPrev.qTextSel");
				
				qTextSel = aqPrev.qTextSel + qTextDie;
			}
			
			eqPrev = aqPrev;
			eIdxNext = 0;
			
			if (aqPrev != null) {
				aqPrev.eqCkUse.sThrowNull("aqPrev.eqCkUse");
				
				Array.Copy(aqPrev.eqCkUse, eqCkUse, tqMain.sCtDie);
			}
			eqCkUse[eSq.X, eSq.Y] = true;
		}
		
		/// <summary>
		/// Creates and returns a new instance covering a square adjacent to the
		/// one covered by this instance, one not already part of the sequence,
		/// and not previously returned by this instance. If no such square
		/// exists, 'null' is returned.
		///
		/// By creating a top-level instance for some square, and then repeatedly
		/// and recursively invoking Next on that and every other instance
		/// returned by Next, all die sequences beginning with the first square
		/// can be identified.
		/// </summary>
		public tqEnum Next() {
			Point? oSqNext = eSqNext(eIdxNext++);
			if (oSqNext == null) return null;
			return new tqEnum(eqBoard, oSqNext.Value, this);
		}
		
		/// <summary>
		/// Returns the squares covered by this instance and its predecessors, in
		/// order of selection.
		/// </summary>
		public IEnumerable<Point> Sqs() {
			var oSqs = new List<Point>();
			tqEnum oq = this;
			while (oq != null) {
				oSqs.Insert(0, oq.eSq);
				oq = oq.eqPrev;
			}
			return oSqs;
		}
		
		/// <summary>
		/// Returns the first available adjacent square after skipping aIdx valid
		/// choices, starting with the square on the right, and proceding
		/// clockwise. Returns 'null' if no such square is found.
		/// </summary>
		Point? eSqNext(Int32 aIdx) {
			if (aIdx > (Int32)tDir8.NE) return null;
			
			for (tDir8 o = tDir8.E; o <= tDir8.NE; ++o) {
				Size oOff = nDir.tDir.sOff(o);
				Point oSq = eSq + oOff;
				if (tqMain.sRectGrid.Contains(oSq) && !eqCkUse[oSq.X, oSq.Y]) {
					if (aIdx < 1) return oSq;
					--aIdx;
				}
			}
			return null;
		}
	}
}
