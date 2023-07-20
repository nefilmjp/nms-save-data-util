using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NMSSaveDataUtil.Classes
{
    internal class Portal
    {
        // 1920x1080
        const int areaX = 1167;
        const int areaY = 737;
        const int boxW = 82;
        const int boxH = 92;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetClientRect(HandleRef hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        private static void ClickPos(int x , int y)
        {
            Thread.Sleep(100);
            MouseInputWrapper.SendMouseMove(x, y, Screen.PrimaryScreen);
            MouseInputWrapper.SendMouseDown();
            Thread.Sleep(30);
            MouseInputWrapper.SendMouseUp();
        }

        private static int[] ParseAddress(string address)
        {
            char[] glyphs = address.Trim().ToCharArray();
            return glyphs.ToList().Select(glyph => 
                Int32.Parse(glyph.ToString(), System.Globalization.NumberStyles.HexNumber)
            ).ToArray();
        }

        private static int[] GetBoxPos(int glyphNum)
        {
            int x = glyphNum % 8;
            int y = glyphNum < 8 ? 0 : 1;
            return new int[] { x, y };
        }

        public bool SendAddress(string address)
        {
            IntPtr hWnd =  FindWindow(null, "No Man's Sky");

            if (hWnd == IntPtr.Zero)
            {
                return false;
            }

            HandleRef hRef = new HandleRef(this, hWnd);

            GetWindowRect(hRef, out RECT wRect);
            GetClientRect(hRef, out RECT cRect);

            int wW = wRect.Right - wRect.Left;
            int wH = wRect.Bottom - wRect.Top;
            int bW = (wW - cRect.Right) / 2;
            int cX = wRect.Left + bW;
            int cY = wRect.Bottom - bW - cRect.Bottom;

            double sizeRatio = 1920 / cRect.Right;

            // ポータルグリフの箱のサイズ
            int gW = (int)Math.Round((double)sizeRatio * boxW, MidpointRounding.AwayFromZero);
            int gH = (int)Math.Round((double)sizeRatio * boxH, MidpointRounding.AwayFromZero);

            // ポータルアドレス1個目の中心の座標
            int gX = (int)Math.Round((double)sizeRatio * areaX, MidpointRounding.AwayFromZero);
            int gY = (int)Math.Round((double)sizeRatio * areaY, MidpointRounding.AwayFromZero);

            Debug.WriteLine($"wRect: {wRect.Left}, {wRect.Top}, {wRect.Right}, {wRect.Bottom}, w:{wW}, h:{wH}, bw:{bW}");
            Debug.WriteLine($"cRect: {cRect.Left}, {cRect.Top}, {cRect.Right}, {cRect.Bottom}, x:{cX}, y:{cY}, ratio:{sizeRatio}");
            Debug.WriteLine($"glyph: {gW}, {gH}, {gX}, {gY}");

            SetForegroundWindow(hWnd);
            Thread.Sleep(200);
            MouseInputWrapper.SendMouseMove(cX + 100, cY + 100, Screen.PrimaryScreen);
            Thread.Sleep(30);
            MouseInputWrapper.SendMouseRightDown();
            Thread.Sleep(30);
            MouseInputWrapper.SendMouseRightUp();

            int[] glyphs = ParseAddress(address);

            foreach (int glyph in glyphs)
            {
                int[] pos = GetBoxPos(glyph);
                Debug.WriteLine($"{glyph}: {pos[0]}, {pos[1]}");
                ClickPos(cX + gX + gW * pos[0], cY + gY + gH * pos[1]);
            }

            return true;
        }
    }
}
