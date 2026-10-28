using System;

namespace OBDII.DTC;

/// <summary>
/// The description manufacturer specific DTC
/// </summary>
[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public sealed class ManufacturerAttribute : Attribute
{
    public ManufacturerAttribute(string manufacturer, string description)
    {
        Manufacturer = manufacturer;
        Description = description;
    }

    /// <summary>
    /// The manufacturer
    /// </summary>
    public string Manufacturer { get; }

    /// <summary>
    /// The manufacturer specific description
    /// </summary>
    public string Description { get; }
}
