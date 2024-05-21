﻿namespace Proprette.DataLayer.Entity.Category;

public class Color(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;

    private Color() : this(null!)
    { }
}