<Query Kind="Program" />

//http://madskristensen.net/post/Generate-random-password-in-C
void Main()
{
    var passwords = Enumerable.Range(1, 10);
    passwords.ToList().ForEach(i=> GenerateRandomPassword(32, i).Dump());
    
    var test = (passwords.Count() == passwords.Distinct().Count());
    test.Dump("are the passwords distinct?");
}

static string GenerateRandomPassword(int passwordLength, int seed)
{
    var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
    var chars = new char[passwordLength];
    var r = new Random(seed);

    for (int i = 0; i < passwordLength; i++)
    {
        chars[i] = allowedChars[r.Next(0, allowedChars.Length)];
    }

    return new string(chars);
}