// If you're wondering about the syntax, I created this file in Sublime Text.
// You will see a lot of inconsistencies across this repository

using Colour = System.Drawing.Color;

namespace osu.Helpers{
	public static class ColourHelper{
		public static void SetDark(Control parent){

			parent.BackColor = Color.FromArgb(30, 30, 30);
		    parent.ForeColor = Color.White;

		    foreach (Control c in parent.Controls)
        	SetDark(c);
		}

		public static void DarkenButtons(Control parent){

			foreach( Control c in parent.Controls){

				if (c is Button btn){

					btn.FlatStyle = FlatStyle.Flat;
					btn.FlatAppearance.BorderColor = Colour.Gray;
					btn.BackColor = Colour.FromArgb(45, 45, 48);
					btn.ForeColor = Colour.White;
				}
				else{

					DarkenButtons(c);
				}
			}
		}
	}	
}
