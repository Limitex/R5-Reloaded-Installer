using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

/// <summary>
/// Source of "https://qiita.com/kenichiuda/items/3079ab93dae564dd5d17"
/// </summary>
namespace R5_Reloaded_Installer_Library.Other.JobObjectSharp
{
    /// <summary>
    /// A class that automatically terminates child processes when the application exits.
    /// </summary>
    public class JobObject : IDisposable
    {
        public static JobObject CreateAsKillOnJobClose()
        {
            var job = new JobObject();

            var jobInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION()
            {
                BasicLimitInformation = new JOBOBJECT_BASIC_LIMIT_INFORMATION()
                {
                    LimitFlags = JobObjectLimit.KillOnJobClose
                },
            };
            var size = Marshal.SizeOf(typeof(JOBOBJECT_EXTENDED_LIMIT_INFORMATION));
            if (!Native.SetInformationJobObject(job.SafeHandle, JobObjectInfoClass.ExtendedLimitInformation, ref jobInfo, size))
            {
                throw new Win32Exception();
            }

            return job;
        }

        private JobObject(SafeJobHandle safeHandle) => SafeHandle = safeHandle;

        private JobObject(string? name = null)
        {
            SafeHandle = Native.CreateJobObject(IntPtr.Zero, name);
            if (SafeHandle.IsInvalid)
                throw new Win32Exception();
        }

        public void AssignProcess(int processId)
        {
            using (var hProcess = Native.OpenProcess(ProcessAccessFlags.SetQuota | ProcessAccessFlags.Terminate, false, processId))
            {
                AssignProcess(hProcess);
            }
        }

        public void AssignProcess(System.Diagnostics.Process process)
            => AssignProcess(process.SafeHandle);

        public void AssignProcess(SafeProcessHandle hProcess)
        {
            if (hProcess.IsInvalid)
                throw new ArgumentException(nameof(hProcess));

            if (!Native.AssignProcessToJobObject(SafeHandle, hProcess))
            {
                throw new Win32Exception();
            }
        }

        public void Terminate(int exitCode = 0)
        {
            if (!Native.TerminateJobObject(SafeHandle, exitCode))
            {
                throw new Win32Exception();
            }
        }

        public SafeJobHandle SafeHandle { get; private set; }

        #region IDisposable Support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    SafeHandle.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
    public sealed class SafeJobHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal static SafeJobHandle InvalidHandle => new SafeJobHandle(IntPtr.Zero);

        private SafeJobHandle()
            : base(true)
        {
        }

        internal SafeJobHandle(IntPtr handle)
            : base(true) => base.SetHandle(handle);

        protected override bool ReleaseHandle() => Native.CloseHandle(base.handle);
    }
    [Flags]
    enum ProcessAccessFlags : uint
    {
        None = 0,
        All = 0x001F0FFF,
        Terminate = 0x00000001,
        CreateThread = 0x00000002,
        VirtualMemoryOperation = 0x00000008,
        VirtualMemoryRead = 0x00000010,
        VirtualMemoryWrite = 0x00000020,
        DuplicateHandle = 0x00000040,
        CreateProcess = 0x000000080,
        SetQuota = 0x00000100,
        SetInformation = 0x00000200,
        QueryInformation = 0x00000400,
        QueryLimitedInformation = 0x00001000,
        Synchronize = 0x00100000,
    }
    enum JobObjectInfoClass
    {
        AssociateCompletionPortInformation = 7,
        BasicLimitInformation = 2,
        BasicUIRestrictions = 4,
        EndOfJobTimeInformation = 6,
        ExtendedLimitInformation = 9,
        SecurityLimitInformation = 5,
        GroupInformation = 11,
    }
    [Flags]
    enum JobObjectLimit : uint
    {
        None = 0,
        // Basic Limits
        Workingset = 0x00000001,
        ProcessTime = 0x00000002,
        JobTime = 0x00000004,
        ActiveProcess = 0x00000008,
        Affinity = 0x00000010,
        PriorityClass = 0x00000020,
        PreserveJobTime = 0x00000040,
        SchedulingClass = 0x00000080,

        // Extended Limits
        ProcessMemory = 0x00000100,
        JobMemory = 0x00000200,
        DieOnUnhandledException = 0x00000400,
        BreakawayOk = 0x00000800,
        SilentBreakawayOk = 0x00001000,
        KillOnJobClose = 0x00002000,
        SubsetAffinity = 0x00004000,

        // Notification Limits
        JobReadBytes = 0x00010000,
        JobWriteBytes = 0x00020000,
        RateControl = 0x00040000,
    }
    [StructLayout(LayoutKind.Sequential)]
    struct JOBOBJECT_BASIC_LIMIT_INFORMATION
    {
        public Int64 PerProcessUserTimeLimit;
        public Int64 PerJobUserTimeLimit;
        public JobObjectLimit LimitFlags;
        public UIntPtr MinimumWorkingSetSize;
        public UIntPtr MaximumWorkingSetSize;
        public UInt32 ActiveProcessLimit;
        public Int64 Affinity;
        public UInt32 PriorityClass;
        public UInt32 SchedulingClass;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct IO_COUNTERS
    {
        public UInt64 ReadOperationCount;
        public UInt64 WriteOperationCount;
        public UInt64 OtherOperationCount;
        public UInt64 ReadTransferCount;
        public UInt64 WriteTransferCount;
        public UInt64 OtherTransferCount;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct JOBOBJECT_EXTENDED_LIMIT_INFORMATION
    {
        public JOBOBJECT_BASIC_LIMIT_INFORMATION BasicLimitInformation;
        public IO_COUNTERS IoInfo;
        public UIntPtr ProcessMemoryLimit;
        public UIntPtr JobMemoryLimit;
        public UIntPtr PeakProcessMemoryUsed;
        public UIntPtr PeakJobMemoryUsed;
    }
    static class Native
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetInformationJobObject(SafeJobHandle hJob,
           JobObjectInfoClass JobObjectInfoClass,
           ref JOBOBJECT_EXTENDED_LIMIT_INFORMATION lpJobObjectInfo,
           int cbJobObjectInfoLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AssignProcessToJobObject(SafeJobHandle hJob, SafeProcessHandle hProcess);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeProcessHandle OpenProcess(
             ProcessAccessFlags processAccess,
             bool bInheritHandle,
             int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern SafeJobHandle CreateJobObject(IntPtr lpJobAttributes, string? lpName);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool TerminateJobObject(SafeJobHandle handle, int uExitCode);
    }
}