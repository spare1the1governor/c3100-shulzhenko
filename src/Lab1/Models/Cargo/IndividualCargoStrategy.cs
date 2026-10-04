using FleetBase = Itmo.ObjectOrientedProgramming.Lab1.Models.Ships.Fleet;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Cargo;

public class IndividualCargoStrategy : ICargoStrategy
{
    public bool CanPlace(IReadOnlyList<FleetBase.ShipState> allStates, FleetBase.ShipState minerState, decimal amount)
    {
        return minerState.FreeSpace >= amount;
    }

    public void Place(IReadOnlyList<FleetBase.ShipState> allStates, FleetBase.ShipState minerState, decimal amount)
    {
        minerState.CurrentCargo += amount;
    }
}