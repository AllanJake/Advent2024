using System.Numerics;
using System.Runtime.CompilerServices;

void Main()
{
    string workingDir = Environment.CurrentDirectory;
    string projectDir = Directory.GetParent(workingDir).Parent.Parent.FullName;
    string file = File.ReadAllText(string.Join("\\", projectDir, "data.txt"));
    //string file = File.ReadAllText(string.Join("\\", projectDir, "test.txt"));
    //PartOne(file);
    PartTwo();
}

Main();

void PartOne(string input)
{
    string[] lines = input.Split('\n');

    int rowCount = 0;
    int colCount = 0;
    int forDiagCount = 0;
    int backDiagCount = 0;

    void RowSearch()
    {
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length - 3; x++)
            {
                if (lines[y][x + 0] == 'X' &&
                        lines[y][x + 1] == 'M' &&
                        lines[y][x + 2] == 'A' &&
                        lines[y][x + 3] == 'S')
                    rowCount++;

                if (lines[y][x + 0] == 'S' &&
                        lines[y][x + 1] == 'A' &&
                        lines[y][x + 2] == 'M' &&
                        lines[y][x + 3] == 'X')
                    rowCount++;
            }
        }
    }

    void ColSearch()
    {
        for (int y = 0; y < lines.Length - 3; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (    lines[y + 0][x] == 'X' &&
                        lines[y + 1][x] == 'M' &&
                        lines[y + 2][x] == 'A' &&
                        lines[y + 3][x] == 'S')
                    colCount++;

                if (    lines[y + 0][x] == 'S' &&
                        lines[y + 1][x] == 'A' &&
                        lines[y + 2][x] == 'M' &&
                        lines[y + 3][x] == 'X')
                    colCount++;
            }
        }
    }

    void ForDiag()
    {
        for (int y = 0; y < lines.Length - 3; y++)
        {
            for (int x = 0; x < lines[y].Length - 3; x++)
            {
                if (    lines[y + 0][x + 0] == 'X' &&
                        lines[y + 1][x + 1] == 'M' &&
                        lines[y + 2][x + 2] == 'A' &&
                        lines[y + 3][x + 3] == 'S')
                    colCount++;

                if (    lines[y + 0][x + 0] == 'S' &&
                        lines[y + 1][x + 1] == 'A' &&
                        lines[y + 2][x + 2] == 'M' &&
                        lines[y + 3][x + 3] == 'X')
                    colCount++;
            }
        }
    }

    void BackDiag()
    {
        for (int y = 0; y < lines.Length - 3; y++)
        {
            for (int x = 3; x < lines[y].Length; x++)
            {
                if (    lines[y + 0][x - 0] == 'X' &&
                        lines[y + 1][x - 1] == 'M' &&
                        lines[y + 2][x - 2] == 'A' &&
                        lines[y + 3][x - 3] == 'S')
                    colCount++;

                if (    lines[y + 0][x - 0] == 'S' &&
                        lines[y + 1][x - 1] == 'A' &&
                        lines[y + 2][x - 2] == 'M' &&
                        lines[y + 3][x - 3] == 'X')
                    colCount++;
            }
        }
    }

    RowSearch();
    ColSearch();
    ForDiag();
    BackDiag();

    Console.WriteLine(string.Format("RowCount: {0}, ColCount: {1}, ForwardDiagCount:{2}, BackDiagCount: {3}", rowCount, colCount, forDiagCount, backDiagCount));

    int total = rowCount + colCount + forDiagCount + backDiagCount;
    Console.WriteLine("Total: " + total);
}

void PartTwo()
{
    string workingDir = Environment.CurrentDirectory;
    string projectDir = Directory.GetParent(workingDir).Parent.Parent.FullName;
    var grid = File.ReadAllLines(string.Join("\\", projectDir, "data.txt"))
        .SelectMany((line, r) => line.Select((ch, c) => (r, c, ch)))
        .ToDictionary(tp => new Complex(tp.r, tp.c), tp => tp.ch);

    Complex[][] xOffsets = [
        [new(-1, -1), new(0, 0), new(1, 1)],
        [new(-1, 1), new(0, 0), new(1, -1)]
        ];

    var part2 = grid.Where(kvp =>
                        (   from arr in xOffsets
                            from tp in arr
                            select string.Concat(
                             arr.Select(tp => grid.GetValueOrDefault(tp + kvp.Key, '.')))
                          ).All(str => str == "MAS" || str == "SAM")).Count();
    Console.WriteLine(part2);
}