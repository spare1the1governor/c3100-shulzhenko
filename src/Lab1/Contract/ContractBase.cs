using Itmo.ObjectOrientedProgramming.Lab1.Models.AsterBelts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

namespace Itmo.ObjectOrientedProgramming.Lab1.Contract;

public abstract class ContractBase
{

   

    public AsterBelt AsteroidBelt { get; }

    public Fleet Fleet { get; }

    public Dictionary<string, int> PriceList { get; }

    public string ContractType { get; protected set; }

    protected ContractBase(AsterBelt asteroidBelt, Fleet fleet, Dictionary<string, int> priceList, string contractType)
    {
        // Проверка пояса астероидов
        if (asteroidBelt == null)
            throw new ArgumentException("Название пояса астероидов не может быть пустым.", nameof(asteroidBelt));

        if (fleet == null || fleet.Length == 0)
            throw new ArgumentException("Флот не может быть пустым.", nameof(fleet));

        // Проверка прайс-листа
        if (priceList == null || priceList.Count == 0)
            throw new ArgumentException("Прайс-лист не должен быть пустым.", nameof(priceList));

        foreach ((string resurs, int price) in priceList)
            {
                if (price <= 0)
                {
                    throw new ArgumentException($"Цена для ресурса '{resurs}' должна быть больше 0 (получено: {price}).", nameof(priceList));
                }
        }

        AsteroidBelt = asteroidBelt;
        Fleet = fleet;
        PriceList = priceList;
        ContractType = contractType;
    }

    public ValidityResponse isValid()
    {
        decimal totalProduction = 0;
        foreach (var ship in Fleet.ships)
        {
            totalProduction += ship.GetProductionForCycle();
        }
        if (totalProduction == 0)
            return new ValidityResponse
            {
                isValid = false,
                notValidReason = "No producing ships"
            };

        var minerals = AsteroidBelt.OreType.ExitOfUnit.keys();
        foreach (var m in minerals)
        {
            if (!PriceList.ContainKey(m))
                return new ValidityResponse
                {
                    isValid = false,
                    notValidReason = $"Price of {nameof(m)} is not listed"
                };
        }

        return new ValidityResponse { isValid = true };
    }

    public abstract bool ShouldContinue(int currentHours, decimal currentVolume);
}
