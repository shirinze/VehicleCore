﻿
namespace VehicleCore.DomainModel.Attributes;

[AttributeUsage(AttributeTargets.Field, Inherited =false,  AllowMultiple = true)]
public sealed class InfoAttribute(string name,object value):Attribute
{
    public string Name { get; set; } = name;
    public object Value { get; set; } = value;
}
