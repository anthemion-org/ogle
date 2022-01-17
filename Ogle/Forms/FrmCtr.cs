// FrmCtr.cs
// ---------
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

using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace nOgle {
	/// <summary>
	/// Adds the center position property to the Form class.
	/// </summary>
	public class tqFrmCtr: Form {
		/// <summary>
		/// Gets and sets the center position of the form. If an attempt is made
		/// to set the center outside the display working area, it will be placed
		/// just inside instead.
		/// </summary>
		[Browsable(false)]
		public Point PosCtr {
			get {
				return new Point(Left + (Size.Width / 2), Top + (Size.Height / 2));
			}
			set {
				Rectangle oDesk = Screen.PrimaryScreen.WorkingArea;
				
				value.X -= (Size.Width / 2);
				if (value.X < 0) value.X = 0;
				else if (value.X > (oDesk.Width - Width))
					value.X = oDesk.Width - Width;
				
				value.Y -= (Size.Height / 2);
				if (value.Y < 0) value.Y = 0;
				else if (value.Y > (oDesk.Height - Height))
					value.Y = oDesk.Height - Height;
				
				Location = value;
			}
		}
	}
}
