using System;
using System.Runtime.InteropServices;
using MelonLoader;
using UnityEngine;

namespace CursorLockFix
{
    public static class BuildInfo
    {
        public const string Name = nameof(CursorLockFix);
        public const string Author = "Requi";
        public const string Version = "1.0";
    }

    public class Main : MelonMod
    {
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern bool ClipCursor(ref RECT lpRect);
        [DllImport("user32.dll")]
        private static extern bool ClipCursor(IntPtr lpRect);
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref RECT lpRect);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(IntPtr zeroOnly, string lpWindowName);

        private bool _unlockedCursor;
        private IntPtr _gameHwnd;
        
        public override void OnUpdate()
        {
            if (_gameHwnd == IntPtr.Zero)
            {
                _gameHwnd = FindWindow(IntPtr.Zero, Application.productName);
                if (_gameHwnd == IntPtr.Zero)
                {
                    UnlockCursor();
                    return;
                }
            }
            if (Application.isFocused)
            {
                var windowRect = new RECT();
                GetWindowRect(_gameHwnd, ref windowRect);

                var cursorLimits = new RECT
                {
                    Left = windowRect.Left,
                    Top = windowRect.Top,
                    Right = Screen.width - 1,
                    Bottom = Screen.height - 1
                };
                ClipCursor(ref cursorLimits);
                _unlockedCursor = false;
            }
            else
            {
                if (!_unlockedCursor)
                {
                    UnlockCursor();
                }
            }
        }
        private void UnlockCursor()
        {
            ClipCursor(IntPtr.Zero);
            _unlockedCursor = true;
        }
    }
}
