using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using System.IO;
using System.Security.Cryptography;

namespace Rybird.Framework
{
    public class iOSAesEncryptionProvider : IEncryptionProvider
    {
        /// <summary>
        ///     Encrypts the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public Stream EncryptStream(Stream stream)
        {
            var aes = GetAES();
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.CopyTo(memoryStream);
                    cryptoStream.FlushFinalBlock();

                    aes.Clear();

                    return memoryStream;
                }
            }
        }

        /// <summary>
        ///     Decrypts the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns></returns>
        public Stream DecryptStream(Stream stream)
        {
            var aes = GetAES();
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.CopyTo(memoryStream);
                    cryptoStream.FlushFinalBlock();

                    aes.Clear();

                    return memoryStream;
                }
            }
        }

        private static AesManaged GetAES()
        {
            // Generate a Key based on a Password, Salt and HMACSHA1 pseudo-random number generator
            // TODO: TS There are so many better ways to do this. Relic from the POC project, once we're farther along we'll want to inspect the FileCrypto project and revamp this based on that. Ask TScheffert or WHeineman about FileCrypto.
            byte[] salt = Encoding.UTF8.GetBytes("12345678");
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes("Mellmen123!", salt);

            // Create AES algorithm with 128 bit encryption, judging from the KeySize.
            var aes = new AesManaged { KeySize = 128 };
            aes.Key = rfc2898.GetBytes(aes.KeySize / 8);
            aes.IV = rfc2898.GetBytes(aes.BlockSize / 8);

            return aes;
        }
    }
}