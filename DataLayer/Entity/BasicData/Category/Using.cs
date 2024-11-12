﻿namespace Proprette.DataLayer.Entity.BasicData.Category;

public class Using(string name) : ICategory
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public Using() : this(null!)
    { }
}