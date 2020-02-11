// Sel.cs
// ------
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
using System.Linq;
using System.Text;
using nDir;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Represents a selection of zero or more elements within the letter grid.
	/// </summary>
	public class tqSel {
		/// <summary>
		/// The board wherein the selection is made.
		/// </summary>
		readonly tqBoard eqBoard;
		/// <summary>
		/// Returns the text selected by this instance.
		/// </summary>
		public string qText { get; private set; }
		/// <summary>
		/// Gets and sets the entry state.
		/// </summary>
		public tSt St { get; set; }
		
		/// <summary>
		/// All nodes by square, or 'null' if a given square is not selected.
		/// </summary>
		tqNode[,] eqNodesBySq;
		/// <summary>
		/// The last node selected, or 'null' if no node is selected. Nodes store
		/// references to their predecessors, so this field allows the selection
		/// to be traversed from back to front.
		/// </summary>
		tqNode eqNodeLast;
		
		/// <summary>
		/// Creates an instance containing no selection.
		/// </summary>
		public tqSel(tqBoard aqBoard) {
			aqBoard.sThrowNull("aqBoard");
			
			eqBoard = aqBoard;
			St = tSt.Valid;
			eqNodesBySq = new tqNode[tqMain.sWthGrid, tqMain.sHgtGrid];
		}
		
		/// <summary>
		/// Creates an instance selecting the specified squares.
		/// </summary>
		public tqSel(tqBoard aqBoard, IEnumerable<Point> aqSqs) {
			aqBoard.sThrowNull("aqBoard");
			aqSqs.sThrowNull("aqSqs");
			
			eqBoard = aqBoard;
			St = tSt.Valid;
			
			eqNodesBySq = new tqNode[tqMain.sWthGrid, tqMain.sHgtGrid];
			Int32 oIdx = 0;
			var oqText = new StringBuilder();
			foreach (Point oSq in aqSqs) {
				tDir8? oDir;
				if (eqNodeLast == null) oDir = null;
				else {
					var oOff = new Size(eqNodeLast.Sq.X - oSq.X, eqNodeLast.Sq.Y - oSq
						.Y);
					oDir = tDir.sDir8(oOff);
				}
				
				eqNodeLast = new tqNode(oIdx, oSq, eqNodeLast, oDir);
				eqNodesBySq[oSq.X, oSq.Y] = eqNodeLast;
				
				oqText.Append(aqBoard[oSq.X, oSq.Y].qText);
				++oIdx;
			}
			
			qText = oqText.ToString().ToLower();
		}
		
		/// <summary>
		/// Creates an instance identical to the one specified.
		/// </summary>
		public tqSel(tqSel aq) {
			aq.sThrowNull("aq");
			aq.eqBoard.sThrowNull("aq.eqBoard");
			aq.qText.sThrowNull("aq.qText");
			aq.eqNodesBySq.sThrowNull("aq.eqNodesBySq");
			
			eqBoard = aq.eqBoard;
			qText = aq.qText;
			St = aq.St;
			
			eqNodesBySq = new tqNode[tqMain.sWthGrid, tqMain.sHgtGrid];
			Array.Copy(aq.eqNodesBySq, eqNodesBySq, eqNodesBySq.Length);
			
			eqNodeLast = aq.eqNodeLast;
		}
		
		/// <summary>
		/// Returns the board associated with this instance.
		/// </summary>
		public tqBoard qBoard {
			get { return eqBoard; }
		}
		
		/// <summary>
		/// Returns 'true' if any square is selected.
		/// </summary>
		public bool Ck() {
			return (eqNodeLast != null);
		}
		
		/// <summary>
		/// Returns the number of squares selected.
		/// </summary>
		public Int32 Ct() {
			if (eqNodeLast == null) return 0;
			return eqNodeLast.Idx + 1;
		}
		
		/// <summary>
		/// Returns the score awarded for the entry.
		/// </summary>
		public Int32 Score(bool aPlay) {
			if (St == tSt.Valid) return 1;
			return 0;
		}
		
		/// <summary>
		/// Returns all selected nodes.
		/// </summary>
		//
		// tqNode instances are immutable, so this method does not violate
		// encapsulation as it might seem to do. It would be more elegant,
		// however, to return a sequence of interfaces instead: [design]
		public IEnumerable<tqNode> qNodes() {
			return tqMain.sSqsGrid()
				.Select(oq => eqNodesBySq[oq.X, oq.Y])
				.Where(oq => oq != null);
		}
		
		/// <summary>
		/// Returns all selected squares.
		/// </summary>
		public IEnumerable<Point> qSqsNode() {
			return qNodes().Select(oq => oq.Sq);
		}
		
		/// <summary>
		/// Returns 'true' if the specified square is selected.
		/// </summary>
		public bool Ck(Point aSq) {
			return eqNodesBySq[aSq.X, aSq.Y] != null;
		}
		
		/// <summary>
		/// Returns 'true' if the specified square can be added to the selection.
		/// </summary>
		public bool CkNext(Point aSq) {
			if (eqNodesBySq[aSq.X, aSq.Y] != null) return false;
			
			if (eqNodeLast == null) return true;
			Size oOff = eOffToNodeLast(aSq).Value;
			
			return (oOff.Width >= -1) && (oOff.Width <= 1)
				&& (oOff.Height >= -1) && (oOff.Height <= 1);
		}
		
		/// <summary>
		/// Adds the specified square to the selection.
		/// </summary>
		public void Add(Point aSq) {
			if (!CkNext(aSq)) throw new Exception("Invalid next square");
			
			if (eqNodeLast == null)
				eqNodeLast = new tqNode(0, aSq, eqNodeLast, null);
			else {
				Int32 oIdx = eqNodeLast.Idx + 1;
				tDir8? oDirPrev = tDir.sDir8(eOffToNodeLast(aSq).Value);
				eqNodeLast = new tqNode(oIdx, aSq, eqNodeLast, oDirPrev);
			}
			eqNodesBySq[aSq.X, aSq.Y] = eqNodeLast;
			qText = qText + eqBoard[aSq.X, aSq.Y].qText.ToLower();
		}
		
		/// <summary>
		/// Removes the specified square from the selection, along with all those
		/// that follow it.
		/// </summary>
		public void Drop(Point aSq) {
			if (eqNodesBySq[aSq.X, aSq.Y] == null) return;
			Int32 oIdx = eqNodesBySq[aSq.X, aSq.Y].Idx;
			
			// Update nodes
			// ------------
			
			eqNodeLast = null;
			for (Int32 oX = 0; oX < tqMain.sWthGrid; ++oX)
				for (Int32 oY = 0; oY < tqMain.sHgtGrid; ++oY) {
					if (eqNodesBySq[oX, oY] == null) continue;
					
					// Clear trailing nodes:
					if (eqNodesBySq[oX, oY].Idx >= oIdx) eqNodesBySq[oX, oY] = null;
					// Reset the last node:
					else if (eqNodesBySq[oX, oY].Idx == (oIdx - 1))
						eqNodeLast = eqNodesBySq[oX, oY];
				}
			
			// Update text
			// -----------
			
			var oqText = new StringBuilder();
			tqNode oqNode = eqNodeLast;
			while (oqNode != null) {
				oqText.Insert(0, eqBoard[oqNode.Sq.X, oqNode.Sq.Y].qText);
				oqNode = oqNode.qNodePrev;
			}
			qText = oqText.ToString().ToLower();
		}
		
		/// <summary>
		/// Removes all squares from the selection.
		/// </summary>
		public void Cl() {
			eqNodesBySq = new tqNode[tqMain.sWthGrid, tqMain.sHgtGrid];
			eqNodeLast = null;
			qText = "";
		}
		
		/// <summary>
		/// Returns the offset from the specified square to the last node, or
		/// 'null' if there is no selection.
		/// </summary>
		Size? eOffToNodeLast(Point aSq) {
			if (eqNodeLast == null) return null;
			return new Size(eqNodeLast.Sq.X - aSq.X, eqNodeLast.Sq.Y - aSq.Y);
		}
		
		/// <summary>
		/// Represents the scoring state of an entry.
		/// </summary>
		public enum tSt {
			/// <summary>
			/// The entry is valid.
			/// </summary>
			Valid = 0,
			/// <summary>
			/// The entry is followed by another.
			/// </summary>
			Follow,
			/// <summary>
			/// The entry is identical to another.
			/// </summary>
			Dup
		}
		
		/// <summary>
		/// Represents a single node within a selection.
		/// </summary>
		/// <remarks>
		/// I would prefer to make this a structure, but structures cannot be
		/// referenced as eqNodePrev requires.
		/// </remarks>
		public class tqNode {
			/// <summary>
			/// The node index within the selection.
			/// </summary>
			readonly Int32 eIdx;
			/// <summary>
			/// The node position within the grid.
			/// </summary>
			readonly Point eSq;
			/// <summary>
			/// The preceding node, or 'null' if this instance has no predecessor.
			/// </summary>
			readonly tqNode eqNodePrev;
			/// <summary>
			/// The direction to the preceding node, or 'null' if this instance has
			/// no predecessor.
			/// </summary>
			readonly tDir8? eDirPrev;
			
			/// <summary>
			/// The node index within the selection.
			/// </summary>
			public Int32 Idx {
				get { return eIdx; }
			}
			/// <summary>
			/// The node position within the grid.
			/// </summary>
			public Point Sq {
				get { return eSq; }
			}
			/// <summary>
			/// The preceding node, or 'null' if this instance has no predecessor.
			/// </summary>
			public tqNode qNodePrev {
				get { return eqNodePrev; }
			}
			/// <summary>
			/// The direction to the preceding node, or 'null' if this instance has
			/// no predecessor.
			/// </summary>
			public tDir8? DirPrev {
				get { return eDirPrev; }
			}
			
			/// <summary>
			/// Creates a selection node.
			/// </summary>
			/// <param name="aIdx">
			/// The node index within the selection.
			/// </param>
			/// <param name="aSq">
			/// The node position within the grid.
			/// </param>
			/// <param name="aqNodePrev">
			/// The preceding node, or 'null' if the node has no predecessor.
			/// </param>
			/// <param name="aDirPrev"
			/// The direction to the preceding node, or 'null' if the node has no
			/// predecessor.
			/// </param>
			public tqNode(Int32 aIdx, Point aSq, tqNode aqNodePrev, tDir8? aDirPrev)
			{
				eIdx = aIdx;
				eSq = aSq;
				eqNodePrev = aqNodePrev;
				eDirPrev = aDirPrev;
			}
		}
	}
}
