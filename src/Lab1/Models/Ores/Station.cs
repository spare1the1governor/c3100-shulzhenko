using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public class Station
{
    private const decimal PortionSize = 100;

    public decimal Tax { get; }

    public Ore CurrentOre { get; }

    // Единицы руды на складе, не дошедшие до полной порции
    public decimal Warehouse { get; private set; }

    public Station(decimal tax, Ore currentOre)
    {
        Tax = tax;
        CurrentOre = currentOre;
    }

    // Принимает объём, добытый за рейс, возвращает минералы за этот рейс
    public Dictionary<Mineral, decimal> Process(decimal minedVolume)
    {
        decimal newUnits = Math.Truncate(minedVolume / CurrentOre.OreValue);
        Warehouse += newUnits;

        decimal portions = Warehouse / PortionSize;
        Warehouse %= PortionSize;

        Dictionary<Mineral, decimal> mined = new Dictionary<Mineral, decimal>();

        foreach ((Mineral mineral, decimal unit) in CurrentOre.ExitOfUnit)
        {
            mined[mineral] = portions * unit;
        }

        return mined;
    }

    public decimal Revenue(Dictionary<Mineral, decimal> minerals, Dictionary<string, decimal> priceList)
    {
        decimal revenue = 0;

        foreach ((Mineral mineral, decimal amount) in minerals)
        {
            if (!priceList.TryGetValue(mineral.ToString(), out decimal price))
            {
                throw new ArgumentException(
                    $"В прайс-листе нет цены для '{mineral}'", nameof(priceList));
            }

            revenue += amount * price;
        }

        return revenue * (1 - Tax);
    }
}