using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
    public class ApiTokenHelper
    {
        public static string Set(object obj, bool isRemember, string key)
        {
            ApplicationSettings applicationSettings = new ApplicationSettings();

            string strJsonUser = JsonConvert.SerializeObject(obj);

            string encryptionKey = applicationSettings.EncryptionKey;
            string verificationKey = applicationSettings.VerificationKey;

            CryptoService cryptoService = new CryptoService(encryptionKey, verificationKey);

            var byteToken = Encoding.UTF8.GetBytes(strJsonUser);
            var result = cryptoService.Protect(byteToken);
            var token = Convert.ToBase64String(result);

            return token;
        }

        public static T Get<T>(string token) where T : class
        {
            T userAuth = null;

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }
            try
            {
                ApplicationSettings applicationSettings = new ApplicationSettings();

                string encryptionKey = applicationSettings.EncryptionKey;
                string verificationKey = applicationSettings.VerificationKey;

                CryptoService cryptoService = new CryptoService(encryptionKey, verificationKey);

                var resultByte = cryptoService.Unprotect(Convert.FromBase64String(token));

                userAuth = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(resultByte));
            }
            catch (Exception ex)
            {
                userAuth = null;
            }

            return userAuth;
        }

        public static void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
