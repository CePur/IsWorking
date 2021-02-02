using System;

namespace CalisiyorMu.Services
{
    public class AdminRegistrationTokenService
    {
        private readonly Lazy<ulong> creationKey = new Lazy<ulong>(() => BitConverter.ToUInt64(Guid.NewGuid().ToByteArray(), 7));

        public ulong CreationKey
        {
            get { return creationKey.Value; }
        }
    }
}