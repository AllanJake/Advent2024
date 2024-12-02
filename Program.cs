using System.IO;
using System.Runtime.CompilerServices;

void Main()
{
    string workingDir = Environment.CurrentDirectory;
    string projectDir = Directory.GetParent(workingDir).Parent.Parent.FullName;
    string file = File.ReadAllText(string.Join("\\", projectDir, "data.txt"));
    int safeCount = 0;
    if (file != null && !string.IsNullOrEmpty(file))
    {
        string[] lines = file.Split("\n");

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 65) Console.WriteLine("");
            List<int> lineVals = lines[i].Split(" ").Select(n => Convert.ToInt32(n)).ToList();
            int toleranceCount = 0;
            bool dir = DirectionSame(lineVals);
            if (!dir && toleranceCount == 0 )
            {
                dir = DirectionSame(lineVals);
                toleranceCount++;
            }
            
            bool diff = DifferenceSafe(lineVals);
            if (!diff && toleranceCount == 0)
            {
                diff = DifferenceSafe(lineVals);
            }

            bool safe = dir && diff;
            if (safe) safeCount++;
        }

        Console.WriteLine(string.Format("Safe Reports: {0}", safeCount));
    }
    else
    {
        Console.WriteLine("File was null or empty");
    }
}

bool DirectionSame(List<int> vals)
{
    bool isAscending = false;
    // Check the first two vals to see if they are ascending or descending and not equal
    if (vals[0] == vals[1])
    {
        vals.RemoveAt(0);
        return false;
    }
    if (vals[0] < vals[1])
    {
        isAscending = true;
    }

    for (int i = 2; i < vals.Count; i++)
    {
        int prev = vals[i - 1];
        int curr = vals[i];
        // Check the rest of the values in the array to see if they match
        if (isAscending)
        {
            if (vals[i - 1] >= vals[i])
            {
                vals.RemoveAt(i);
                return false;
            }
        }
        else
        {
            if (vals[i - 1] <= vals[i])
            {
                vals.RemoveAt(i);
                return false;
            }
        }
    }

    return true;
}

bool DifferenceSafe(List<int> vals)
{
    for (int i = 1; i < vals.Count; i++)
    {
        if (Math.Abs(vals[i - 1] - vals[i]) > 3)
        {
            vals.RemoveAt(i);
            return false;
        }
    }

    return true;
}




Main();