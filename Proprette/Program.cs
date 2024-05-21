// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using Proprette.DataLayer.Context;
using Proprette.DataLayer.Entity.Category;
using System.Reflection;
using System.Runtime.InteropServices;

//var interfaceCategoryType = typeof(ICategory);
//var entityAssembly = interfaceCategoryType.Assembly;
//var allCategoryTypes = interfaceCategoryType.Assembly.GetTypes()
//                .Where(type => interfaceCategoryType.IsAssignableFrom(type) && type.IsClass && !type.IsAbstract);
//var tt = typeof(Brand).Assembly;
//foreach(var type in allCategoryTypes)
//{
//    Console.WriteLine(type.FullName);
//}

using var context = new PropretteDbContext();
context.Database.EnsureCreated();



Console.WriteLine("Hello, World!");
