using System;
using DiscordRPC;
using RichPresenceAPI;
using SubnauticaZeroRP.Maps;
using static SubnauticaZeroRP.Maps.HoverMap;

namespace SubnauticaZeroRP;

public class Discord
{
    private readonly Timestamps _sessionTime = new() { Start = DateTime.UtcNow };
    private DiscordRpcClient _client;
    private bool _hasPresence;

    public void Initialize()
    {
        var appId = string.IsNullOrWhiteSpace(Plugin.ModConfig.AppId)
            ? "1506535741109571705"
            : Plugin.ModConfig.AppId.Trim();

        Plugin.Logger.LogInfo($"Connecting To AppID: {appId}");

        try
        {
            _client = Utility.CreateDiscordRpcClient(appId);
            _client.SkipIdenticalPresence = false;

            _client.OnReady += (_, e) =>
            {
                Plugin.Logger.LogInfo($"Bound Successfully To Discord Profile: {e.User.Username}");
            };

            _client.OnError += (_, e) => { Plugin.Logger.LogError($"API Exception: {e.Message}"); };

            _client.OnConnectionFailed += (_, e) =>
            {
                Plugin.Logger.LogError($"Connection Drop On Pipe Channel: {e.FailedPipe}");
            };

            _client.Initialize();

            Plugin.Logger.LogInfo("Pipeline Initialization Fired");
        }
        catch (Exception e)
        {
            Plugin.Logger.LogError(e.Message);
        }
    }

    public void UpdatePresence(bool runMainMenu)
    {
        if (_client is null) return;

        if (!Plugin.ModConfig.EnableSZRP)
        {
            if (_hasPresence)
            {
                _client.ClearPresence();
                _hasPresence = false;
            }

            return;
        }

        if (runMainMenu || Player.main is null || DayNightCycle.main is null)
        {
            _client.SetPresence(new RichPresence
            {
                Details = Language.main.Get("MainMenu_Details"),
                State = Language.main.Get("MainMenu_State"),
                Timestamps = _sessionTime
            });
            _hasPresence = true;

            return;
        }

        var presence = new RichPresence
        {
            Timestamps = _sessionTime,
            Assets = new Assets()
        };

        AddBiomeInfo(presence);

        if (Player.main.GetVehicle() is not null)
        {
            AddVehicleInfo(presence);
        }
        else if (Player.main.currentInterior is not null &&
                 Player.main.currentInterior.GetType().Name.ToLower().Trim().Contains("lifepod"))
        {
            presence.Details = Language.main.Get("Lifepod_Details");
            presence.State = Language.main.Get("Base_State").Replace("{depth}", Player.main.cachedDepth.ToString());
            presence.Assets.LargeImageKey = "lifepod"; // First Game's Lifepod Is Much Better Sooo
            presence.Assets.LargeImageText = GetRandomL("lifepod");
            presence.Assets.SmallImageKey = "room";
            presence.Assets.SmallImageText = GetRandomS("lifepod");
        }
        else if (Player.main.currentInterior is not null &&
                 Player.main.currentInterior.GetType().Name.ToLower().Trim().Contains("seatruck"))
        {
            presence.State = VehicleState(Language.main.Get("Seatruck_Vehicle"));
            presence.Assets.SmallImageKey = "seatruck";
            presence.Assets.SmallImageText = GetRandomS("seatruck");
        }
        else if (Player.main.inHovercraft)
        {
            presence.State = VehicleState(Language.main.Get("Snowfox_Vehicle"));
            presence.Assets.SmallImageKey = "snowfox";
            presence.Assets.SmallImageText = GetRandomS("hoverbike");
        }
        else if (Player.main.GetCurrentSub() is not null && Player.main.GetCurrentSub().isBase)
        {
            presence.Details = Language.main.Get("Base_Details");
            presence.State = Language.main.Get("Base_State").Replace("{depth}", Player.main.cachedDepth.ToString());
            presence.Assets.SmallImageKey = "room";
            presence.Assets.SmallImageText = GetRandomS("base");
        }
        else if (!Player.main.isUnderwater.value && (Player.main.motorMode == Player.MotorMode.Walk ||
                                                     Player.main.motorMode == Player.MotorMode.Run))
        {
            presence.State = Language.main.Get("Land_State");
            presence.Assets.SmallImageKey = "fins";
            presence.Assets.SmallImageText = GetRandomS("land");
        }
        else
        {
            if (Player.main.motorMode == Player.MotorMode.Seaglide)
            {
                presence.State = Language.main.Get("Seaglide_State")
                    .Replace("{depth}", Player.main.cachedDepth.ToString());
                presence.Assets.SmallImageKey = "seaglide";
                presence.Assets.SmallImageText = GetRandomS("seaglide");
            }
            else if (Player.main.motorMode == Player.MotorMode.CreatureRide)
            {
                presence.State = Language.main.Get("GlowWhale_Ride")
                    .Replace("{depth}", Player.main.cachedDepth.ToString());
                presence.Assets.SmallImageKey = "glowwhale";
                presence.Assets.SmallImageText = GetRandomS("glowwhale");
            }
            else // MotorMode.Dive
            {
                presence.State = Language.main.Get("Swim_State").Replace("{depth}", Player.main.cachedDepth.ToString());
                presence.Assets.SmallImageKey = "fins";
                presence.Assets.SmallImageText = GetRandomS("swim");
            }
        }

        if (!Plugin.ModConfig.EnableHoverText)
        {
            presence.Assets.LargeImageText = null;
            presence.Assets.SmallImageText = null;
        }

        _client.SetPresence(presence);
        _hasPresence = true;
    }

    private static void AddBiomeInfo(RichPresence presence)
    {
        var biomeStr = Player.main.GetBiomeString().ToLower().Trim();

        switch (biomeStr)
        {
            case not null when biomeStr.Contains("precursor"):
            {
                presence.Details = Language.main.Get("Precursor_Details");
                presence.Assets.LargeImageKey = "precursor";
                presence.Assets.LargeImageText = GetRandomL("precursor");
                return;
            }
            case not null when biomeStr.Contains("introicecave"):
            case not null when biomeStr.Contains("startzone"):
            {
                presence.Details = Language.main.Get("Start_Zone");
                presence.Assets.LargeImageKey = "arcticspires";
                presence.Assets.LargeImageText = GetRandomL("startzone");
                return;
            }
            case "<unknown>":
            case "unassignes":
            case "":
            {
                presence.Details = Language.main.Get("UnknownBiome_Details");
                presence.Assets.LargeImageKey = "unknown";
                presence.Assets.LargeImageText = GetRandomL("unk");
                return;
            }
        }

        BiomeMap.MapBiome(presence, biomeStr, BiomeDetails);
    }

    private static void AddVehicleInfo(RichPresence presence)
    {
        var vehicle = Player.main.GetVehicle().GetType().Name.ToLower().Trim();

        VehicleMap.MapVehicle(presence, vehicle, VehicleState);
    }

    private static string VehicleState(string vehicle)
    {
        return Player.main.isPiloting
            ? Language.main.Get("Vehicle_Piloting_State").Replace("{vehicle}", vehicle)
                .Replace("{depth}", Player.main.cachedDepth.ToString())
            : Language.main.Get("Vehicle_Chilling_State").Replace("{vehicle}", vehicle)
                .Replace("{depth}", Player.main.cachedDepth.ToString());
    }

    private static string BiomeDetails(string biomeString)
    {
        return Language.main.Get("Biome_Details").Replace("{biome}", biomeString);
    }

    public void Shutdown()
    {
        Plugin.Logger.LogInfo("Disposing Discord Connection");
        _client?.Dispose();
        _client = null;
    }
}