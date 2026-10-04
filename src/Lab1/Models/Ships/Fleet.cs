using Itmo.ObjectOrientedProgramming.Lab1.Contract;

using Itmo.ObjectOrientedProgramming.Lab1.Models.Cargo;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

// Information Expert - так камк хранит список кораблей, является экспертом по оценке их вренды //
public class Fleet
{
    public class ShipState
    {
        public Ship Ship { get; }

        public decimal CurrentCargo { get; set; }

        public bool IsFull => CurrentCargo >= Ship.CargoCapacity;

        public int CycleInCurrentTrip { get; private set; } = 1;

        public ShipState(Ship ship)
        {
            Ship = ship;
        }

        public decimal FreeSpace => Ship.CargoCapacity - CurrentCargo;

        public void AdvanceCycle()
        {
            CycleInCurrentTrip++;
        }

        public void Unload()
        {
            CurrentCargo = 0;
            CycleInCurrentTrip = 1;
        }
    }

    private readonly List<ShipState> _shipStates;

    public readonly Ship[] ships;

    public Fleet(Ship[] ships)
    {
        if (ships.Length == 0)
            throw new ArgumentException("-----В контракте не указаны корабли для флота.");

        this.ships = ships;
        _shipStates = new List<ShipState>(ships.Length);

        foreach (Ship ship in ships)
        {
            _shipStates.Add(new ShipState(ship));
        }
    }

    public int Length => _shipStates.Count;

    // Скорость флота равна скорости самого медленного корабля (по ТЗ)
    public decimal Speed => _shipStates.Min(s => s.Ship.Speed);

    public decimal GetHourlyRent()
    {
        
        decimal hourlyRent = 0;

        foreach (ShipState state in _shipStates)
        {
            hourlyRent += state.Ship.RentalRatePerHour;
        }

        return hourlyRent;
    }

    // Расчет итоговой аренды за переданное количество часов
    public decimal CalculateRentalCost(int hours)
    {
        if (hours < 0)
            throw new ArgumentOutOfRangeException(nameof(hours), "Часы не могут быть отрицательными.");

        return hours * GetHourlyRent();
    }// Полный запуск выполнения контракта с учетом типа контракта и стратегии размещения //

    public SimulationResult ExecuteContract(ContractBase contract, ICargoStrategy strategy)
    {
        int totalHours = 0;
        decimal totalMinedVolume = 0;

        // Симуляция идет циклами по 1 часу
        while (contract.ShouldContinue(totalHours, totalMinedVolume))
        {
            totalHours++;

            foreach (ShipState state in _shipStates)
            {
                // Сколько корабль хочет добыть в текущем цикле своего рейса
                decimal potentialMining = state.Ship.GetProductionForCycle(state.CycleInCurrentTrip);

                if (potentialMining <= 0)
                    continue; // не добываем

                // Проверяем через выбранную стратегию (Раздельный/Общий трюм)
                if (strategy.CanPlace(_shipStates, state, potentialMining))
                {
                    strategy.Place(_shipStates, state, potentialMining);

                    state.AdvanceCycle();
                    totalMinedVolume += potentialMining;
                }
            }

            // Проверяем, заполнился ли флот для отправки на разгрузку
            TryUnloadFullShips();
        }

        decimal totalCost = CalculateRentalCost(totalHours);
        return new SimulationResult(totalHours, totalMinedVolume, totalCost);
    }

    private void TryUnloadFullShips()
    {
        // Если корабль заполнен, производим его разгрузку (сброс рейса)
        foreach (ShipState state in _shipStates)
        {
            if (state.IsFull)
            {
                state.Unload();
            }
        }
    }
}