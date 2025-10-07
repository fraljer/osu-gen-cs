// If you're wondering about the syntax, I created this file in Sublime Text.
// You will see a lot of inconsistencies across this repository

// fuck, I found Dark.Net, just AFTER I made this.

using Colour = System.Drawing.Color;

namespace osu.Helpers{
	public static class ColourHelper{
		public static void SetDark(Control parent){

			parent.BackColor = Colour.FromArgb(21,21,21);
		    parent.ForeColor = Colour.White;

		    foreach (Control c in parent.Controls)
        	SetDark(c);
		}

		public static void DarkenButtons(Control parent){

			foreach( Control c in parent.Controls){

				if (c is Button btn){

					btn.FlatStyle = FlatStyle.Standard;
					btn.FlatAppearance.BorderColor = Colour.Red;
					btn.BackColor = Colour.FromArgb(125, 196, 228);
					btn.ForeColor = Colour.Red;
				}
				else{

					DarkenButtons(c);
				}
			}
		}
	}	
}
