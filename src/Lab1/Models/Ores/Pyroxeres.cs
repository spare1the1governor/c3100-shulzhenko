using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public record Pyroxeres(string OreType) : Ore(OreType, OreValue: 0.15m, new Dictionary<Mineral, decimal> { { new Mexallon(), 15.0m }, { new Pyerite(), 45.0m } });