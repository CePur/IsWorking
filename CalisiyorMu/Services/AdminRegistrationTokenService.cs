using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalisiyorMu.Services
{
    public class AdminRegistrationTokenService
    {
        private readonly Lazy<ulong> _creationKey = new Lazy<ulong>(() => BitConverter.ToUInt64(Guid.NewGuid().ToByteArray(), 7));

        public ulong CreationKey => _creationKey.Value;

    }
}
