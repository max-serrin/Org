using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyExtensions
{
    public static class DataExtensions
    {
        /// <summary>
        /// Gets common keys, optional boolean parameters allow specific sets to be added or skipped.
        /// </summary>
        /// <param name="alphabet">A-Z</param>
        /// <param name="numbers">Tilde, 1-9, 0, Minus and Plus</param>
        /// <param name="punctuation">Space, Brackets, Slashes, Colons, Quotes, Comma, Period and Question Mark</param>
        /// <param name="numpad">NumPad0-9, Divide and Times</param>
        /// <param name="escape">Escape</param>
        /// <param name="function">F1-F12</param>
        /// <param name="specialFunction">PrintScreen, Pause, Insert, Delete, Home, End, PageUp, PageDown</param>
        /// <param name="arrows">Left, Right, Up, Down</param>
        /// <param name="enter">Enter</param>
        /// <param name="backspace">Backspace</param>
        /// <returns></returns>
        public static List<Keys> GetCommonKeysAndButtons(bool alphabet = true, bool numbers = true, bool punctuation = true, bool numpad = true,
            bool escape = true, bool function = true, bool specialFunction = true, bool arrows = true, bool enter = true, bool backspace = true)
        {
            List<Keys> keys = new List<Keys>();
            if (alphabet)
                keys.AddRange(new Keys[] { Keys.A, Keys.B, Keys.C, Keys.D, Keys.E,
                    Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L,
                    Keys.M, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T,
                    Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z });
            if (numbers)
                keys.AddRange(new Keys[] { Keys.Oemtilde, Keys.D1, Keys.D2,
                    Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8,
                    Keys.D9,  Keys.D0, Keys.OemMinus, Keys.Oemplus });
            if (punctuation)
                keys.AddRange(new Keys[] { Keys.Space, Keys.OemOpenBrackets,
                    Keys.OemCloseBrackets, Keys.OemBackslash, Keys.OemSemicolon,
                    Keys.OemQuotes, Keys.Oemcomma, Keys.OemPeriod, Keys.OemQuestion });
            if (numpad)
                keys.AddRange(new Keys[] { Keys.NumPad0, Keys.NumPad1, Keys.NumPad2, Keys.NumPad3,
                    Keys.NumPad4, Keys.NumPad5, Keys.NumPad6, Keys.NumPad7,
                    Keys.NumPad8, Keys.NumPad9, Keys.Divide, Keys.Multiply });
            if (escape)
                keys.Add(Keys.Escape);
            if (function)
                keys.AddRange(new Keys[] { Keys.F1, Keys.F2, Keys.F3, Keys.F4, Keys.F5,
                    Keys.F6, Keys.F7, Keys.F8, Keys.F9, Keys.F10, Keys.F11, Keys.F12 });
            if (specialFunction)
                keys.AddRange(new Keys[] { Keys.PrintScreen, Keys.Pause, Keys.Insert,
                    Keys.Home, Keys.PageUp, Keys.Delete, Keys.End, Keys.PageDown });
            if (arrows)
                keys.AddRange(new Keys[] { Keys.Left, Keys.Up, Keys.Down, Keys.Right });
            if (enter)
                keys.Add(Keys.Enter);
            if (backspace)
                keys.Add(Keys.Back);
            return keys;
        }

        public static string ToCommonString(this Keys key)
        {
            if (key.ToString().Contains("Oem"))
                return key.ToString().Substring(3, 1).ToUpper() + key.ToString().Substring(4);
            else if (key.ToString().First() == 'D' && key.ToString().Length == 2)
                return key.ToString().Substring(1);
            else if (key.Equals(Keys.OemCloseBrackets))
                return "CloseBrackets";
            else if (key.Equals(Keys.OemSemicolon))
                return "Semi-colon";
            else if (key.Equals(Keys.OemQuotes))
                return "Quotes";
            return key.ToString();
        }
    }
}
