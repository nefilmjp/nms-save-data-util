using NMSSaveDataUtil.Properties;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NMSSaveDataUtil.Classes
{
    internal class LocalFont
    {
        public static void AddFontFromResource(PrivateFontCollection privateFontCollection, byte[] fontBytes)
        {
            IntPtr fontData = Marshal.AllocCoTaskMem(fontBytes.Length);
            Marshal.Copy(fontBytes, 0, fontData, fontBytes.Length);
            privateFontCollection.AddMemoryFont(fontData, fontBytes.Length);
        }
    }
}
