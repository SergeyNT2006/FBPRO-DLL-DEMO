# FBPRO Benchmark Suite

This project compares FBPRO (projection without trigonometry) with a traditional trig-based implementation and generates a performance and accuracy report.

## Run

From the repository root:

```bash
dotnet run --project BENCHMARK
```

Or from this folder:

```bash
cd BENCHMARK
dotnet run
```

## Optional: iteration count

```bash
dotnet run --project BENCHMARK -- -i 10000000
dotnet run --project BENCHMARK -- --iterations 20000000
```

Default: 50,000,000 iterations.

## Output

- **Console** — Short summary (accuracy, accumulation, performance, speedup).
- **FBPRO_Benchmark_Report.html** — Written in the current directory when you run the app; open in a browser for the full report.

## Requirements

- .NET 6.0 or later.
- The project references `../bin/FBPRO.dll` (demo DLL). Ensure the DLL is present in `bin/` before building.
