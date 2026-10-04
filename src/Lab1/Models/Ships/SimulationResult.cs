namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public class SimulationResult
{
    public int TotalHours { get; }

    public decimal TotalMinedVolume { get; }

    public decimal TotalCost { get; }

    public SimulationResult(int totalHours, decimal totalMinedVolume, decimal totalCost)
    {
        TotalHours = totalHours;
        TotalMinedVolume = totalMinedVolume;
        TotalCost = totalCost;
    }
}