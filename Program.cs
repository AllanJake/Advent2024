using System.IO;

void Main()
{
    string workingDir = Environment.CurrentDirectory;
    string projectDir = Directory.GetParent(workingDir).Parent.Parent.FullName;
    string file = File.ReadAllText(string.Join("\\", projectDir, "data.txt"));
    List<int> left = new List<int>();
    List<int> right = new List<int>();
    List<int> diffs = new List<int>();

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

        // Get the difference between each item
        for (int i = 0; i < left.Count; i++)
        {
            int min = Math.Min(left[i], right[i]);
            int max = Math.Max(left[i], right[i]);
            diffs.Add(max - min);
        }

        // Add up the diffs
        int sum = diffs.Sum();
        Console.WriteLine(string.Format("Final number: {0}", sum));
    }
    else
    {
        Console.WriteLine("Failed to read file");
    }
}
Main();