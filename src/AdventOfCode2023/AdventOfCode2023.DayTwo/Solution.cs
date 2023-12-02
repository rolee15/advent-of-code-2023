namespace AdventOfCode2023.DayTwo;
public static class Solution
{
    private const int AllowedRed = 12;
    private const int AllowedGreen = 13;
    private const int AllowedBlue = 14;
    
    public static int PartOne(IEnumerable<string> input) =>
        input.Select(ParseLine).Where(g => 
            g.Selections.All(s => 
                s is { Red.Quantity: <= AllowedRed, Green.Quantity: <= AllowedGreen, Blue.Quantity: <= AllowedBlue }))
        .Sum(x => x.Id);
    
    public static int PartTwo(string[] input)
    {
        return 0;
    }
    
    private static Game ParseLine(string line)
    {
        var parts = line.Split(": ");
        var header = parts[0];
        var body = parts[1];
        
        var id = int.Parse(header.Split(' ')[1]);

        var selections = body.Split("; ");
        var cubes = selections.Select(ParseCubes);
        
        return new Game(id, cubes);
    }

    private static Selection ParseCubes(string selection)
    {
        var cubes = selection.Split(", ");

        var redCube = new Cube(0);
        var greenCube = new Cube(0);
        var blueCube = new Cube(0);
        foreach (var cube in cubes)
        {
            var parts = cube.Split(' ');
            var color = parts[0];
            var quantity = int.Parse(parts[1]);
            switch (color)
            {
                case "red":
                    redCube = new Cube(quantity);
                    break;
                case "green":
                    blueCube = new Cube(quantity);
                    break;
                case "blue":
                    greenCube = new Cube(quantity);
                    break;
            }
        }
        return new Selection(redCube, greenCube, blueCube);
    }
}

public readonly record struct Cube(int Quantity);
public readonly record struct Selection(Cube Red, Cube Green, Cube Blue);
public readonly record struct Game(int Id, IEnumerable<Selection> Selections);