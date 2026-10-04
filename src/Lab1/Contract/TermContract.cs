namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public class TermContract : Contract
{
    public int MaxHours { get; }

    public TermContract(
        string asteroidBelt,
        string[] fleet,
        Dictionary<string, int> priceList,
        int maxHours) // <-- Добавили срок
        : base(asteroidBelt, fleet, priceList, "term")
    {
        MaxHours = maxHours;
    }

    public override bool ShouldContinue(int currentHours, decimal currentVolume)
    {
        return currentHours < MaxHours;
    }
}