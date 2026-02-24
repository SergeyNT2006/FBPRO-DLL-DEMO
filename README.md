# FBPRO-DLL-DEMO

**FBPRO — Fast Projection Without Trigonometry!**

This repository provides a **demo** of the FBPRO library: fast geometric projection and distance helpers that use **no trigonometric functions** (no `atan2`, `sin`, or `cos`). Typical speedup versus trig-based methods is in the **2–5x** range, depending on workload.

## Contents

- **`bin/`** — Pre-built FBPRO demo DLL and API documentation (XML). The demo is **time-limited to 30 days** from the DLL build date; after that, a full license is required.
- **`BENCHMARK/`** — Console app and source code to compare FBPRO vs a trig-based implementation and to measure performance. You can build and run it to verify speedup on your machine.

## Quick start

1. **Use the DLL**  
   Add a reference to `bin/FBPRO.dll` in your .NET project (e.g. .NET Framework, .NET Core, .NET 5+). Optionally reference `bin/FBPRO.xml` for IntelliSense.  
   See **[GETTING_STARTED.md](GETTING_STARTED.md)** for step-by-step integration and API usage.

2. **Run the benchmark**  
   From the repo root:
   ```bash
   dotnet run --project BENCHMARK
   ```
   Optional: set iteration count, e.g. `dotnet run --project BENCHMARK -- -i 10000000`.  
   The tool prints a short summary and writes `FBPRO_Benchmark_Report.html` in the current directory.

## Demo limitation

The DLL in this repository is a **30-day evaluation** build. After the trial period, calls will throw. For production use or extended evaluation, you need a **full commercial license**.

## License and contact

This software is **proprietary**. All rights reserved. Use, redistribution, or commercial use is allowed only under a written license agreement.

- **Full license and pricing:** contact **ryzl@hotmail.com** (price on request).
- **Legal terms:** see **[EULA.md](EULA.md)** and **[LICENSE](LICENSE)**.

---

FBPRO - Fast Projection Without Trigonometry!
