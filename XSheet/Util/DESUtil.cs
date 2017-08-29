using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Util
{
    /*加密解密工具类*/
    public class DESUtil
    {
        public static string GenerateKey()
        {
            /*DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            Console.WriteLine(ASCIIEncoding.ASCII.GetString(desCrypto.Key));
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);*/
            return "fg2357k9";
        }
        // 加密字符串   
        public static string EncryptString(string sInputString, string sKey)
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }
        // 解密字符串   
        public static string DecryptString(string sInputString, string sKey)
        {
            string[] sInput = sInputString.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }
    }
    /*class Test
    {
        static void Main()
        {
            DesUtil des = new DesUtil();
            string key = des.GenerateKey();
            string s0 = "中国软件 - csdn.net";
            string s1 = des.EncryptString(s0, key);
            string s2 = des.DecryptString(s1, key);
            Console.WriteLine("原串: [{0}]", s0);
            Console.WriteLine("加密: [{0}]", s1);
            Console.WriteLine("解密: [{0}]", s2);
        }
    }*/
}
