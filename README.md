# OBDII.DTC

[![.github/workflows/build.yml](https://github.com/lennykean/OBDII.DTC/actions/workflows/build.yml/badge.svg)](https://github.com/lennykean/OBDII.DTC/actions/workflows/build.yml)
[![.github/workflows/publish.yml](https://github.com/lennykean/OBDII.DTC/actions/workflows/publish.yml/badge.svg)](https://github.com/lennykean/OBDII.DTC/actions/workflows/publish.yml)
[![UnitsNet.Metadata](https://img.shields.io/nuget/v/OBDII.DTC.svg)](https://www.nuget.org/packages/OBDII.DTC/)

The OBDII.DTC is a .NET standard library that provides a collection of generic (non-manufacturer specific) categorized On-board Diagnostics II (OBDII) Diagnostic Trouble Codes (DTCs) along with code descriptions. DTCs are categorised as Powertrain (P codes), Chassis (C codes), Body (B codes), or Network Communication (U codes). Each code has a hex value based on its category as defined in ISO[^1] and SAE[^2] standards, Powertrain (0x0000 - 0x3FFF), Chassis (0x4000 - 0x7FFF), Body (0x8000 - 0xBFFF), Network Communication (0xC000 - 0xFFFF). 

[^1]: [ISO 15031](https://www.iso.org/standard/64636.html)
[^2]: [SAE J2012](https://www.sae.org/standards/content/j2012_201612/)

## Features
- Enumerated DTCs with thousands of codes.
- Extension methods `DTC.GetDescription()` and `DTC.GetCategory()` to fetch the description and the category of a particular DTC.

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

## Contributions
If any codes are incorrect or missing, contributions are greatly appreciated to help improve this library.

## License
OBDII.DTC is released under the [MIT License](LICENSE).

## Support 
For any questions or issues, please create an issue on the [GitHub](http://github.com/lennykean/OBDII.DTC) page.
