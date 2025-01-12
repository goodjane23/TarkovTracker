using System.Reflection.Metadata.Ecma335;

namespace TarkovTracker.Models;

public class HideoutStation
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public List<HideoutStationLevel> Levels { get; set; }
}
