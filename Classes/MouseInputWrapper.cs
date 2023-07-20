﻿using System.Runtime.InteropServices;

namespace NMSSaveDataUtil.Classes
{
    public static class MouseInputWrapper
    {
        class NativeMethods
        {
            [DllImport("user32.dll", SetLastError = true)]
            public extern static int SendInput(int nInputs, ref Input pInputs, int cbsize);

            [DllImport("user32.dll", SetLastError = true)]
            public extern static IntPtr GetMessageExtraInfo();

            [DllImport("user32.dll")]
            public extern static bool SetCursorPos(int x, int y);
        }

        const int MOUSEEVENTF_MOVE = 0x0001;
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const int MOUSEEVENTF_RIGHTUP = 0x00010;
        const int MOUSEEVENTF_VIRTUALDESK = 0x4000;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [StructLayout(LayoutKind.Sequential)]
        struct MouseInput
        {
            public int X;
            public int Y;
            public int Data;
            public int Flags;
            public int Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KeyboardInput
        {
            public short VirtualKey;
            public short ScanCode;
            public int Flags;
            public int Time;
            public IntPtr ExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HardwareInput
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct Input
        {
            public int Type;
            public InputUnion ui;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)]
            public MouseInput Mouse;
            [FieldOffset(0)]
            public KeyboardInput Keyboard;
            [FieldOffset(0)]
            public HardwareInput Hardware;
        }

        static Input MouseMoveData(int x, int y, System.Windows.Forms.Screen screen, IntPtr extraInfo)
        {
            x *= (65535 / screen.Bounds.Width);
            y *= (65535 / screen.Bounds.Height);

            Input input = new()
            {
                Type = 0, // MOUSE = 0
            };

            input.ui.Mouse.Flags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE; // | MOUSEEVENTF_VIRTUALDESK
            input.ui.Mouse.Data = 0;
            input.ui.Mouse.X = x;
            input.ui.Mouse.Y = y;
            input.ui.Mouse.Time = 0;
            input.ui.Mouse.ExtraInfo = extraInfo;

            return input;
        }

        static Input MouseDataWithoutMove(int flags, IntPtr extraInfo)
        {
            Input input = new()
            {
                Type = 0, // MOUSE = 0
            };

            input.ui.Mouse.Flags = flags;
            input.ui.Mouse.Data = 0;
            input.ui.Mouse.X = 0;
            input.ui.Mouse.Y = 0;
            input.ui.Mouse.Time = 0;
            input.ui.Mouse.ExtraInfo = extraInfo;

            return input;
        }



        public static void SendMouseMove(int x, int y, System.Windows.Forms.Screen screen)
        {
            IntPtr extraInfo = NativeMethods.GetMessageExtraInfo();
            Input inputs = MouseMoveData(x, y, screen, extraInfo);
            NativeMethods.SetCursorPos(x, y);
            int ret = NativeMethods.SendInput(1, ref inputs, Marshal.SizeOf(inputs));
            // int errCode = Marshal.GetLastWin32Error();
            // Console.WriteLine(ret);
            // Console.WriteLine(errCode);
        }

        public static void SendMouseDown()
        {
            IntPtr extraInfo = NativeMethods.GetMessageExtraInfo();
            Input inputs = MouseDataWithoutMove(MOUSEEVENTF_LEFTDOWN, extraInfo);
            int ret = NativeMethods.SendInput(1, ref inputs, Marshal.SizeOf(inputs));
        }
        public static void SendMouseUp()
        {
            IntPtr extraInfo = NativeMethods.GetMessageExtraInfo();
            Input inputs = MouseDataWithoutMove(MOUSEEVENTF_LEFTUP, extraInfo);
            int ret = NativeMethods.SendInput(1, ref inputs, Marshal.SizeOf(inputs));
        }
        
        public static void SendMouseRightDown()
        {
            IntPtr extraInfo = NativeMethods.GetMessageExtraInfo();
            Input inputs = MouseDataWithoutMove(MOUSEEVENTF_RIGHTDOWN, extraInfo);
            int ret = NativeMethods.SendInput(1, ref inputs, Marshal.SizeOf(inputs));
        }
        public static void SendMouseRightUp()
        {
            IntPtr extraInfo = NativeMethods.GetMessageExtraInfo();
            Input inputs = MouseDataWithoutMove(MOUSEEVENTF_RIGHTUP, extraInfo);
            int ret = NativeMethods.SendInput(1, ref inputs, Marshal.SizeOf(inputs));
        }
    }
}
