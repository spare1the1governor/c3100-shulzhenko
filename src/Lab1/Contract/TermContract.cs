using Itmo.ObjectOrientedProgramming.Lab1.Models.AsterBelts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public class TermContract : ContractBase
{
    public int MaxHours { get; }

    public TermContract(
        AsterBelt asteroidBelt,
        Fleet fleet,
        Dictionary<string, int> priceList,
        int maxHours)
        : base(asteroidBelt, fleet, priceList, "term")
    {
        if (maxHours <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxHours), "Срок контракта должен быть больше 0.");

        MaxHours = maxHours;
    }

    public override ValidityResponse Validate()
    {
        ValidityResponse baseResult = base.Validate();

        if (!baseResult.IsValid)
            return baseResult;

        // полет туда + 1 час работы + полет обратно
        decimal minimalTime = (AsteroidBelt.Distance / Fleet.Speed * 2) + 1;

        // проверка 3го требования
        if (minimalTime > MaxHours)
        {
            return new ValidityResponse(
                false,
                "Срок контракта меньше времени полета и работы.");
        }

        return new ValidityResponse(true, null);
    }

    public override bool ShouldContinue(int currentHours, decimal currentVolume)
    {
        // требование начала рейса
        return currentHours < MaxHours;
    }
}