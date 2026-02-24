# FBPRO-DLL-DEMO

**FBPRO — Fast Projection Without Trigonometry!**

This repository provides a **demo** of the FBPRO library: fast geometric projection and distance helpers that use **no trigonometric functions** (no `atan2`, `sin`, or `cos`). Typical speedup versus trig-based methods is in the **2–5x** range, depending on workload.

## Contents

- **`bin/`** — Pre-built FBPRO demo DLL and API documentation (XML). The demo is **time-limited to 30 days** from the DLL build date; after that, a full license is required.
- **`BENCHMARK/`** — Benchmark app: **prebuilt executable** (run without building) or **source code** to build and run yourself. Compares FBPRO vs a trig-based implementation and generates a performance report.

## Quick start

1. **Use the DLL**  
   Add a reference to `bin/FBPRO.dll` in your .NET project (e.g. .NET Framework, .NET Core, .NET 5+). Optionally reference `bin/FBPRO.xml` for IntelliSense.  
   See **[GETTING_STARTED.md](GETTING_STARTED.md)** for step-by-step integration and API usage.

2. **Run the benchmark** — choose one:

   **Option A — Prebuilt exe (no build required)**  
   Go to `BENCHMARK/bin/Release/net6.0/` and run:
   ```bash
   BENCHMARK.exe
   ```
   With custom iterations: `BENCHMARK.exe -i 10000000` or `BENCHMARK.exe --iterations 10000000`.  
   Requires [.NET 6.0 Runtime](https://dotnet.microsoft.com/download/dotnet/6.0) (or later) installed.

   **Option B — Build from source and run**  
   From the repo root:
   ```bash
   dotnet run --project BENCHMARK
   ```
   Optional: `dotnet run --project BENCHMARK -- -i 10000000`.  
   Requires [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (or later).

   In both cases the tool prints a summary and writes **FBPRO_Benchmark_Report.html** in the current directory. See **[BENCHMARK/README.md](BENCHMARK/README.md)** for details.

## Demo limitation

The DLL in this repository is a **30-day evaluation** build. After the trial period, calls will throw. For production use or extended evaluation, you need a **full commercial license**.

## License and contact

This software is **proprietary**. All rights reserved. Use, redistribution, or commercial use is allowed only under a written license agreement.

- **Full license and pricing:** contact **ryzl@hotmail.com** (price on request).
- **Legal terms:** see **[EULA.md](EULA.md)** and **[LICENSE](LICENSE)**.

---

FBPRO - Fast Projection Without Trigonometry!
