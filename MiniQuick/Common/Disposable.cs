using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniQuick.Common
{
    public class Disposable : IDisposable
    {
        bool _disposed;

        protected Action OnDispose;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing && OnDispose != null)
            {
                OnDispose();
            }

            _disposed = true;
        }

        ~Disposable()
        {
            Dispose(false);
        }
    }
}
