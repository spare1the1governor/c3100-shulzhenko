using Itmo.ObjectOrientedProgramming.Lab1.Models.AsterBelts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public class TermContract : Contract
{
    public int MaxHours { get; }

    public TermContract(
        AsterBelt asteroidBelt,
        Fleet fleet,
        Dictionary<string, int> priceList,
        int maxHours) // <-- Добавили срок
        : base(asteroidBelt, fleet, priceList, "term")
    {
        MaxHours = maxHours;
    }

    public override ValidityResponse isValid()
    {
        var baseRes = base().isValid();

        if (baseRes.isValid == false)
            return baseRes;

        var timeWork = MaxHours - (AsteroidBelt.distance / Fleet.Speed) * 2 - 1;
        if (timeWork < 0)
            return new ValidityResponse { isValid = false, notValidReason = "Not enough time" };
    }

    public override bool ShouldContinue(int currentHours, decimal currentVolume)
    {
        return currentHours < MaxHours;
    }
}