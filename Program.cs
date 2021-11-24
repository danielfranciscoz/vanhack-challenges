using System;

namespace base64
{
    class Program
    {
        static void Main(string[] args)
        {
            //  Console.WriteLine(Challenge.ToBase64("Hola Mundo"));

            foreach (var item in ChallengeMorceCode.Possibilities(".?"))
            {
                Console.WriteLine(item);
            }
         ;
        }
    }
}
