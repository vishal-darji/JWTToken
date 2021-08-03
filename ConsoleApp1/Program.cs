using JWTTokenGenerator;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TokenGenerator tg = new TokenGenerator();
            var z = tg.GetToken("vishal","password");
        }
    }
}
