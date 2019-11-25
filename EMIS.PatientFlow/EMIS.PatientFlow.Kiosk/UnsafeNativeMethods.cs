using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace EMIS.PatientFlow.Kiosk
{
	internal static class UnsafeNativeMethods
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern SafeWaitHandle CreateWaitableTimer(IntPtr lpTimerAttributes,
																  bool bManualReset,
																string lpTimerName);

		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool SetWaitableTimer(SafeWaitHandle hTimer,
													[In] ref long pDueTime,
															  int lPeriod,
														   IntPtr pfnCompletionRoutine,
														   IntPtr lpArgToCompletionRoutine,
															 bool fResume);

		[DllImport("User32.dll")]
		internal extern static bool GetLastInputInfo(out LASTINPUTINFO plii);
	}
}
