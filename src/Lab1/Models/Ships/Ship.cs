namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

// Абстрактный базовый класс корабля. Соответствие принципу Open-closed //
public abstract class Ship
{
    public string Name { get; protected set; }

    // public decimal MiningPerCycle { get; protected set; } //

    public decimal CargoCapacity { get; protected set; }

    public decimal Speed { get; protected set; }

    public decimal RentalRatePerHour { get; protected set; }

    protected Ship(string name, decimal cargoCapacity, decimal speed, decimal rentalRatePerHour)
    {
        Name = name;
        CargoCapacity = cargoCapacity;
        Speed = speed;
        RentalRatePerHour = rentalRatePerHour;
    }

    public abstract decimal GetProductionForCycle(int cycleInTrip);
}
