using System;
using System.ComponentModel;
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
}
