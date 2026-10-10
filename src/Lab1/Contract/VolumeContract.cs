using Itmo.ObjectOrientedProgramming.Lab1.Models.AsterBelts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public class VolumeContract : ContractBase
{
    public decimal TargetVolume { get; }

    public VolumeContract(
        AsterBelt asteroidBelt,
        Fleet fleet,
        Dictionary<string, int> priceList,
        decimal targetVolume) // целевой объем
        : base(asteroidBelt, fleet, priceList, "volume")
    {
        TargetVolume = targetVolume;
    }

    public override ValidityResponse Validate()
    {
        return base.Validate();
    }

    public override bool ShouldContinue(int currentHours, decimal currentVolume)
    {
        // требование начала рейса
        return currentVolume < TargetVolume;
    }
}