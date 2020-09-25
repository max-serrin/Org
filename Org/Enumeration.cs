using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyExtensions;

namespace Org
{
    class Enumeration
    {
        internal enum Modes
        {
            Browse,
            Organize,
            Randomize
        }

        internal enum SettingsTypes
        {
            General,
            Hotkeys
        }

        internal enum Actions
        {
            None,
            Move,
            Copy,
            CopyData,
            Cut,
            Undo,
            SelectAll,
            SelectAllPrevious,
            SelectAllFollowing,
            Delete,
            FullScreen,
            NextFile,
            PreviousFile,
            Skip,
            UpDirectory,
            NextDirectory,
            PreviousDirectory,
            Rename,
            Open,
            Mode
        }

        internal class HotkeySettings
        {
            public Keys Key { get; set; }
            public string Name { get; set; }
            public Actions Action { get; set; }
            public List<string> Arguments { get; set; }
            public object Tag { get; set; }

            public HotkeySettings(Keys key)
            {
                Key = key;
                Name = key.ToCommonString();
                Action = Actions.None;
                Arguments = new List<string>();
                Tag = null;
            }
        }

        internal class UndoActions
        {
            public List<string> Arguments { get; set; }

            public UndoActions()
            {
                Arguments = new List<string>();
            }
        }

        internal static class Sizes
        {
            internal static class Width
            {
                public static int XSmall { get; } = 40;
                public static int Small { get; } = 50;
                public static int Medium { get; } = 100;
                public static int Large { get; } = 150;
                public static int XLarge { get; } = 200;
                public static int Default { get; } = 80;
            }

            internal static class Height
            {
                public static int Default { get; } = 20;
            }
        }
    }
}
