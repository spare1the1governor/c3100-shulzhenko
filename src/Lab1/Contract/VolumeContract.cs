using Itmo.ObjectOrientedProgramming.Lab1.Models.AsterBelts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public class VolumeContract : Contract
{
    public double TargetVolume { get; }

    public VolumeContract(
        AsterBelt asteroidBelt,
        Fleet fleet,
        Dictionary<string, int> priceList,
        double targetVolume) // целевой объем
        : base(asteroidBelt, fleet, priceList, "volume")
    {
        TargetVolume = targetVolume;
    }

    public override ValidityResponse isValid()
    {
        return base().isValid();
    }

    public override bool ShouldContinue(int currentHours, decimal currentVolume)
    {
        return currentVolume < TargetVolume;
    }
}