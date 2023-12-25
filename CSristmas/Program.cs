namespace CSristmas;

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string sourceCodeText = GetSourceCodeText();
        char[] separators = new char[] { ' ', ',', ';', '.' };
        string[] keywordList = sourceCodeText.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        List<ConsoleCharacter> consoleCharacterList = GetConsoleCharacterList(keywordList);

        int[,] binaryArray = BinanaryImageConverter.ConvertToBinaryImage("C:\\Users\\user\\Documents\\binary_transparent_img_75.png");
        int index = 0;

        Console.WriteLine("result: ");
            
        for (int i = 0; i < binaryArray.GetLength(0); ++i)
        {
            for (int j = 0; j < binaryArray.GetLength(1); ++j)
            {
                if (index >= consoleCharacterList.Count)
                {
                    return;
                }
                if (binaryArray[i, j] == 0)
                {
                    Console.Write(" ");
                } else
                {
                    Console.ForegroundColor = consoleCharacterList[index].Color;
                    Console.Write(consoleCharacterList[index].Text);
                    ++index;
                }
            }
            Console.WriteLine();
        }
    }

    private static List<ConsoleCharacter> GetConsoleCharacterList(string[] keywordList)
    {
        var consoleCharacterList = new List<ConsoleCharacter>(1024);

        foreach (string keyword in keywordList)
        {
            consoleCharacterList.AddRange(
                   from char character in keyword
                   select new ConsoleCharacter(character, GetColor(keyword))
            );
        }

        return consoleCharacterList;
    }

    private static string GetSourceCodeText()
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        var allSources = new StringBuilder();

        foreach (string file in Directory.EnumerateFiles(currentDirectory, "*.cs", SearchOption.AllDirectories))
        {
            string contents = File.ReadAllText(file);
            allSources.AppendLine(contents);
        }

        allSources.Replace("\n", "");

        string keywordList = allSources.ToString();
        return keywordList;
    }

    private static ConsoleColor GetColor(string keyword) => (keyword) switch
    {
        "if" or
        "for" or
        "in" or
        "foreach" or
        "switch" or
        "case" or
        "break" or
        "using" or
        "class" or
        "static" => ConsoleColor.Red,

        "namespace" or
        "{" or
        "}" or
        "=" => ConsoleColor.Yellow,

        "string" or
        "int" or
        "long" or
        "var" or
        "void" or
        "int[]" or
        "int[,]" or
        "char[]" or
       "string[]" => ConsoleColor.White,

        _ => ConsoleColor.DarkGreen,
    };
}