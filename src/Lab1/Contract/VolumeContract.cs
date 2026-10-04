namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public class VolumeContract : Contract
{
    public double TargetVolume { get; }

    public VolumeContract(
        string asteroidBelt,
        string[] fleet,
        Dictionary<string, int> priceList,
        double targetVolume) // целевой объем
        : base(asteroidBelt, fleet, priceList, "volume")
    {
        TargetVolume = targetVolume;
    }

    public override bool ShouldContinue(int currentHours, decimal currentVolume)
    {
        return currentVolume < TargetVolume;
    }
}