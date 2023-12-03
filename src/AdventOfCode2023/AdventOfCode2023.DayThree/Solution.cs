using System.Text;

namespace AdventOfCode2023.DayThree;

public static class Solution
{
    private const int Dimension = 140;
    private static readonly char[,] Map = new char[Dimension, Dimension];
    
    public static int PartOne(IEnumerable<string> input)
    {
        ProcessInput(input);
        
        var numIndices = new HashSet<(int, int)>();
        for (var i = 0; i < Dimension; i++)
        {
            for (var j = 0; j < Dimension; j++)
            {
                if (Map[i, j] == '.') continue;
                
                if (IsSymbol(Map[i, j]))
                {
                    CollectAdjacentLeftIndices(i, j, numIndices);
                }
            }
        }

        return numIndices.Select(NumberValueAt).Sum();
    }

    public static int PartTwo(IEnumerable<string> input)
    {
        ProcessInput(input);

        var gearRatios = new List<int>();
        for (var i = 0; i < Dimension; i++)
        {
            for (var j = 0; j < Dimension; j++)
            {
                if (Map[i, j] == '*')
                {
                    AddGearRatio(i, j, gearRatios);
                }
            }
        }
        
        return gearRatios.Sum();
    }

    private static void AddGearRatio(int i, int j, ICollection<int> gearRatios)
    {
        var leftIndices = new HashSet<(int, int)>();
        CollectAdjacentLeftIndices(i, j, leftIndices);
        
        if (leftIndices.Count != 2) return;
        
        var gearRatio = leftIndices.Aggregate(1, (current, x) => current * NumberValueAt(x));
        gearRatios.Add(gearRatio);
    }

    private static void CollectAdjacentLeftIndices(int i, int j, ICollection<(int, int)> indices)
    {
        // check left
        if (j > 0)
        {
            if (IsDigit(Map[i, j - 1]))
            {
                var leftIndex = GetLeftIndex(i, j - 1);
                indices.Add((i, leftIndex));
            }
        }
        
        // check right
        if (j < Dimension - 1)
        {
            if (IsDigit(Map[i, j + 1]))
            {
                indices.Add((i, j + 1));
            }
        }
        
        // check up
        if (i > 0)
        {
            if (IsDigit(Map[i - 1, j]))
            {
                var leftIndex = GetLeftIndex(i - 1, j);
                indices.Add((i - 1, leftIndex));
            }
        }
        
        // check down
        if (i < Dimension - 1)
        {
            if (IsDigit(Map[i + 1, j]))
            {
                var leftIndex = GetLeftIndex(i + 1, j);
                indices.Add((i + 1, leftIndex));
            }
        }
        
        // check up-left
        if (i > 0 && j > 0)
        {
            if (IsDigit(Map[i - 1, j - 1]))
            {
                var leftIndex = GetLeftIndex(i - 1, j - 1);
                indices.Add((i - 1, leftIndex));
            }
        }
        
        // check up-right
        if (i > 0 && j < Dimension - 1)
        {
            if (IsDigit(Map[i - 1, j + 1]))
            {
                var leftIndex = GetLeftIndex(i - 1, j + 1);
                indices.Add((i - 1, leftIndex));
            }
        }
        
        // check down-left
        if (i < Dimension - 1 && j > 0)
        {
            if (IsDigit(Map[i + 1, j - 1]))
            {
                var leftIndex = GetLeftIndex(i + 1, j - 1);
                indices.Add((i + 1, leftIndex));
            }
        }
        
        // check down-right
        if (i < Dimension - 1 && j < Dimension - 1)
        {
            if (IsDigit(Map[i + 1, j + 1]))
            {
                var leftIndex = GetLeftIndex(i + 1, j + 1);
                indices.Add((i + 1, leftIndex));
            }
        }
    }

    private static int GetLeftIndex(int i, int j)
    {
        if (!IsDigit(Map[i, j])) throw new ArgumentException("Must be a digit", $"[{i}, {j}]");
        
        if (j == 0) return j;
        
        while (j > 0 && IsDigit(Map[i, j - 1]))
        {
            j--;
        }
        
        return j;
    }

    private static int NumberValueAt((int i, int j) x)
    {
        var sb = new StringBuilder();
        while (x.j < Dimension && IsDigit(Map[x.i, x.j]))
        {
            sb.Append(Map[x.i, x.j]);
            x.j++;
        }
        return int.Parse(sb.ToString());
    }

    private static bool IsSymbol(char c)
    {
        return !IsDigit(c);
    }

    private static bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    private static void ProcessInput(IEnumerable<string> input)
    {
        var i = 0;
        foreach (var line in input)
        {
            for (var j = 0; j < line.Length; j++)
            {
                Map[i, j] = line[j];
            }
            i++;
        }
    }
}