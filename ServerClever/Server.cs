using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerClever
{
    internal class Server
    {
        private static ReaderWriterLock _locker = new ReaderWriterLock();
        private static int _count;

        public static int GetCount()
        {
            _locker.AcquireReaderLock(100000);
            try
            {
                Thread.Sleep(1000);
                return _count;
            }
            finally
            {
                _locker.ReleaseReaderLock();
            }
        }

        public static void AddToCount(int value)
        {
            _locker.AcquireReaderLock(100000);
            try
            {
                Thread.Sleep(5000);
                _count = value;
            }
            finally
            {
                _locker.ReleaseLock();
            }
        }
    }
}
