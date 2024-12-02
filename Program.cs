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

        //var valOne = lines[0].Split(" ").Select(n => Convert.ToInt32(n)).ToArray();
        //var valTwo = lines[1].Split(" ").Select(n => Convert.ToInt32(n)).ToArray();
        //var valThree = lines[2].Split(" ").Select(n => Convert.ToInt32(n)).ToArray();

        //bool one = DirectionSame(valOne) && DifferenceSafe(valOne);
        //bool two = DirectionSame(valTwo) && DifferenceSafe(valTwo);
        //bool three = DirectionSame(valThree) && DifferenceSafe(valThree);


        //Console.WriteLine(string.Format("One: {0} Two: {1} Three: {2}", one, two, three));

        //foreach (string line in lines)
        //{
        //    int[] lineVals = line.Split(" ").Select(n => Convert.ToInt32(n)).ToArray();
        //    bool safe = DirectionSame(lineVals) && DifferenceSafe(lineVals);
        //    if (safe) safeCount++;
        //}

        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 65) Console.WriteLine("");
            int[] lineVals = lines[i].Split(" ").Select(n => Convert.ToInt32(n)).ToArray();
            bool safe = DirectionSame(lineVals) && DifferenceSafe(lineVals);
            if (safe) safeCount++;
        }

        Console.WriteLine(string.Format("Safe Reports: {0}", safeCount));
    }
    else
    {
        Console.WriteLine("File was null or empty");
    }
}

bool DirectionSame(int[] vals)
{
    bool isAscending = false;
    // Check the first two vals to see if they are ascending or descending and not equal
    if (vals[0] == vals[1])
    {
        return false;
    }
    if (vals[0] < vals[1])
    {
        isAscending = true;
    }

    for (int i = 2; i < vals.Length; i++)
    {
        int prev = vals[i - 1];
        int curr = vals[i];
        // Check the rest of the values in the array to see if they match
        if (isAscending)
        {
            if (vals[i - 1] >= vals[i])
            {
                return false;
            }
        }
        else
        {
            if (vals[i - 1] <= vals[i])
            {
                return false;
            }
        }
    }

    return true;
}

bool DifferenceSafe(int[] vals)
{
    for (int i = 1; i < vals.Length; i++)
    {
        if (Math.Abs(vals[i - 1] - vals[i]) > 3)
        {
            return false;
        }
    }

    return true;
}




Main();