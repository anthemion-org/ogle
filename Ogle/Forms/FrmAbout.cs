// FrmAbout.cs
// -----------
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Implements the About form.
	/// </summary>
	public partial class tqFrmAbout: tqFrmOgle {
		public tqFrmAbout(): base("about.html") {
			InitializeComponent();
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Reads the version from the running assembly and updates the version
		/// label.
		/// </summary>
		void eFrmAbout_Load(object aqSend, EventArgs aqArgs) {
			Assembly oqAssem = Assembly.GetEntryAssembly();
			AssemblyName oqNameAssem = oqAssem.GetName();
			Version oqVer = oqNameAssem.Version;
			eqLblVer.Text = "Version " + string.Format("{0}.{1}.{2}", oqVer.Major,
				oqVer.Minor, oqVer.Build);
		}
		
		/// <summary>
		/// Plays a sound when a control is moused-over.
		/// </summary>
		//
		// I tried defining this in tqFrmOgle, but the designer complained:
		void eCtl_MouseEnter(object aqSend, EventArgs aqArgs) {
			cqMgrSound.MouseOver_Play();
		}
		
		/// <summary>
		/// Closes the form.
		/// </summary>
		void eBtnOK_Click(object aqSend, EventArgs aqArgs) {
			Close();
		}
		
		/// <summary>
		/// Visits the anthemion website.
		/// </summary>
		void eLblSite_LinkClicked(object aqSend, LinkLabelLinkClickedEventArgs
			aqArgs) {
			
			Process.Start("http://www.anthemion.org");
		}
		
		/// <summary>
		/// Opens the local copy of the GPL.
		/// </summary>
		void eLblLicense_LinkClicked(object aqSend, LinkLabelLinkClickedEventArgs
			aqArgs) {
			
			String oqName = "GNU General Public License.txt";
			if (File.Exists(oqName)) Process.Start(oqName);
			else
				MessageBox.Show("License file not found", "Ogle", MessageBoxButtons
					.OK, MessageBoxIcon.Error);
		}
		
		/// <summary>
		/// Creates an e-mail addressed to the support account.
		/// </summary>
		void eLblMail_LinkClicked(object aqSend, LinkLabelLinkClickedEventArgs
			aqArgs) {
			
			Process.Start("mailto:support@anthemion.org");
		}
		
		/// <summary>
		/// Visits the SCOWL website.
		/// </summary>
		void eLblSCOWL_LinkClicked(object aqSend, LinkLabelLinkClickedEventArgs
			aqArgs) {
			
			Process.Start("http://wordlist.sourceforge.net/");
		}

		private void eqLblInno_LinkClicked(object aqSend,
			LinkLabelLinkClickedEventArgs aqArgs) {
			
			Process.Start("http://www.jrsoftware.org/isinfo.php");
		}

		private void eqLblISTool_LinkClicked(object aqSend,
			LinkLabelLinkClickedEventArgs aqArgs) {
			
			Process.Start("http://sourceforge.net/projects/istool/");
		}

	}
}
