// See https://aka.ms/new-console-template for more information

Console.Write("Write some thing: ");
string input = Console.ReadLine();

SimpleHash.SimpleHash myHash = new SimpleHash.SimpleHash();


Console.WriteLine("\n\n//////////////////////// Hash without salt ////////////////////////////");
Console.WriteLine(myHash.Hash(input));


Console.WriteLine("\n\n//////////////////////// Hash with random salt ////////////////////////");
string hash,salt;
hash = myHash.Hash(input, out salt);
Console.WriteLine($"Hash: {hash}");
Console.WriteLine($"Salt: {salt}");


Console.WriteLine("\n\n//////////////////////// Verify hashed text //////////////////////////");
Console.WriteLine($"Is verified: {myHash.Verify(input, hash, salt)}");



Console.ReadKey();
