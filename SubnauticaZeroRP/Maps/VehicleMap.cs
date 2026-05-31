using System;
using System.Collections.Generic;
using System.Linq;
using DiscordRPC;
using static SubnauticaZeroRP.Maps.HoverMap;

namespace SubnauticaZeroRP.Maps;

public class VehicleMap
{
    private static readonly Dictionary<string, VehicleData> Vahicles = new()
    {
        ["exosuit"] = new VehicleData
        {
            State = Language.main.Get("Prawn_Suit_Vehicle"),
            SmallImageKey = "exosuit"
        },
        ["seamoth"] = new VehicleData
        {
            State = Language.main.Get("Seamoth_Vehicle"),
            SmallImageKey = "seamoth"
        }
        // Snowfox & Seatruck Aren't Vehicles....WTH???
    };

    public static void MapVehicle(RichPresence presence, string vehicle,
        Func<string, string> formatter)
    {
        if (string.IsNullOrEmpty(vehicle))
        {
            presence.State = formatter(Language.main.Get("Unknown Vehicle"));
            presence.Assets.SmallImageKey = "unknown";
            presence.Assets.SmallImageText = GetRandomS("unkvehicle");

            return;
        }

        var data = Vahicles.FirstOrDefault(v => vehicle.Contains(v.Key));

        if (data.Key != null)
        {
            Apply(presence, data.Key, data.Value, formatter);
            return;
        }

        var formattedName = char.ToUpper(vehicle[0]) + vehicle[1..];
        presence.State = formatter(formattedName);
        presence.Assets.SmallImageKey = "unknown";
        presence.Assets.SmallImageText = formattedName;
    }

    private static void Apply(RichPresence presence, string key, VehicleData data,
        Func<string, string> formatter)
    {
        presence.State = formatter(data.State);
        presence.Assets.SmallImageKey = data.SmallImageKey;
        presence.Assets.SmallImageText = GetRandomS(key);
    }
}