using System;
using System.Security.Cryptography;
using System.Text;

namespace GenAdminPasswordHashFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generate admin hash file");
            byte[] hash_b = SHA256.HashData(Encoding.UTF8.GetBytes("zmhVVCb*MtanC2^a7V"));
            System.IO.File.WriteAllBytes("default_admin_cred.bin", hash_b);
        }
    }
}
