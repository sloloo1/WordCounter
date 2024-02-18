using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;


string str = File.ReadAllText(@"D:\C#\WordCounter\P&W.txt");

str = str.ToLowerInvariant();
str = Regex.Unescape(str);

StringBuilder sb = new StringBuilder();
List<string> listOfFilteredWords = new List<string>();

for (int i = 0; i < str.Length - 1; i++)
{
    if (char.IsLetter(str[i]))
    {
        while (
            Regex.IsMatch(str[i].ToString(), @"^[\p{Ll}-–’\']+$")
            )
        {
            sb.Append(str[i]);
            i++;
        }

        string w = sb.ToString();
        w = w.Trim('\'', '’', '-', '–');

        if (!Regex.IsMatch(w, "^m{0,3}(cm|cd|d?C{0,3})(xl|xc|l?x{0,3})(ix|iv|v?i{0,3})$"))
        {
            listOfFilteredWords.Add(w);
        }

        sb.Clear();
    }

    if (char.IsWhiteSpace(str[i]))
    {
        continue;
    }
}

Dictionary<string, int> wordCounts = new Dictionary<string, int>();

foreach (string word in listOfFilteredWords)
{
    if (wordCounts.ContainsKey(word))
    {
        wordCounts[word]++;
    }
    else
    {
        wordCounts[word] = 1;
    }
}

var sortedWords = wordCounts.OrderByDescending(pair => pair.Value)
                             .ThenBy(pair => pair.Key)
                             .Take(2000);

foreach (var pair in sortedWords)
{
    Console.WriteLine($"{pair.Key} ({pair.Value})");
}

Console.ReadLine();