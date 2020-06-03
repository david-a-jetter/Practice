using System;
using System.Collections.Generic;
using System.Text;

namespace Practice.Minesweeper
{
    public class Cell
    {
        internal bool IsBomb { get; }
        internal bool Clicked { get; private set; }
        private string _Display { get; set; }
        private bool _Flagged { get; set; }
        public string Display => GetDisplay();

        public Cell(bool isBomb, string display="")
        {
            IsBomb = isBomb;
            _Display = display ?? throw new ArgumentNullException(nameof(display));
        }

        public void Click()
        {
            Clicked = true;
        }

        public void Flag()
        {
            _Flagged = true;
        }

        internal void SetDisplay(string display)
        {
            _Display = display ?? throw new ArgumentNullException(nameof(display));
        }

        private string GetDisplay()
        {
            var display = _Display;
            if (Clicked)
            {
                if(IsBomb)
                {
                    display = "!";
                }
                else
                {
                    display = _Display;
                }
            }
            else
            {
                if (_Flagged)
                {
                    display = "F";
                }
                else
                {
                    display = "?";
                }
            }

            return display;
        }
    }
}
