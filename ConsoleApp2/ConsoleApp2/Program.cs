using System.Security.Cryptography;
using System.Text;

public class HashEncryption
{

    private const string EncryptionKey = "HASH64MD5";
    private const string EncryptionKeyApp = "0eed73feb2045049a47ff5820c9c8718";
    private static readonly Lazy<HashEncryption> Instance = new Lazy<HashEncryption>(() => new HashEncryption());
    static string Decrypt(string cipherText)
    {
        try
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
        }
        catch
        {
        }
        return cipherText;
    }   
    public static void Main(string[] args)
    {
        string res = HashEncryption.Decrypt("6uIztaxtirALjv2yCKewcolDz7Vq/OiqvizmwTag6ks=");
        Console.WriteLine($"Password is : {res}");
    }
}


