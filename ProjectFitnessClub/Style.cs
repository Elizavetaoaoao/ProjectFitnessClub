using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub
{
    internal class Style
    {

        PrivateFontCollection pfc = new PrivateFontCollection();
        public Style() { 
            LoadFont();
        }
        public PrivateFontCollection getPFC()
        {
            return pfc;
        }
        private void LoadFont()
        {
            string s = @"G:\УП\Project\Inter_24pt-Regular.ttf";
            pfc.AddFontFile(s);
        }

    }
}
