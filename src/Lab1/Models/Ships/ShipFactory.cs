namespace Itmo.ObjectOrientedProgramming.Lab1.Models.Ships;

// Pure Fabrication //
public class ShipFactory
{
    public static Ship CreateShip(string shipName)
    {
        ArgumentNullException.ThrowIfNull(shipName);

        return shipName.ToLowerInvariant() switch
        {
            "epithal" => new Epithal("Epithal"),
            "prospect" => new Prospect("Prospect"),
            "venture" => new Venture("Venture"),
            _ => throw new ArgumentException($"Неизвестный тип корабля: {shipName}", nameof(shipName)),
        };
}
}
