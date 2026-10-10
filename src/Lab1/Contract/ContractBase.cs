using Itmo.ObjectOrientedProgramming.Lab1.Models.AsterBelts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public abstract class ContractBase
{
    public AsterBelt AsteroidBelt { get; }

    public Fleet Fleet { get; }

    private const int FirstCycle = 1;

    // содержит минерал и его цену за единицу
    public Dictionary<string, int> PriceList { get; }

    public string ContractType { get; protected set; }

    protected ContractBase(AsterBelt asteroidBelt, Fleet fleet, Dictionary<string, int> priceList, string contractType)
    {
        if (asteroidBelt == null)
            ArgumentNullException.ThrowIfNull(asteroidBelt);

        if (fleet == null || fleet.Length == 0)
            throw new ArgumentException("Флот не может быть пустым.", nameof(fleet));

        if (priceList == null || priceList.Count == 0)
            throw new ArgumentException("Прайс-лист не должен быть пустым.", nameof(priceList));

        foreach ((string resource, int price) in priceList)
        {
            if (price <= 0)
            {
                throw new ArgumentException(
                    $"Цена для ресурса '{resource}' должна быть больше 0 (получено: {price}).",
                    nameof(priceList));
            }
        }

        AsteroidBelt = asteroidBelt;
        Fleet = fleet;
        PriceList = priceList;
        ContractType = contractType;
    }

    public virtual ValidityResponse Validate()
    {
        bool anyShipMines = false;

        foreach (Ship ship in Fleet.Ships)
        {
            if (ship.GetProductionForCycle(FirstCycle) > 0)
            {
                anyShipMines = true;
                break;
            }
        }

        // проверка 2го тебования
        if (!anyShipMines)
        {
            return new ValidityResponse(
                false,
                "Ни один корабль не может добывать руду в первом цикле.");
        }

        // проверка 1го требования
        foreach (Mineral mineral in AsteroidBelt.OreType.ExitOfUnit.Keys)
        {
            if (!PriceList.ContainsKey(mineral.ToString()))
            {
                return new ValidityResponse(
                    false,
                    $"В прайс-листе нет цены для '{mineral}'.");
            }
        }

        return new ValidityResponse(true, null);
    }

    public abstract bool ShouldContinue(int currentHours, decimal currentVolume);
}