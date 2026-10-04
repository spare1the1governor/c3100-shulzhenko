using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public abstract record Ore
(
    string OreType,
    decimal OreValue,
    Dictionary<Mineral, decimal> ExitOfUnit);
