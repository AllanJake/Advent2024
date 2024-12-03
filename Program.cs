using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

void Main()
{
    string workingDir = Environment.CurrentDirectory;
    string projectDir = Directory.GetParent(workingDir).Parent.Parent.FullName;
    string file = File.ReadAllText(string.Join("\\", projectDir, "data.txt"));
    string testCase1 = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
    string testCase2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
    //PartOne(file);
    PartTwo(file);
}

Main();

int PartOne(string input)
{
    // Matches pattern mul(X,Y) where X, Y can be a 1-3 digit number
    var pattern = @"mul\(\d{1,3},\d{1,3}\)";
    MatchCollection matches = Regex.Matches(input, pattern);
    List<KeyValuePair<int, int>> pairs = new List<KeyValuePair<int, int>>();
    foreach (Match match in matches)
    {
        Console.WriteLine(match.Value);
        string[] split = match.Value.Split(",");
        int X = int.Parse(Regex.Match(split[0], @"\d+").Value);
        int Y = Int32.Parse(Regex.Match(split[1], @"\d+").Value);
        pairs.Add(new KeyValuePair<int, int>(X, Y));
    }

    // Multiple and add the values in pairs
    int sum = 0;
    foreach (KeyValuePair<int, int> pair in pairs)
    {
        sum += pair.Key * pair.Value;
    }
    Console.WriteLine(string.Format("Sum Total: {0}", sum));
    return sum;
}

void PartTwo(string input)
{
    // Find all the indexes of "do()" and "don't()"
    var dos = AllIndexesOf(input, "do()", true);
    var donts = AllIndexesOf(input, "don't()", false);
    SortedList<int, bool> combined = new SortedList<int, bool>();
    dos.ToList().ForEach(x => combined.Add(x.Key, x.Value));
    donts.ToList().ForEach(x => combined.Add(x.Key, x.Value));


    List<string> acceptedStrings = new List<string>();
    Dictionary<int, bool> newList = new Dictionary<int, bool>();
    // Get the string up to the first don't
    acceptedStrings.Add(input.Substring(0, combined.GetKeyAtIndex(0)));
    bool currentState = combined.GetValueAtIndex(0);
    newList.Add(combined.GetKeyAtIndex(0), combined.GetValueAtIndex(0));

    // Ignore any repeating statements
    for (int i = 1; i < combined.Count; i++)
    {
        if (combined.GetValueAtIndex(i) == currentState) continue;
        newList.Add(combined.GetKeyAtIndex(i), combined.GetValueAtIndex(i));
        currentState = combined.GetValueAtIndex(i);
    }
    Console.WriteLine("StringLength: " + input.Length);
    foreach (var item in newList)
    {
        Console.WriteLine(String.Format("{0}: {1}", item.Key, item.Value));
    }
    // Create the substrings
    for (int i = 1; i < newList.Count - 1; i+=2)
    {
        string text = input.Substring(newList.ElementAt(i).Key, newList.ElementAt(i + 1).Key - newList.ElementAt(i).Key);
        acceptedStrings.Add(text);
    }

    int sum = 0;
    foreach (string s in acceptedStrings)
    {
        sum += PartOne(s);
    }
    Console.WriteLine("Part Two Sum: " + sum);
}

Dictionary<int, bool> AllIndexesOf(string str, string value, bool isDo)
{
    if (String.IsNullOrEmpty(value))
    {
        Console.WriteLine("The string to find may not be empty");
        return null;
    }

    Dictionary<int, bool> indexes = new Dictionary<int, bool>();
    for (int i = 0;; i += value.Length)
    {
        i = str.IndexOf(value, i);
        if (i == -1)
            return indexes;
        indexes.Add(i, isDo);
    }
}
