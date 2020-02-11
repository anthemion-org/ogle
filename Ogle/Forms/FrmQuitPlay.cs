using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace nOgle {
	/// <summary>
	/// Implements the Quit play form, used to query the player's intentions
	/// when they click the Close button on the Play form.
	/// </summary>
	public partial class tqFrmQuitPlay: tqFrmOgle {
		public tqFrmQuitPlay(): base("play.html") {
			InitializeComponent();
		}
		
		// Events
		// ------
		
		/// <summary>
		/// Plays a sound when a control is moused-over.
		/// </summary>
		//
		// I tried defining this in tqFrmOgle, but the designer complained:
		void eCtl_MouseEnter(object aqSend, EventArgs aqArgs) {
			cqMgrSound.MouseOver_Play();
		}
		
		/// <summary>
		/// Closes the form, signaling that play should resume.
		/// </summary>
		void eBtnReturn_Click(object aqSend, EventArgs aqArgs) {
			Next = tNext.Play;
			Close();
		}
		
		/// <summary>
		/// Closes the form, signaling that the round should end.
		/// </summary>
		void eBtnEndRound_Click(object aqSend, EventArgs aqArgs) {
			Next = tNext.EndRound;
			Close();
		}
		
		/// <summary>
		/// Closes the form, signaling that the application should close also.
		/// </summary>
		void eBtnQuitOgle_Click(object aqSend, EventArgs aqArgs) {
			Next = tNext.QuitOgle;
			Close();
		}
	}
}