using System;

namespace MyNamespace
{
    public class Program
    {
        public static bool CheckPalindrome(string words)
        {
            int l = 0;
            int r = words.Length - 1;

            while (l < r)
            {
                if (words[l] != words[r])
                {
                    return false;
                }
                l += 1;
                r -= 1;
            }

            return true;
        }

        public static void Main()
        {
            Console.WriteLine("Enter your Words:");
            string randomWord = Console.ReadLine();
            string temp = "";

            foreach (char character in randomWord)
            {
                if (char.IsLetter(character))
                {
                    temp += character;
                }
            }

            if (CheckPalindrome(temp))
            {
                Console.WriteLine("This is a palindrome.");
            }
            else
            {
                Console.WriteLine("This is not a palindrome.");
            }
        }
    }
}
