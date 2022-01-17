// MgrSound.cs
// -----------
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
using System.Windows.Forms;
using System.IO;
using Microsoft.DirectX.DirectSound;
using nMisc;

namespace nOgle {
	/// <summary>
	/// Manages sound resources, with support for asynchronous, layered
	/// playback.
	/// </summary>
	public class tqMgrSound: IDisposable {
		/// <summary>
		/// The sound folder, relative to the executable folder.
		/// </summary>
		const string eqFold = "Sound\\";
		
		/// <summary>
		/// Set to 'true' if playback is to be muted.
		/// </summary>
		public bool Mute { get; set; }
		
		/// <summary>
		/// The output volume, as a percent of maximum volume.
		/// </summary>
		Int32 ePerVol;
		/// <summary>
		/// The output volume, as a percent of maximum volume.
		/// </summary>
		public Int32 PerVol {
			get { return ePerVol; }
			set {
				if ((value < 0) || (value > 100))
					throw new Exception("Value out-of-range");
				ePerVol = value;
				
				Int32 oInc = (Volume.Max - Volume.Min) / 100;
				Int32 oVol = (Int32)Volume.Min + (ePerVol * oInc);
				if (eqBuffMouseOver != null) eqBuffMouseOver.Volume = oVol;
				if (eqBuffSelDie != null) eqBuffSelDie.Volume = oVol;
				if (eqBuffUnselDie != null) eqBuffUnselDie.Volume = oVol;
				if (eqBuffEntVal != null) eqBuffEntVal.Volume = oVol;
				if (eqBuffEntRedund != null) eqBuffEntRedund.Volume = oVol;
				if (eqBuffEntChall != null) eqBuffEntChall.Volume = oVol;
				if (eqBuffTick != null) eqBuffTick.Volume = oVol;
				if (eqBuffTickLow != null) eqBuffTickLow.Volume = oVol;
				if (eqBuffTickLast != null) eqBuffTickLast.Volume = oVol;
			}
		}
		
		/// <summary>
		/// The DirectSound device.
		/// </summary>
		readonly Device eqDev;
		
		/// <summary>
		/// The mouse over sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffMouseOver;
		/// <summary>
		/// The die selection sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffSelDie;
		/// <summary>
		/// The die unselection sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffUnselDie;
		/// <summary>
		/// The valid entry sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffEntVal;
		/// <summary>
		/// The redundant entry sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffEntRedund;
		/// <summary>
		/// The entry challenge sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffEntChall;
		/// <summary>
		/// The tick sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffTick;
		/// <summary>
		/// The low-time tick sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffTickLow;
		/// <summary>
		/// The last-time tick sound buffer.
		/// </summary>
		readonly SecondaryBuffer eqBuffTickLast;
		
		/// <param name="aqCtl">
		/// The owner of the DirectSound device.
		/// </param>
		/// <param name="aModeDesign">
		/// DirectSound is not available in the form designer; set aModeDesign to
		/// 'true' to create a dummy instance that ignores play requests. Because
		/// Component.DesignMode cannot be relied upon within a form's constructor,
		/// this entails that the sound manager be created within the form's OnLoad
		/// method, or someplace similar.
		/// </param>
		/// <param name="aPerVol">
		/// The starting output volume, as a percent of maximum volume.
		/// </param>
		/// <param name="aMute">
		/// Set to 'true' if the instance should start muted.
		/// </param>
		public tqMgrSound(Control aqCtl, bool aModeDesign, Int32 aPerVol, bool
			aMute) {
			
			aqCtl.sThrowNull("aqCtl");
			
			if (aModeDesign) return;
			
			eqDev = new Device();
			eqDev.SetCooperativeLevel(aqCtl, CooperativeLevel.Priority);
			
			var oqDesc = new BufferDescription();
			// Ensures that sound is output even when aqCtl loses the focus:
			oqDesc.GlobalFocus = true;
			oqDesc.ControlVolume = true;
			
			eqBuffMouseOver = eBuff_Create(eqFold + "MouseOver.wav", oqDesc);
			eqBuffSelDie = eBuff_Create(eqFold + "SelDie.wav", oqDesc);
			eqBuffUnselDie = eBuff_Create(eqFold + "UnselDie.wav", oqDesc);
			eqBuffEntVal = eBuff_Create(eqFold + "EntVal.wav", oqDesc);
			eqBuffEntRedund = eBuff_Create(eqFold + "EntInval.wav", oqDesc);
			eqBuffEntChall = eBuff_Create(eqFold + "EntInval.wav", oqDesc);
			eqBuffTick = eBuff_Create(eqFold + "Tick.wav", oqDesc);
			eqBuffTickLow = eBuff_Create(eqFold + "TickLow.wav", oqDesc);
			eqBuffTickLast = eBuff_Create(eqFold + "TickLast.wav", oqDesc);
			
			PerVol = aPerVol;
			Mute = aMute;
		}
		
		/// <summary>
		/// Creates and returns a secondary buffer representing the specified WAV
		/// file, or 'null' if the file is not found.
		/// </summary>
		/// <param name="aqPath">
		/// The path from the executable to the WAV file to be buffered.
		/// </param>
		/// <param name="aqDescBuff">
		/// The buffer description to be used when creating the buffer.
		/// </param>
		SecondaryBuffer eBuff_Create(string aqPath, BufferDescription aqDescBuff)
		{
			aqPath.sThrowNull("aqPath");
			aqDescBuff.sThrowNull("aqDescBuff");
			eqDev.sThrowNull("eqDev");
			
			if (!File.Exists(aqPath)) return null;
			return new SecondaryBuffer(aqPath, aqDescBuff, eqDev);
		}
		
		/// <summary>
		/// Plays the mouse over sound.
		/// </summary>
		public void MouseOver_Play() {
			if (Mute || (eqBuffMouseOver == null)) return;
			eqBuffMouseOver.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the die selection sound.
		/// </summary>
		public void SelDie_Play() {
			if (Mute || (eqBuffSelDie == null)) return;
			eqBuffSelDie.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the die unselection sound.
		/// </summary>
		public void UnselDie_Play() {
			if (Mute || (eqBuffUnselDie == null)) return;
			eqBuffUnselDie.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the valid entry sound.
		/// </summary>
		public void EntVal_Play() {
			if (Mute || (eqBuffEntVal == null)) return;
			eqBuffEntVal.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the redundant entry sound.
		/// </summary>
		public void EntRedund_Play() {
			if (Mute || (eqBuffEntRedund == null)) return;
			eqBuffEntRedund.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the entry challenge sound.
		/// </summary>
		public void EntChall_Play() {
			if (Mute || (eqBuffEntChall == null)) return;
			eqBuffEntChall.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the tick sound.
		/// </summary>
		public void Tick_Play() {
			if (Mute || (eqBuffTick == null)) return;
			eqBuffTick.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the low-time tick sound.
		/// </summary>
		public void TickLow_Play() {
			if (Mute || (eqBuffTickLow == null)) return;
			eqBuffTickLow.Play(0, BufferPlayFlags.Default);
		}
		
		/// <summary>
		/// Plays the last-time tick sound.
		/// </summary>
		public void TickLast_Play() {
			if (Mute || (eqBuffTickLast == null)) return;
			eqBuffTickLast.Play(0, BufferPlayFlags.Default);
		}
		
		// IDisposable
		// -----------
		
		/// <summary>
		/// Disposes of all sound buffers.
		/// </summary>
		public void Dispose() {
			if (eqDev != null) eqDev.Dispose();
			
			if (eqBuffMouseOver != null) eqBuffMouseOver.Dispose();
			if (eqBuffSelDie != null) eqBuffSelDie.Dispose();
			if (eqBuffUnselDie != null) eqBuffUnselDie.Dispose();
			if (eqBuffEntVal != null) eqBuffEntVal.Dispose();
			if (eqBuffEntRedund != null) eqBuffEntRedund.Dispose();
			if (eqBuffEntChall != null) eqBuffEntChall.Dispose();
			if (eqBuffTick != null) eqBuffTick.Dispose();
			if (eqBuffTickLow != null) eqBuffTickLow.Dispose();
			if (eqBuffTickLast != null) eqBuffTickLast.Dispose();
		}
	}
}
