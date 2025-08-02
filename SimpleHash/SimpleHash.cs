using System.Security.Cryptography;
using System.Text;

namespace SimpleHash;

public class Hashing
{
    //Hash without salt
    public string Hash(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        int state1 = input[0];
        int state2 = 5381;
        int state3 = 997;

        for (int i = 1; i < input.Length; i++)
        {
            int val = input[i];

            if (val > state1)
                state1 = (state1 ^ val) + (val << (i % 3));
            else
                state1 += val * (i + 1);

            state2 = (state2 * 31) + val;
            state3 ^= (val * (i + 17)) >> (i % 5);

            // Normalize to keep within 32 bits
            state1 &= 0x7FFFFFFF;
            state2 &= 0x7FFFFFFF;
            state3 &= 0x7FFFFFFF;
        }

        long hash = ((long)state1 << 32) | (uint)state2;
        hash ^= state3;

        long outputLong = hash & 0x7FFFFFFFFFFFFFFF;
        byte[] output = Encoding.UTF8.GetBytes(outputLong.ToString());


        return Convert.ToBase64String(output);
    }


    //Hash with random salt
    public string Hash(string input,out string salt)
    {
        salt = CreateRandomSalt();
        input = salt + input;

        if (string.IsNullOrEmpty(input))
            return "";
        else
            return Hash(input);
    }


    //Hash with none random salt
    public string Hash(string input, string salt)
    {
        input = salt + input;

        if (string.IsNullOrEmpty(input))
            return "";
        else
            return Hash(input);
    }


    //Create a random salt
    public string CreateRandomSalt(int length = 32)
    {
        if (length > 64)
            length = 64;

        byte[] salt = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }


    //Verify hashed text
    public bool Verify(string plainText, string hashedText)
        => hashedText == Hash(plainText);


    //Verify hashed text with salt
    public bool Verify(string plainText, string hashedText, string salt)
        => hashedText == Hash(plainText, salt);
}

//SLVZ.DEV