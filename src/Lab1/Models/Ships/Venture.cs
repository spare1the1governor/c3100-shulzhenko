namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

public class Venture : Ship
{
    public Venture(string name) : base(name, cargoCapacity: 400, speed: 4, rentalRatePerHour: 1000) { }

    public override decimal GetProductionForCycle(int cycleInTrip)
    {
    return 100;
    } // Всегда 100
}