namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public class Prospect : Ship
{
    public Prospect(string name) : base(name, cargoCapacity: 1000, speed: 5, rentalRatePerHour: 1500) { }

    public override decimal GetProductionForCycle(int cycleInTrip)
    {
        return cycleInTrip == 1m ? 75.0m : 150.0m;
    }
}