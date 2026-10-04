using FleetBase = Itmo.ObjectOrientedProgramming.Lab1.Models.Ships.Fleet;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Cargo;

public class SharedCargoStrategy : ICargoStrategy
{
    public bool CanPlace(IReadOnlyList<FleetBase.ShipState> allStates, FleetBase.ShipState minerState, decimal amount)
    {
        return allStates.Sum(s => s.FreeSpace) >= amount;
    }

    public void Place(IReadOnlyList<FleetBase.ShipState> allStates, FleetBase.ShipState minerState, decimal amount)
    {
        decimal remaining = amount;

        // Сначала кладем в сам добытчик
        decimal toSelf = Math.Min(minerState.FreeSpace, remaining);
        minerState.CurrentCargo += toSelf;
        remaining -= toSelf;

        // Остаток раскладываем по флоту
        if (remaining > 0)
        {
            foreach (FleetBase.ShipState state in allStates)
            {
                if (state == minerState) continue;

                decimal toAdd = Math.Min(state.FreeSpace, remaining);
                state.CurrentCargo += toAdd;
                remaining -= toAdd;

                if (remaining <= 0) break;
            }
        }
    }
}