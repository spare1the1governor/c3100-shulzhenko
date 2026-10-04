namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public class Epithal : Ship
{
    public Epithal(string name) : base(name, cargoCapacity: 3000, speed: 2, rentalRatePerHour: 500) { }

    public override decimal GetProductionForCycle(int cycleInTrip)
    {
        return 0;
    }
}

// перевозчик //
