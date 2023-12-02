namespace AdventOfCode2023.DayTwo;

public static class Solution
{
    private const int AllowedRed = 12;
    private const int AllowedGreen = 13;
    private const int AllowedBlue = 14;

    public static int PartOne(IEnumerable<string> input)
    {
        return input.Select(ParseLine).Where(g =>
                g.Selections.All(s =>
                    s is
                    {
                        Red.Quantity: <= AllowedRed, Green.Quantity: <= AllowedGreen, Blue.Quantity: <= AllowedBlue
                    }))
            .Sum(x => x.Id);
    }

    public static int PartTwo(IEnumerable<string> input)
    {
        return input.Select(ParseLine).Select(FewestCubes).Sum(Power);
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
            var quantity = int.Parse(parts[0]);
            var color = parts[1];
            switch (color)
            {
                case "red":
                    redCube = new Cube(quantity);
                    break;
                case "green":
                    greenCube = new Cube(quantity);
                    break;
                case "blue":
                    blueCube = new Cube(quantity);
                    break;
            }
        }

        return new Selection(redCube, greenCube, blueCube);
    }

    private static Selection FewestCubes(Game game)
    {
        var maxRed = 0;
        var maxGreen = 0;
        var maxBlue = 0;
        foreach (var selection in game.Selections)
        {
            if (selection.Red.Quantity > maxRed) maxRed = selection.Red.Quantity;
            if (selection.Green.Quantity > maxGreen) maxGreen = selection.Green.Quantity;
            if (selection.Blue.Quantity > maxBlue) maxBlue = selection.Blue.Quantity;
        }

        return new Selection(new Cube(maxRed), new Cube(maxGreen), new Cube(maxBlue));
    }

    private static int Power(Selection selection)
    {
        return selection.Red.Quantity * selection.Green.Quantity * selection.Blue.Quantity;
    }
}

public readonly record struct Cube(int Quantity);

public readonly record struct Selection(Cube Red, Cube Green, Cube Blue);

public readonly record struct Game(int Id, IEnumerable<Selection> Selections);