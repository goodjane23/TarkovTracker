using System.Text.Json.Serialization;

namespace TarkovTracker.Models;

public class HideoutStationLevel
{
    public string Id { get; set; }

    public int Level { get; set; }
    public List<ItemRequirements> ItemRequirements { get; set; }
}
