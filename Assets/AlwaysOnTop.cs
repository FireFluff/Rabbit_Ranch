#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;


public class AlwaysOnTop : MonoBehaviour
{
    #region WIN32API

    const string CONSTANT_WINDOW_TITLE_FROM_GAME = "DesktopTest"; // Change this to your game's window title
    public static readonly System.IntPtr HWND_TOPMOST = new System.IntPtr(-1);
    public static readonly System.IntPtr HWND_NOT_TOPMOST = new System.IntPtr(-2);
    const System.UInt32 SWP_SHOWWINDOW = 0x0040;

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(System.Drawing.Rectangle r)
            : this(r.Left, r.Top, r.Right, r.Bottom)
        {
        }

        public int X
        {
            get
            {
                return Left;
            }
            set
            {
                Right -= (Left - value);
                Left = value;
            }
        }

        public int Y
        {
            get
            {
                return Top;
            }
            set
            {
                Bottom -= (Top - value);
                Top = value;
            }
        }

        public int Height
        {
            get
            {
                return Bottom - Top;
            }
            set
            {
                Bottom = value + Top;
            }
        }

        public int Width
        {
            get
            {
                return Right - Left;
            }
            set
            {
                Right = value + Left;
            }
        }

        public static implicit operator System.Drawing.Rectangle(RECT r)
        {
            return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
        }

        public static implicit operator RECT(System.Drawing.Rectangle r)
        {
            return new RECT(r);
        }
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern System.IntPtr FindWindow(String lpClassName, String lpWindowName);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetWindowPos(System.IntPtr hWnd, System.IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

#if !UNITY_EDITOR
    [DllImport("user32.dll")]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;
    // Remove WS_EX_TRANSPARENT to allow window interaction
    const int LWA_COLORKEY = 0x1;
    const int LWA_ALPHA = 0x2;
#endif
    #endregion


    // Use this for initialization
    void Start()
    {
        AssignTopmostWindow(CONSTANT_WINDOW_TITLE_FROM_GAME, true);
#if !UNITY_EDITOR
        IntPtr hwnd = GetActiveWindow();
        // Make window layered but not input-transparent
        SetWindowLong(hwnd, GWL_EXSTYLE, GetWindowLong(hwnd, GWL_EXSTYLE) | WS_EX_LAYERED);

        // Adjust color key and alpha for selective transparency
        SetLayeredWindowAttributes(hwnd, 0, 255, LWA_COLORKEY); // You might not need LWA_COLORKEY
#endif
    }

    public bool AssignTopmostWindow(string WindowTitle, bool MakeTopmost)
    {
        UnityEngine.Debug.Log("Assigning top most flag to window of title: " + WindowTitle);

        System.IntPtr hWnd = FindWindow((string)null, WindowTitle);

        RECT rect = new RECT();
        GetWindowRect(new HandleRef(this, hWnd), out rect);

        return SetWindowPos(hWnd, MakeTopmost ? HWND_TOPMOST : HWND_NOT_TOPMOST, rect.X, rect.Y, rect.Width, rect.Height, SWP_SHOWWINDOW);
    }

    public bool IsNullOrWhitespace(string Str)
    {
        if (Str.Equals("null"))
        {
            return true;
        }
        foreach (char c in Str)
        {
            if (c != ' ')
            {
                return false;
            }
        }
        return true;
    }
}
#endif