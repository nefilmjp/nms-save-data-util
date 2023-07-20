using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NMSSaveDataUtil.Classes
{
    internal class MoveCamera: IDisposable
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        private IntPtr hWnd;
        private readonly IXbox360Controller? controller;

        public MoveCamera()
        {
            ViGEmClient client = new();
            controller = client.CreateXbox360Controller();
            if (controller == null)
            {
                return;
            }
            controller.Connect();
        }

        private bool ActivateNMS()
        {
            hWnd = FindWindow(null, "No Man's Sky");

            if (hWnd == IntPtr.Zero)
            {
                return false;
            }

            SetForegroundWindow(hWnd);

            return true;
        }

        public void Start(short move, int delay, short rotate, int duration)
        {
            if (controller == null) return;

            bool nmsExists = ActivateNMS();

            if (!nmsExists) return;

            ThreadPool.QueueUserWorkItem(delegate
            { 
                Thread.Sleep(500);
                controller.SetAxisValue(Xbox360Axis.LeftThumbX, move);
                Thread.Sleep(delay);
                controller.SetAxisValue(Xbox360Axis.RightThumbX, rotate);
                Thread.Sleep(duration);
                controller.SetAxisValue(Xbox360Axis.LeftThumbX, 0);
                controller.SetAxisValue(Xbox360Axis.RightThumbX, 0);
            });
        }

        public void Dispose()
        {
            controller?.Disconnect();
        }
    }
}
