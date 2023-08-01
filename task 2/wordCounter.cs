using System;

namespace MyNamespace
{
    public class Program
    {
        static Dictionary<string, int> counter;

        public static Dictionary<string, int> countWords(string words)
        {
            int l = 0;
            string temp = "";
            counter = new Dictionary<string, int>();

            while (l < words.Length)
            {
                if (!char.IsLetter(words[l]) && words[l] != ' ')
                {
                    l += 1;
                    continue;
                }
                if (words[l] == ' ')
                {
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        if (counter.ContainsKey(temp))
                            counter[temp] += 1;
                        else
                            counter[temp] = 1;
                        temp = "";
                    }
                }
                else
                {
                    temp += words[l];
                }
                l += 1;
            }

            // Handle the last word
            if (!string.IsNullOrWhiteSpace(temp))
            {
                if (counter.ContainsKey(temp))
                    counter[temp] += 1;
                else
                    counter[temp] = 1;
            }

            return counter;
        }
        public static void Main()
        {
            Console.WriteLine("Enter your sentence:");
            string sentence = Console.ReadLine();
            sentence = sentence.ToLower();
            foreach (var pair in countWords(sentence))
            {
                Console.WriteLine($"Word: {pair.Key}, Count: {pair.Value}");
            }



        }
    }
}
