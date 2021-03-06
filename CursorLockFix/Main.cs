﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using MelonLoader;
using UnityEngine;
using UnityEngine.XR;

namespace CursorLockFix
{
    public static class BuildInfo
    {
        public const string Name = nameof(CursorLockFix);
        public const string Author = "Requi";
        public const string Version = "1.3";
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
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private bool _isVr;

        private bool _unlockedCursor;
        private IntPtr _gameHwnd;

        private bool IsVr()
        {
            return _isVr || XRDevice.isPresent;
        }

        public override void OnApplicationStart()
        {
            _isVr = Environment.GetCommandLineArgs()
                .All(args => !args.Equals("--no-vr", StringComparison.OrdinalIgnoreCase));
        }

        public override void OnUpdate()
        {
            if (IsVr())
                return;

            if (_gameHwnd == IntPtr.Zero)
            {
                _gameHwnd = FindWindow("UnityWndClass", Application.productName);
                if (_gameHwnd == IntPtr.Zero)
                {
                    UnlockCursor();
                    return;
                }
            }

            const int titleBarHeight = 45;
            const int borderSize = 10;

            if (_gameHwnd == GetForegroundWindow()) // I don't trust Application.isFocused anymore
            {
                var windowRect = new RECT();
                GetWindowRect(_gameHwnd, ref windowRect);

                var cursorLimits = new RECT
                {
                    Left = windowRect.Left + borderSize,
                    Top = windowRect.Top + titleBarHeight,
                    Right = windowRect.Right - borderSize,
                    Bottom = windowRect.Bottom - borderSize
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
