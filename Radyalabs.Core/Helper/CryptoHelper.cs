using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Radyalabs.Core.Helper
{
#pragma warning disable CS1591
    public class CryptoHelper
    {
        public static byte[] GenerateRandomBytes(int n = 32)
        {
            using (var cryptoProvider = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[n];
                cryptoProvider.GetBytes(bytes);
                return bytes;
            }
        }
        // AES uses a 16 byte IV
        private const int IVLength = 16;

        // HMAC 256
        private const int HMacLength = 32;

        public static byte[] Protect(byte[] encryptionKey, byte[] validationKey, byte[] initializationVector, byte[] plainText)
        {
            using (var provider = new AesCryptoServiceProvider())
            {
                using (ICryptoTransform transform = provider.CreateEncryptor(encryptionKey, initializationVector))
                {
                    using (var ms = new MemoryStream())
                    {
                        ms.Write(initializationVector, 0, initializationVector.Length);
                        using (var cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                        {
                            // Encrypted payload
                            cryptoStream.Write(plainText, 0, plainText.Length);
                            cryptoStream.FlushFinalBlock();

                            // Compute signature
                            using (var sha = new HMACSHA256(validationKey))
                            {
                                checked
                                {
                                    byte[] signature = sha.ComputeHash(ms.GetBuffer(), 0, (int)ms.Length);

                                    // Write the signature to the paylod
                                    ms.Write(signature, 0, signature.Length);

                                    // Final bytes
                                    return ms.ToArray();
                                }
                            }
                        }
                    }
                }
            }
        }

        public static byte[] Unprotect(byte[] encryptionKey, byte[] validationKey, byte[] payload)
        {
            using (var provider = new AesCryptoServiceProvider())
            {
                var initializationVector = new byte[IVLength];

                using (var sha = new HMACSHA256(validationKey))
                {
                    checked
                    {
                        // The length of the unsigned payload
                        int payloadOffset = payload.Length - HMacLength;

                        // Computer the hash of the IV and cipher text for validation
                        byte[] hash = sha.ComputeHash(payload, 0, payloadOffset);

                        // Make sure they match
                        ValidateHashBytes(payload, hash, payloadOffset);

                        Buffer.BlockCopy(payload, 0, initializationVector, 0, initializationVector.Length);
                    }
                }

                using (ICryptoTransform transform = provider.CreateDecryptor(encryptionKey, initializationVector))
                {
                    using (var ms = new MemoryStream())
                    {
                        using (var cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                        {
                            checked
                            {
                                cryptoStream.Write(payload, IVLength, payload.Length - (HMacLength + IVLength));
                                cryptoStream.FlushFinalBlock();

                                return ms.ToArray();
                            }
                        }
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void ValidateHashBytes(byte[] payload, byte[] hash, int payloadOffset)
        {
            // Just because
            checked
            {
                bool ok = true;

                // DO NOT Change this CODE! It's O(HMacLength) for a reason
                for (int i = 0; i < HMacLength; i++)
                {
                    ok &= hash[i] == payload[i + payloadOffset];
                }

                if (!ok)
                {
                    // Tsk tsk tsk, stop tampering with my data (BAD TOUCH!)
                    throw new InvalidOperationException();
                }
            }
        }

        public static string ToHex(byte[] buffer)
        {
            var sb = new StringBuilder(buffer.Length * 2);

            foreach (byte b in buffer)
            {
                sb.Append(HexChar((int)(b >> 4)));
                sb.Append(HexChar((int)(b & 0xF)));
            }

            return sb.ToString();

        }

        public static byte[] FromHex(string hexValue)
        {
            var buffer = new byte[hexValue.Length / 2];

            for (int i = 0; i < buffer.Length; i++)
            {
                var b1 = HexValue(hexValue[i * 2]) << 4;
                var b2 = HexValue(hexValue[(i * 2) + 1]);
                buffer[i] = (byte)(b1 + b2);
            }

            return buffer;
        }

        private static int HexValue(char digit)
        {
            return digit > '9' ? digit - '7' : digit - '0';
        }

        private static char HexChar(int value)
        {
            return (char)(value > 9 ? value + '7' : value + '0');
        }

    }

    public class CryptoService
    {
        private const int TokenBytesLength = 13;

        private readonly byte[] _encryptionKey;
        private readonly byte[] _verificationKey;

        public CryptoService()
        {
            _encryptionKey = CryptoHelper.GenerateRandomBytes();
            _verificationKey = CryptoHelper.GenerateRandomBytes();
        }

        public CryptoService(string encryptionKey, string verificationKey)
        {
            _encryptionKey = Encoding.UTF8.GetBytes(encryptionKey);
            _verificationKey = Encoding.UTF8.GetBytes(verificationKey);
        }

        public string CreateSalt()
        {
            var data = new byte[0x10];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);

                return Convert.ToBase64String(data);
            }
        }

        public byte[] Protect(byte[] plainText)
        {
            var initializationVector = new byte[16];
            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(initializationVector);
                return CryptoHelper.Protect(_encryptionKey, _verificationKey, initializationVector, plainText);
            }
        }

        public byte[] Unprotect(byte[] payload)
        {
            return CryptoHelper.Unprotect(_encryptionKey, _verificationKey, payload);
        }

        public string CreateToken(string value)
        {
            var token = new byte[TokenBytesLength];
            var userNameBytes = Encoding.Default.GetBytes(value);

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(token);
                var tokenBytes = token.Concat(userNameBytes)
                                      .ToArray();

                return Convert.ToBase64String(tokenBytes);
            }
        }

        public string GetValueFromToken(string token)
        {
            var tokenBytes = Convert.FromBase64String(token);
            var valueBytes = tokenBytes.Skip(TokenBytesLength)
                                       .ToArray();

            return Encoding.Default.GetString(valueBytes);
        }
    }

#pragma warning restore CS1591
}
