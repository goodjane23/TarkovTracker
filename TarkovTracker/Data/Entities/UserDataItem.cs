namespace TarkovTracker.Data.Entities;

public class UserDataItem
{
    public string Id { get; set; }
    public string NormalizedName { get; set; }
    public string Description { get; set; }
    public string Image512pxLink { get; set; }
    public int RequiredCount { get; set; }
    public int Quantity { get; set; }
    public int СollectedCount { get; set; }
}
