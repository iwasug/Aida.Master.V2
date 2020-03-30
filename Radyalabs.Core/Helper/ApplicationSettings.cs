using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
    public class ApplicationSettings
    {
        public ApplicationSettings()
        {
            EncryptionKey = "VBvxXwkKyG3ipGMRB68iXcNhk7e1KsD7";
            VerificationKey = "HZpQKRUeWxBCRn18B485aoOMQfZS3V10";
        }

        public string EncryptionKey { get; private set; }
        public string VerificationKey { get; private set; }
    }
}
