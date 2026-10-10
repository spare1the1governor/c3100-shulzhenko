using Itmo.ObjectOrientedProgramming.Lab1.Contract;
using Itmo.ObjectOrientedProgramming.Lab1.Models.AsterBelts;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Cargo;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Minerals;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ores;
using Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class ContractScenarioTests
{
    [Fact(DisplayName = "Сценарий 1. Контракт на срок")]
    public void TimeContract_WithSingleVenture_IsCompleted()
    {
        var belt = new AsterBelt("Пояс A", 4, new Veldspar("Veldspar"));
        var fleet = CreateVentureFleet();
        var contract = new TermContract(belt, fleet, CreatePriceList(), maxHours: 6);
        var station = new Station(0, belt.OreType, 0);
        var simulator = new ContractSimulator();

        ValidityResponse validity = contract.Validate();
        ContractSimulator.SimulationResult result = simulator.Run(
            contract,
            fleet,
            new SharedCargoStrategy(),
            station);

        Assert.True(validity.IsValid);
        Assert.Equal(1, result.TotalFlight);
        Assert.Equal(6, result.TotalHours);
        Assert.Equal(400m, result.TotalVolume);
        Assert.Equal(32_000m, result.TotalRevenue);
        Assert.Equal(6_000m, result.RentalCost);
        Assert.Equal(0m, result.TotalTax);
    }

    [Fact(DisplayName = "Сценарий 2. Пустой флот")]
    public void Fleet_WithoutShips_CannotBeCreated()
    {
        Assert.Throws<ArgumentException>(() => new Fleet([]));
    }

    [Fact(DisplayName = "Сценарий 3. Контракт на объём")]
    public void VolumeContract_WithSingleVenture_IsCompleted()
    {
        var belt = new AsterBelt("Пояс A", 4, new Veldspar("Veldspar"));
        var fleet = CreateVentureFleet();
        var contract = new VolumeContract(belt, fleet, CreatePriceList(), targetVolume: 1_000);
        var station = new Station(0, belt.OreType, 0);
        var simulator = new ContractSimulator();

        ValidityResponse validity = contract.Validate();
        ContractSimulator.SimulationResult result = simulator.Run(
            contract,
            fleet,
            new SharedCargoStrategy(),
            station);

        Assert.True(validity.IsValid);
        Assert.Equal(3, result.TotalFlight);
        Assert.Equal(16, result.TotalHours);
        Assert.Equal(1_000m, result.TotalVolume);
        Assert.Equal(80_000m, result.TotalRevenue);
        Assert.Equal(16_000m, result.RentalCost);
        Assert.Equal(0m, result.TotalTax);
    }

    [Fact(DisplayName = "Сценарий 4. Отклонение по сроку")]
    public void TimeContract_TooShort_IsRejected()
    {
        var belt = new AsterBelt("Пояс A", 4, new Veldspar("Veldspar"));
        var contract = new TermContract(belt, CreateVentureFleet(), CreatePriceList(), maxHours: 2);

        ValidityResponse validity = contract.Validate();

        Assert.False(validity.IsValid);
        Assert.Contains("Срок контракта", validity.NotValidReason, StringComparison.Ordinal);
    }

    [Fact(DisplayName = "Сценарий 5. Отклонение по прайс-листу")]
    public void Contract_WithIncompletePriceList_IsRejected()
    {
        var belt = new AsterBelt("Пояс B", 4, new Scordite("Scordite"));
        Dictionary<string, int> priceList = CreatePriceList();
        priceList.Remove(new Pyerite().ToString());
        var contract = new TermContract(belt, CreateVentureFleet(), priceList, maxHours: 6);

        ValidityResponse validity = contract.Validate();

        Assert.False(validity.IsValid);
        Assert.Contains(new Pyerite().ToString(), validity.NotValidReason, StringComparison.Ordinal);
    }

    [Fact]
    public void Contract_WithNoMiningShip_IsInvalid()
    {
        var belt = new AsterBelt("Пояс A", 4, new Veldspar("Veldspar"));
        var fleet = new Fleet([new Epithal("Epithal")]);
        var contract = new TermContract(belt, fleet, CreatePriceList(), maxHours: 6);

        ValidityResponse validity = contract.Validate();

        Assert.False(validity.IsValid);
        Assert.Contains("Ни один корабль", validity.NotValidReason, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void TermContract_WithNonPositiveDuration_CannotBeCreated(int maxHours)
    {
        var belt = new AsterBelt("Пояс A", 4, new Veldspar("Veldspar"));

        Assert.Throws<ArgumentOutOfRangeException>(
            () => { _ = new TermContract(belt, CreateVentureFleet(), CreatePriceList(), maxHours); });
    }

    [Fact]
    public void TermContract_ShouldContinueOnlyBeforeHourLimit()
    {
        var belt = new AsterBelt("Пояс A", 4, new Veldspar("Veldspar"));
        var contract = new TermContract(belt, CreateVentureFleet(), CreatePriceList(), maxHours: 6);

        Assert.True(contract.ShouldContinue(0, 0));
        Assert.True(contract.ShouldContinue(5, 0));
        Assert.False(contract.ShouldContinue(6, 0));
        Assert.False(contract.ShouldContinue(7, 0));
    }

    [Fact]
    public void VolumeContract_ShouldContinueOnlyBeforeVolumeTarget()
    {
        var belt = new AsterBelt("Пояс A", 4, new Veldspar("Veldspar"));
        var contract = new VolumeContract(belt, CreateVentureFleet(), CreatePriceList(), targetVolume: 1_000);

        Assert.True(contract.ShouldContinue(0, 0));
        Assert.True(contract.ShouldContinue(0, 999));
        Assert.False(contract.ShouldContinue(0, 1_000));
        Assert.False(contract.ShouldContinue(0, 1_001));
    }

    private static Dictionary<string, int> CreatePriceList()
    {
        return new Dictionary<string, int>
        {
            [new Tritanium().ToString()] = 4,
            [new Pyerite().ToString()] = 10,
            [new Mexallon().ToString()] = 70,
            [new Isogen().ToString()] = 150,
        };
    }

    private static Fleet CreateVentureFleet()
    {
        return new Fleet([new Venture("Venture")]);
    }
}
