﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Rigsom.SecureVault.Model.Cryptography
{
    /// <summary>
    /// TODO: Comment
    /// </summary>
    internal static class RijndaelHelper
    {
        /// <summary>
        /// TODO: Comment
        /// </summary>
        private const int KEY_SIZE = 256;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        private const int BLOCK_SIZE = 128;

        /// <summary>
        /// TODO: Comment
        /// </summary>
        /// <param name="decrypt"></param>
        /// <param name="key"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static ICryptoTransform GenerateRijndael(bool decrypt, byte[] key, byte[] salt)
        {
            using (Rfc2898DeriveBytes rfcKey = new Rfc2898DeriveBytes(Encoding.UTF8.GetString(key), salt))
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.KeySize = KEY_SIZE;
                aes.BlockSize = BLOCK_SIZE;
                aes.Key = rfcKey.GetBytes(aes.KeySize / 8);
                aes.IV = rfcKey.GetBytes(aes.BlockSize / 8);
                
                if (decrypt)
                {
                    return aes.CreateDecryptor();
                }
                else
                {
                    return aes.CreateEncryptor();
                }
            }
        }
    }
}
