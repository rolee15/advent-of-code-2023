using System.Diagnostics;
using AdventOfCode2023.DayFive;

var input = File.ReadAllLines("input.txt");

var resultOne = Solution.PartOne(input);

Console.WriteLine(resultOne);

var sw = new Stopwatch();
sw.Start();

var resultTwo = Solution.PartTwo(input);

Console.WriteLine(resultTwo);

sw.Stop();
Console.WriteLine(sw.Elapsed);