using System.ComponentModel;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace PhotoCloud.Utility
{
    public static class Util
    {
        /// <summary>
        /// MD5 encryption
        /// </summary>
        public static string EncryptionMD5(string str)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(str);
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// MD5 encryption
        /// </summary>
        public static string EncryptionMD5(Stream stream)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] newBuffer = mi.ComputeHash(stream);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// Base64 encoding
        /// </summary>
        public static string EncodeBase64(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64 decoding
        /// </summary>
        public static string DecodeBase64(string str)
        {
            byte[] outputb = Convert.FromBase64String(str);
            return Encoding.Default.GetString(outputb);
        }

        /// <summary>
        /// DES encryption
        /// </summary>
        public static string EncodeDES(string str, string key)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            byte[] keyBytes = Encoding.Unicode.GetBytes(key);
            byte[] data = Encoding.Unicode.GetBytes(str);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(keyBytes, keyBytes), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);
            CStream.FlushFinalBlock();
            return Convert.ToBase64String(MStream.ToArray());
        }

        /// <summary>
        /// DES decryption
        /// </summary>
        public static string DecodeDES(string str, string key)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            byte[] keyBytes = Encoding.Unicode.GetBytes(key);
            byte[] data = Convert.FromBase64String(str);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(keyBytes, keyBytes), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);
            CStream.FlushFinalBlock();
            return Encoding.Unicode.GetString(MStream.ToArray());
        }

        /// <summary>
        /// Get the description of the enumeration
        /// </summary>
        public static string ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());
            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (null != attrs && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumeration.ToString();
        }

        /// <summary>
        /// Convert int value to a enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="intValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T ConvertIntToEnum<T>(int intValue) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), intValue))
            {
                return (T)Enum.ToObject(typeof(T), intValue);
            }
            else
            {
                throw new ArgumentException($"The value {intValue} is not a valid {typeof(T)} enum value.");
            }
        }

        /// <summary>
        /// Convert string to a enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="intValue"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static T ConvertStringToEnum<T>(string str) where T : Enum
        {
            if (Enum.IsDefined(typeof(T), str))
            {
                return (T)Enum.ToObject(typeof(T), str);
            }
            else
            {
                throw new ArgumentException($"The value {str} is not a valid {typeof(T)} enum value.");
            }
        }
    }
}
