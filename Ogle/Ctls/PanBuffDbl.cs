// PanBuffDbl.cs
// -------------
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Resources;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace nOgle {
	/// <summary>
	/// Implements a panel control with double-buffered rendering.
	/// </summary>
	public class tqPanBuffDbl: Panel {
		public tqPanBuffDbl(): base() {
			DoubleBuffered = true;
		}
	}
}
