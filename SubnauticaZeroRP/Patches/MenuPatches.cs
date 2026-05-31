using HarmonyLib;

namespace SubnauticaZeroRP.Patches;

[HarmonyPatch(typeof(MainMenuController))]
public static class MenuPatches
{
    [HarmonyPatch(nameof(MainMenuController.Start))]
    [HarmonyPostfix]
    public static void PostfixMenuStart()
    {
        if (Plugin.Discord is not null) Plugin.Discord.UpdatePresence(true);
    }
}