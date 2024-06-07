using TextEncryption.Lib;

string password = "hasło do zaszyfrowania";
StringCipher stringCipher = new("2");
string encryptedPassword = stringCipher.Encrypt(password);
string decryptedPassword = stringCipher.Decrypt(encryptedPassword);

Console.WriteLine($"password = {password}");
Console.WriteLine($"encryptedPassword = {encryptedPassword}");
Console.WriteLine($"decryptedPassword = {decryptedPassword}");

Console.ReadLine();