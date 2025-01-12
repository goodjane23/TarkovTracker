﻿namespace TarkovTracker.Models;

public class ItemRequirements
{
    public string Id { get; set; }
    public Item Item { get; set; }
    public int Count { get; set; }
    public int Quantity { get; set; }

    public override bool Equals(object? obj)
    {
        return Id.Equals(Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
