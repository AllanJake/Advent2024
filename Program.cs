using System.IO;

void Main()
{
    string workingDir = Environment.CurrentDirectory;
    string projectDir = Directory.GetParent(workingDir).Parent.Parent.FullName;
    string file = File.ReadAllText(string.Join("\\", projectDir, "data.txt"));
    List<int> left = new List<int>();
    List<int> right = new List<int>();
    List<int> results = new List<int>();

    if (file != null && !string.IsNullOrEmpty(file))
    {
        string[] lines = file.Split('\n');
        foreach (string line in lines)
        {
            string[] split = line.Split(' ');

            left.Add(int.Parse(split[0]));
            right.Add(int.Parse(split[3]));
        }
        if (left.Count != right.Count)
        {
            Console.WriteLine("Two columns are not equal");
            return;
        }
        // Sort both the lists in ascending order
        left.Sort();
        right.Sort();

        // Use each left int as query and find it's occurences in the right list.
        foreach (int query in left)
        {
            int occurrences = 0;
            foreach (int test in right)
            {
                if (test == query) { occurrences++; }
            }
            results.Add(query * occurrences);
        }

        // Add up the diffs
        int sum = results.Sum();
        Console.WriteLine(string.Format("Final number: {0}", sum));
    }
    else
    {
        Console.WriteLine("Failed to read file");
    }
}
Main();