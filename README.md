# OBDII.DTC

[![CI](https://github.com/lennykean/OBDII.DTC/actions/workflows/ci.yml/badge.svg)](https://github.com/lennykean/OBDII.DTC/actions/workflows/ci.yml)
[![OBDII.DTC](https://img.shields.io/nuget/v/OBDII.DTC.svg)](https://www.nuget.org/packages/OBDII.DTC/)

OBDII.DTC is a .NET Standard library of categorized On-board Diagnostics II (OBDII) Diagnostic Trouble Codes (DTCs), with code descriptions. It covers the generic codes defined in the ISO[^1] and SAE[^2] standards, along with a limited set of manufacturer-specific codes. Additional manufacturer codes are welcome as contributions. Manufacturer-specific codes share number ranges across manufacturers, so their descriptions may only be accurate for some vehicles. DTCs are categorised as Powertrain (P codes), Chassis (C codes), Body (B codes), or Network Communication (U codes). Each code has a hex value based on its category as defined in ISO[^1] and SAE[^2] standards, Powertrain (0x0000 - 0x3FFF), Chassis (0x4000 - 0x7FFF), Body (0x8000 - 0xBFFF), Network Communication (0xC000 - 0xFFFF).

[^1]: [ISO 15031](https://www.iso.org/standard/64636.html)
[^2]: [SAE J2012](https://www.sae.org/standards/content/j2012_201612/)

## Features

- Enumerated DTCs with thousands of codes.
- Extension methods `DTC.GetDescription()` and `DTC.GetCategory()` to fetch the description and the category of a particular DTC.
- Manufacturer-specific descriptions and code checks.

## Installation
Install this package from nuget:

```sh
dotnet add package OBDII.DTC
```

## Usage

After installing the package, the DTC enum can be used to reference the trouble codes. For example:

```csharp
DTC code = DTC.P0100;
Console.WriteLine(code.GetDescription()); // Outputs: "Mass or Volume Air Flow Sensor A Circuit Range/Performance"
Console.WriteLine(code.GetCategory()); // Outputs: "Powertrain"
```

Manufacturer-specific descriptions and code checks:

```csharp
DTC code = DTC.P134B;
Console.WriteLine(code.GetDescription(Manufacturers.Honda)); // Outputs: "Crankshaft Signal Diagnose"
Console.WriteLine(code.IsManufacturerCode()); // Outputs: True
Console.WriteLine(code.IsManufacturerCode(Manufacturers.Honda)); // Outputs: True
```

## Development

The library and NUnit tests are in `src/OBDII.DTC` and `src/OBDII.DTC.Test`. Run the tests with the .NET 10 SDK:

```sh
dotnet test --configuration Release
```

## Contributions
If any codes are incorrect or missing, contributions are greatly appreciated to help improve this library.

## License
OBDII.DTC is released under the [MIT License](LICENSE).

## Support 
For any questions or issues, please create an issue on the [GitHub](http://github.com/lennykean/OBDII.DTC) page.
