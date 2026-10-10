using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;

namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;

public class Station
{
    private const decimal PortionSize = 100;

    public decimal Tax
{
    get => _tax;
    set
    {
        // Проверяем, входит ли значение в диапазон от 0 до 1 включительно
        if (value < 0 || value > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Налог должен быть в диапазоне от 0 до 1.");
        }

        _tax = value;
    }
}

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
        // единицы миниралов полученные из руды
        decimal newUnits = Math.Truncate(minedVolume / CurrentOre.OreValue);
        Warehouse += newUnits;

        decimal portions = Warehouse / PortionSize;
        Warehouse -= portions * PortionSize;

        var mined = new Dictionary<Mineral, decimal>();

        foreach ((Mineral mineral, decimal unit) in CurrentOre.ExitOfUnit)
        {
            mined[mineral] = portions * unit;
        }

        return mined;
    }

    public (decimal Revenue, decimal RevenueAfterTax) Revenue(Dictionary<Mineral, decimal> minerals,
     Dictionary<string, int> priceList)
    {
        decimal revenue = 0;
        foreach ((Mineral mineral, decimal amount) in minerals)
        {
            // if (!priceList.TryGetValue(mineral.ToString(), out decimal price))
            // {
            //     throw new ArgumentException(
            //         $"В прайс-листе нет цены для '{mineral}'", nameof(priceList));
            // }
            int price = priceList[mineral.ToString()];

            revenue += amount * price;
        }

        decimal rTax = revenue * (1 - Tax);

        return (revenue, rTax);
    }
}