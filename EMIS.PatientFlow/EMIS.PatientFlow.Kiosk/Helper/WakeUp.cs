using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace EMIS.PatientFlow.Kiosk.Helper
{
    public sealed class WakeUp : IDisposable
    {
        public event EventHandler Woken;

        private static BackgroundWorker bgWorker = new BackgroundWorker();

        public WakeUp()
        {
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted +=
            BgWorker_RunWorkerCompleted;
        }

        public void SetWakeUpTime(DateTime time)
        {
            bgWorker.RunWorkerAsync(time.ToFileTime());
        }

        void BgWorker_RunWorkerCompleted(
            object sender,
            RunWorkerCompletedEventArgs e)
        {
            if (Woken != null)
            {
                Woken(this, new EventArgs());
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Interoperability",
            "CA1404:CallGetLastErrorImmediatelyAfterPInvoke",
            Justification = "No calls exist between the pInvoke call and the GetLastWin32Error call that can change the error state.")]
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            long waketime = (long)e.Argument;

            using (SafeWaitHandle handle =
                UnsafeNativeMethods.CreateWaitableTimer(
                    IntPtr.Zero,
                    true,
                    GetType().Assembly.GetName().Name + "Timer"))
            {
                if (UnsafeNativeMethods.SetWaitableTimer(
                    handle,
                    ref waketime,
                    0,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    true))
                {
                    using (EventWaitHandle wh = new EventWaitHandle(
                        false,
                        EventResetMode.AutoReset))
                    {
                        wh.SafeWaitHandle = handle;
                        wh.WaitOne();
                    }
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        public void Dispose()
        {
            bgWorker.Dispose();
        }
    }
}
