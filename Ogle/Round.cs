// Round.cs
// --------
// Copyright ©2022 Jeremy Kelly
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
	/// Represents one round of the game, creating and storing the board and the
	/// player score cards.
	/// </summary>
	public class tqRound {
		/// <summary>
		/// The round creation time.
		/// </summary>
		readonly DateTime eTime;
		/// <summary>
		/// The board.
		/// </summary>
		readonly tqBoard eqBoard;
		/// <summary>
		/// The player's score card.
		/// </summary>
		readonly tqCard eqCardPlay;
		/// <summary>
		/// The computer's score card.
		/// </summary>
		readonly tqCard eqCardComp;
		
		/// <summary>
		/// The round creation time.
		/// </summary>
		public DateTime Time {
			get { return eTime; }
		}
		/// <summary>
		/// The board.
		/// </summary>
		public tqBoard qBoard {
			get { return eqBoard; }
		}
		/// <summary>
		/// The player's score card.
		/// </summary>
		public tqCard qCardPlay {
			get { return eqCardPlay; }
		}
		/// <summary>
		/// The computer's score card.
		/// </summary>
		public tqCard qCardComp {
			get { return eqCardComp; }
		}
		
		/// <param name="aCeilPool">
		/// The letter pool frequency ceiling. See <see cref="tqPool.tqPool(
		/// float)"/>.
		/// </param>
		public tqRound(float aCeilPool) {
			eTime = DateTime.Now;
			eqBoard = new tqBoard(aCeilPool);
			eqCardPlay = new tqCard(true);
			eqCardComp = new tqCard(false);
		}
	}
}
