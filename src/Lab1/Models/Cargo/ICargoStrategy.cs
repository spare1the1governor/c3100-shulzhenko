using FleetBase = Itmo.ObjectOrientedProgramming.Lab1.Models.Ships.Fleet;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Cargo;

public interface ICargoStrategy
{
    // Проверка - поместится ли вся добыча?
    bool CanPlace(IReadOnlyList<FleetBase.ShipState> allStates, FleetBase.ShipState minerState, decimal amount); // allst - список состояний всех кораблей флота

    // разложить руду по трюмам
    void Place(IReadOnlyList<FleetBase.ShipState> allStates, FleetBase.ShipState minerState, decimal amount);
}