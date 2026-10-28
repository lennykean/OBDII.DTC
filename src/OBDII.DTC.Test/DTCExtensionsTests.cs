using System.Reflection;

using NUnit.Framework;

namespace OBDII.DTC.Test;

[TestFixture]
public sealed class DTCExtensionsTests
{
    [TestCase(DTC.Unknown, "Unknown")]
    [TestCase(DTC.P0100, "Mass or Volume Air Flow Sensor A Circuit")]
    [TestCase(DTC.P134B, "Crankshaft Signal Diagnose")]
    public void GetDescription_PreservesStandardDescriptionAttribute(DTC dtc, string expected)
    {
        var field = typeof(DTC).GetField(dtc.ToString())!;
        var attribute = field.GetCustomAttribute<System.ComponentModel.DescriptionAttribute>();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(attribute, Is.Not.Null);
            Assert.That(attribute?.Description, Is.EqualTo(expected));
            Assert.That(dtc.GetDescription(), Is.EqualTo(expected));
        }
    }

    [TestCase(DTC.P134B, "Honda", "Crankshaft Signal Diagnose")]
    [TestCase(DTC.P15C7, "hOnDa", "Brake Booster Pressure Sensor A Characteristic High Voltage")]
    [TestCase(DTC.P1AF1, "HONDA", "Input Power A Voltage Correlation Abnormality")]
    public void GetDescription_FindsManufacturerDescriptionIgnoringCase(DTC dtc, string manufacturer, string expected)
    {
        Assert.That(dtc.GetDescription(manufacturer), Is.EqualTo(expected));
    }

    [TestCase(DTC.P0100, "Honda")]
    [TestCase(DTC.P134B, "Other")]
    [TestCase(DTC.P134B, "")]
    public void GetDescription_FallsBackToGeneralDescription(DTC dtc, string manufacturer)
    {
        Assert.That(dtc.GetDescription(manufacturer), Is.EqualTo(dtc.GetDescription()));
    }

    [TestCase(DTC.Unknown, Categories.Unknown)]
    [TestCase(DTC.P0100, Categories.Powertrain)]
    [TestCase(DTC.C0001, Categories.Chassis)]
    [TestCase(DTC.B0001, Categories.Body)]
    [TestCase(DTC.U0001, Categories.NetworkCommunication)]
    public void GetCategory_ReturnsCodeCategory(DTC dtc, string expected)
    {
        Assert.That(dtc.GetCategory(), Is.EqualTo(expected));
    }

    [TestCase(0x0FFF, false)]
    [TestCase(0x1000, true)]
    [TestCase(0x1FFF, true)]
    [TestCase(0x2000, false)]
    [TestCase(0x2FFF, false)]
    [TestCase(0x3000, true)]
    [TestCase(0x33FF, true)]
    [TestCase(0x3400, false)]
    [TestCase(0x4FFF, false)]
    [TestCase(0x5000, true)]
    [TestCase(0x6FFF, true)]
    [TestCase(0x7000, false)]
    [TestCase(0x8FFF, false)]
    [TestCase(0x9000, true)]
    [TestCase(0xAFFF, true)]
    [TestCase(0xB000, false)]
    [TestCase(0xCFFF, false)]
    [TestCase(0xD000, true)]
    [TestCase(0xEFFF, true)]
    [TestCase(0xF000, false)]
    public void IsManufacturerCode_ChecksRangeBoundaries(int value, bool expected)
    {
        Assert.That(((DTC)value).IsManufacturerCode(), Is.EqualTo(expected));
    }

    [TestCase(DTC.P134B, "Honda", true)]
    [TestCase(DTC.P15C7, "hOnDa", true)]
    [TestCase(DTC.P1AF1, "HONDA", true)]
    [TestCase(DTC.P134B, "Other", false)]
    [TestCase(DTC.P134B, "", false)]
    [TestCase(DTC.P1349, "Honda", false)]
    [TestCase(DTC.P0100, "Honda", false)]
    [TestCase(DTC.Unknown, "Honda", false)]
    [TestCase((DTC)0x1FFF, "Honda", false)]
    public void IsManufacturerCode_RequiresMatchingManufacturerMetadata(DTC dtc, string manufacturer, bool expected)
    {
        Assert.That(dtc.IsManufacturerCode(manufacturer), Is.EqualTo(expected));
    }
}
