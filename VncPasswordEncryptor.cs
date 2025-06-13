using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

public static class VncPasswordEncryptor
{
    public static byte[] ConvertToEncryptedVncPassword(SecureString password)
    {
        if (password == null)
            throw new ArgumentNullException(nameof(password));

        // Hardcoded magic key used in VNC applications like TightVNC
        byte[] magicKey = new byte[] { 0xE8, 0x4A, 0xD6, 0x60, 0xC4, 0x72, 0x1A, 0xE0 };
        Encoding ansi = Encoding.GetEncoding(
            System.Globalization.CultureInfo.CurrentCulture.TextInfo.ANSICodePage);

        // Convert SecureString to string
        string pass = ConvertToUnsecureString(password);
        int byteCount = ansi.GetByteCount(pass);

        if (byteCount > 8)
        {
            throw new ArgumentException("Password must not exceed 8 characters", nameof(password));
        }

        // Prepare byte array for encryption (pad with zeros if needed)
        byte[] toEncrypt = new byte[8];
        ansi.GetBytes(pass, 0, pass.Length, toEncrypt, 0);

        using (DES des = DES.Create())
        {
            des.Padding = PaddingMode.None;
            using (ICryptoTransform encryptor = des.CreateEncryptor(magicKey, new byte[8]))
            {
                byte[] data = new byte[8];
                encryptor.TransformBlock(toEncrypt, 0, toEncrypt.Length, data, 0);
                return data;
            }
        }
    }

    private static string ConvertToUnsecureString(SecureString secureString)
    {
        if (secureString == null)
            throw new ArgumentNullException(nameof(secureString));

        IntPtr ptr = IntPtr.Zero;
        try
        {
            ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(secureString);
            return System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
        }
        finally
        {
            if (ptr != IntPtr.Zero)
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
        }
    }
}