# Getting started with FBPRO

This guide explains how to add the FBPRO demo DLL to your project and call its API. The library performs projection and distance calculations **without using trigonometry** (no `atan2`, `sin`, or `cos`).

## Requirements

- .NET Framework 4.6.1+ or .NET Core / .NET 5+ (the DLL targets .NET Standard 2.0).
- No extra NuGet packages are required.

## Adding the DLL to your project

1. Copy `bin/FBPRO.dll` (and optionally `bin/FBPRO.xml` for IntelliSense) into your solution, e.g. into a `lib` or `bin` folder next to your project.

2. **SDK-style project** (e.g. `.csproj` with `<Project Sdk="Microsoft.NET.Sdk">`):

   ```xml
   <ItemGroup>
     <Reference Include="FBPRO">
       <HintPath>path\to\bin\FBPRO.dll</HintPath>
     </Reference>
   </ItemGroup>
   ```

   Adjust `HintPath` to the actual path to `FBPRO.dll`.

3. **Non-SDK project**: In Visual Studio, right-click the project → Add → Reference → Browse, and select `FBPRO.dll`.

4. If you use `FBPRO.xml`, place it next to the DLL with the same name; Visual Studio will use it for tooltips.

## Namespace and main class

- **Namespace:** `FBPRO`
- **Class:** `ProjectionHelper` (static)

All methods are static. Call them as `FBPRO.ProjectionHelper.MethodName(...)`.

## Main methods

| Method | Description |
|--------|-------------|
| `HeightToSegment` | Perpendicular distance from a point to the line defined by a segment (point coordinates and segment endpoints). |
| `ProjectionParam` | Parameter `t` along the segment (0 = start, 1 = end). Use to check if the foot lies on the segment. |
| `ProjectionPoint` | Foot of the perpendicular (projection point) on the line; outputs x, y. |
| `ProjectionArbitrary` | Scalar projection of one side onto another in an arbitrary triangle (three side lengths). |
| `ProjectionIsosceles` | Same idea for the isosceles triangle case (base and lateral side lengths). |

Signatures and parameter meanings are available in the XML docs and in your IDE after referencing the DLL (and XML).

## Example (height from point to segment)

```csharp
using FBPRO;

double h = ProjectionHelper.HeightToSegment(
    ptX: 30, ptY: 80,
    lineStartX: 0, lineStartY: 0,
    lineEndX: 100, lineEndY: 50);
// h is the perpendicular distance from (30, 80) to the line through (0,0)-(100,50).
```

## Demo time limit

The DLL in this repository is a **30-day demo** from its build date. After the trial period, methods will throw. For uninterrupted use, contact **ryzl@hotmail.com** for a full license (price on request).

## License and terms

Use is subject to the **EULA** and **LICENSE** in this repository. Commercial use requires a separate license. See [EULA.md](EULA.md) and [LICENSE](LICENSE).

---

FBPRO - Fast Projection Without Trigonometry!
