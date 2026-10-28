using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace OBDII.DTC;

public static class DTCExtensions
{
    /// <summary>
    /// Gets the description of a DTC
    /// </summary>
    public static string GetDescription(this DTC dtc)
    {
        var type = typeof(DTC);
        var name = Enum.GetName(type, dtc);
        var field = type.GetField(name);
        var attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute.Description;
    }

    /// <summary>
    /// Gets the description of a DTC for a manufacturer
    /// </summary>
    public static string GetDescription(this DTC dtc, string manufacturer)
    {
        var type = typeof(DTC);
        var name = Enum.GetName(type, dtc);
        var field = type.GetField(name);
        var attribute = field.GetCustomAttributes<ManufacturerAttribute>()
            .FirstOrDefault(attribute => string.Equals(attribute.Manufacturer, manufacturer, StringComparison.OrdinalIgnoreCase));

        return attribute?.Description ?? dtc.GetDescription();
    }

    /// <summary>
    /// Gets the category of a DTC
    /// </summary>
    public static string GetCategory(this DTC dtc)
    {
        var type = typeof(DTC);
        var name = Enum.GetName(type, dtc);
        var field = type.GetField(name);
        var attribute = field.GetCustomAttribute<CategoryAttribute>();

        return attribute.Category;
    }

    /// <summary>
    /// Gets whether a DTC is in the ranges designated for manufacturers.
    /// </summary>
    public static bool IsManufacturerCode(this DTC dtc)
    {
        var value = (ushort)dtc;
        var category = value >> 14;
        var firstDigit = (value >> 12) & 0x3;
        var secondDigit = (value >> 8) & 0xF;

        if (category == 0)
        {
            return firstDigit == 1 || (firstDigit == 3 && secondDigit <= 3);
        }

        return firstDigit == 1 || firstDigit == 2;
    }

    /// <summary>
    /// Gets whether a DTC is designated for a specific manufacturer.
    /// </summary>
    public static bool IsManufacturerCode(this DTC dtc, string manufacturer)
    {
        if (!dtc.IsManufacturerCode())
        {
            return false;
        }

        var type = typeof(DTC);
        var name = Enum.GetName(type, dtc);
        var field = name is null ? null : type.GetField(name);

        return field?.GetCustomAttributes<ManufacturerAttribute>()
            .Any(attribute => string.Equals(attribute.Manufacturer, manufacturer, StringComparison.OrdinalIgnoreCase)) ?? false;
    }
}
