using System;
using System.Runtime.InteropServices;

namespace Flare.Interop
{
   public class InteropUtility
   {
      public const int FlashwAll = ( FlashwCaption | FlashwTray );
      public const int FlashwCaption = 0x00000001;
      public const int FlashwStop = 0;
      public const int FlashwTimer = 0x00000004;
      public const int FlashwTimernofg = 0x0000000C;
      public const int FlashwTray = 0x00000002;

      #region Nested type: FLASHWINFO
      [StructLayout( LayoutKind.Sequential )]
      public struct FLASHWINFO
      {
         [MarshalAs( UnmanagedType.U4 )]
         public int CbSize;
         public IntPtr Hwnd;
         [MarshalAs( UnmanagedType.U4 )]
         public int DWFlags;
         [MarshalAs( UnmanagedType.U4 )]
         public int UCount;
         [MarshalAs( UnmanagedType.U4 )]
         public int DWTimeout;
      }
      #endregion

      [DllImport( "user32.dll" )]
      public static extern bool FlashWindowEx( [MarshalAs( UnmanagedType.Struct )] ref FLASHWINFO pfwi );
   }
}