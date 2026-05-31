using System;
using System.Collections.Generic;
using System.Linq;
using DiscordRPC;
using static SubnauticaZeroRP.Maps.HoverMap;

namespace SubnauticaZeroRP.Maps;

public static class BiomeMap
{
    private static readonly Dictionary<string, BiomeData> CaveBiomes = new()
    {
        ["glacialbasin_creaturecave"] = new BiomeData
        {
            Details = Language.main.Get("Frozen_Boi"),
            LargeImageKey = "frozen_boi",
            IsDeep = false
        },
        ["twistybridges_shallow"] = new BiomeData
        {
            Details = Language.main.Get("Shallow_Twisty_Bridges"),
            LargeImageKey = "twistybridges_shallow",
            IsDeep = false
        },
        ["arctickelp_cave"] = new BiomeData
        {
            Details = Language.main.Get("Arctic_Kelp_Caves"),
            LargeImageKey = "arctickelp_cave",
            IsDeep = false
        },
        ["lilypads_deep"] = new BiomeData
        {
            Details = Language.main.Get("Deep_Lilypads_Cave"),
            LargeImageKey = "lilypads_deep",
            IsDeep = false
        },
        ["purplevents_deep"] = new BiomeData
        {
            Details = Language.main.Get("Deep_Purple_Vents"),
            LargeImageKey = "purplevents_deep",
            IsDeep = false
        },
        ["twistybridges_deep"] = new BiomeData
        {
            Details = Language.main.Get("Deep_Twisty_Bridges"),
            LargeImageKey = "twistybridges_deep",
            IsDeep = false
        },
        ["thermalspires_cave"] = new BiomeData
        {
            Details = Language.main.Get("Thermal_Spires_Caves"),
            LargeImageKey = "thermalspires_cave",
            IsDeep = false
        },
        ["twistybridges_cave"] = new BiomeData
        {
            Details = Language.main.Get("Twisty_Bridges_Caves"),
            LargeImageKey = "twistybridges_cave",
            IsDeep = false
        },
        ["crystalcave_castle"] = new BiomeData
        {
            Details = Language.main.Get("Crystal_Castle"),
            LargeImageKey = "crystalcave_castle",
            IsDeep = false
        },
        ["crystalcave_fissure"] = new BiomeData
        {
            Details = Language.main.Get("Crystal_Caves_Fissure"),
            LargeImageKey = "crystalcave_fissure",
            IsDeep = false
        },
        ["treespires_bigtree"] = new BiomeData
        {
            Details = Language.main.Get("Large_Tree_Spire"),
            LargeImageKey = "treespires_bigtree",
            IsDeep = false
        },
        ["lilypads_megaisland_cave"] = new BiomeData
        {
            Details = Language.main.Get("Lilypad_Caves"),
            LargeImageKey = "lilypads_cave",
            IsDeep = false
        },
        ["lilypads_cave"] = new BiomeData
        {
            Details = Language.main.Get("Lilypad_Caves"),
            LargeImageKey = "lilypads_cave",
            IsDeep = false
        },
        ["tundravoid"] = new BiomeData
        {
            Details = Language.main.Get("Tundra_Void"),
            LargeImageKey = "tundravoid",
            IsDeep = false
        },
        // Cut Content, But Appears In Game Sooo
        ["icesheet"] = new BiomeData
        {
            Details = Language.main.Get("Tundra_Void"),
            LargeImageKey = "tundravoid",
            IsDeep = false
        },
        ["watervoid"] = new BiomeData
        {
            Details = Language.main.Get("Water_Void"),
            LargeImageKey = "void",
            IsDeep = false
        }
    };

    private static readonly Dictionary<string, BiomeData> Biomes = new()
    {
        ["arcticspires"] = new BiomeData
        {
            Details = Language.main.Get("Arctic_Spires"),
            LargeImageKey = "arcticspires",
            IsDeep = false
        },
        ["rocketarea"] = new BiomeData
        {
            Details = Language.main.Get("Delta_Island"),
            LargeImageKey = "rocketarea",
            IsDeep = false
        },
        ["glacialbasin"] = new BiomeData
        {
            Details = Language.main.Get("Glacial_Basin"),
            LargeImageKey = "glacialbasin",
            IsDeep = false
        },
        ["glacialbay"] = new BiomeData
        {
            Details = Language.main.Get("Glacial_Bay"),
            LargeImageKey = "glacialbay",
            IsDeep = false
        },
        ["westarctic"] = new BiomeData
        {
            Details = Language.main.Get("Arctic"),
            LargeImageKey = "arctic",
            IsDeep = false
        },
        ["eastarctic"] = new BiomeData
        {
            Details = Language.main.Get("Arctic"),
            LargeImageKey = "arctic",
            IsDeep = false
        },
        ["arctickelp"] = new BiomeData
        {
            Details = Language.main.Get("Arctic_Kelp_Forest"),
            LargeImageKey = "arctickelp",
            IsDeep = false
        },
        ["lilypads"] = new BiomeData
        {
            Details = Language.main.Get("Lilypad_Islands"),
            LargeImageKey = "lilypads",
            IsDeep = false
        },
        ["purplevents"] = new BiomeData
        {
            Details = Language.main.Get("Purple_Vents"),
            LargeImageKey = "purplevents",
            IsDeep = false
        },
        ["sparsearctic"] = new BiomeData
        {
            Details = Language.main.Get("Sparse_Arctic"),
            LargeImageKey = "sparsearctic",
            IsDeep = false
        },
        ["thermalspires"] = new BiomeData
        {
            Details = Language.main.Get("Thermal_Spires"),
            LargeImageKey = "thermalspires",
            IsDeep = false
        },
        ["treespires"] = new BiomeData
        {
            Details = Language.main.Get("Tree_Spires"),
            LargeImageKey = "treespires",
            IsDeep = false
        },
        ["twistybridges"] = new BiomeData
        {
            Details = Language.main.Get("Twisty_Bridges"),
            LargeImageKey = "twistybridges",
            IsDeep = false
        },
        ["worldedge"] = new BiomeData
        {
            Details = Language.main.Get("Worlds_Edge"),
            LargeImageKey = "worldsedge",
            IsDeep = false
        },
        ["crystalcave"] = new BiomeData
        {
            Details = Language.main.Get("Crystal_Caves"),
            LargeImageKey = "crystalcave",
            IsDeep = false
        },
        ["fabricatorcaverns"] = new BiomeData
        {
            Details = Language.main.Get("Fabricator_Caverns"),
            LargeImageKey = "fabricatorcaverns",
            IsDeep = false
        },
        ["glacialconnection"] = new BiomeData
        {
            Details = Language.main.Get("Glacial_Connection"),
            LargeImageKey = "glacialconnection",
            IsDeep = false
        },
        ["miningsite"] = new BiomeData
        {
            Details = Language.main.Get("Koppa_Mining_Site"),
            LargeImageKey = "miningsite",
            IsDeep = false
        },
        ["margbase"] = new BiomeData
        {
            Details = Language.main.Get("Marg_Base"),
            LargeImageKey = "margbase",
            IsDeep = false
        },
        ["void"] = new BiomeData
        {
            Details = Language.main.Get("Void"),
            LargeImageKey = "void",
            IsDeep = false
        }
    };

    public static void MapBiome(RichPresence presence, string biome, Func<string, string> formatter)
    {
        foreach (var kv in CaveBiomes.Where(kv => biome.Contains(kv.Key)))
        {
            Apply(presence, kv.Key, kv.Value, formatter);
            return;
        }

        foreach (var kv in Biomes.Where(kv => biome.Contains(kv.Key)))
        {
            Apply(presence, kv.Key, kv.Value, formatter);
            return;
        }

        var formattedName = char.ToUpper(biome[0]) + biome[1..];
        presence.Details = formatter(formattedName);
        presence.Assets.LargeImageText = GetRandomL("unkbiome");
    }

    private static void Apply(RichPresence presence, string key, BiomeData data, Func<string, string> formatter)
    {
        presence.Details = formatter(data.Details);
        presence.Assets.LargeImageKey = data.LargeImageKey;
        presence.Assets.LargeImageText = GetRandomL(key);
    }
}