// Board.cs
// --------
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

namespace nOgle {
	/// <summary>
	/// Represents the text, underlining, and orientation of all dice within the
	/// letter grid.
	/// </summary>
	public class tqBoard {
		/// <summary>
		/// Stores all dice in the board.
		/// </summary>
		readonly tDie[,] eqDice = new tDie[tqMain.sWthGrid, tqMain.sHgtGrid];
		/// <summary>
		/// Returns the die in the specified square.
		/// </summary>
		public tDie this[Int32 aX, Int32 aY] {
			get { return eqDice[aX, aY]; }
		}
		
		/// <summary>
		/// Creates a random board.
		/// </summary>
		/// <param name="aCeilPool">
		/// The die pool letter frequency ceiling. See <see cref="tqPool.tqPool(
		/// float)"/> for details.
		/// </param>
		public tqBoard(float aCeilPool) {
			tqPool oqPool = new tqPool(aCeilPool);
			for (Int32 oX = 0; oX < tqMain.sWthGrid; ++oX)
				for (Int32 oY = 0; oY < tqMain.sHgtGrid; ++oY)
					eqDice[oX, oY] = oqPool.Die();
		}
	}
}
