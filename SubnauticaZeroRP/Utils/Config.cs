using Nautilus.Json;
using Nautilus.Options.Attributes;

namespace SubnauticaZeroRP.Utils;

[Menu("SZRP (Subnautica Zero Rich Presence)")]
public class Config : ConfigFile
{
    public string AppId = "1509748105074184212";

    [Toggle(LabelLanguageId = "EnableHoverText_Setting", Order = 2,
        TooltipLanguageId = "EnableHoverText_Setting_Tooltip")]
    public bool EnableHoverText = true;

    [Toggle(LabelLanguageId = "EnableSZRP_Setting", Order = 1, TooltipLanguageId = "EnableSZRP_Setting_Tooltip")]
    public bool EnableSZRP = true;

    [Slider(
        LabelLanguageId = "RPCUpdateInterval_Setting",
        Min = 1f,
        Max = 60f,
        DefaultValue = 15f,
        Format = "{0:F0}",
        Step = 1f,
        Order = 3,
        TooltipLanguageId = "RPCUpdateInterval_Setting_Tooltip"
    )]
    public float RPCUpdateInterval = 15f;
}