using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public record Kernite(string OreType) : Ore(OreType, OreValue: 1.2m, new Dictionary<Mineral, decimal>
{ { new Mexallon(), 30.0m }, { new Isogen(), 60.0m } });