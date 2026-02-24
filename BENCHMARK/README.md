# FBPRO Benchmark Suite

This project compares FBPRO (projection without trigonometry) with a traditional trig-based implementation and generates a performance and accuracy report.

You can run the benchmark in two ways: **use the prebuilt exe** (no build) or **build from source**.

---

## Option 1: Run the prebuilt exe (no build)

A ready-to-run executable is provided in:

```
BENCHMARK/bin/Release/net6.0/
```

**Steps:**

1. Open a terminal and go to that folder:
   ```bash
   cd BENCHMARK/bin/Release/net6.0
   ```
   (Or navigate there in Explorer and run from that folder.)

2. Run:
   ```bash
   BENCHMARK.exe
   ```

3. Optional — set iteration count:
   ```bash
   BENCHMARK.exe -i 10000000
   BENCHMARK.exe --iterations 20000000
   ```

**Requirements:** [.NET 6.0 Runtime](https://dotnet.microsoft.com/download/dotnet/6.0) (or later) must be installed. You do **not** need the SDK.

The report **FBPRO_Benchmark_Report.html** is created in the **current directory** (the folder where you ran the exe).

---

## Option 2: Build from source and run

**Steps:**

1. Ensure the FBPRO demo DLL is in the repo root `bin/` folder (`bin/FBPRO.dll`). The project references `../bin/FBPRO.dll`.

2. From the **repository root**:
   ```bash
   dotnet run --project BENCHMARK
   ```

   Or from this folder:
   ```bash
   cd BENCHMARK
   dotnet run
   ```

3. Optional — iteration count:
   ```bash
   dotnet run --project BENCHMARK -- -i 10000000
   dotnet run --project BENCHMARK -- --iterations 20000000
   ```

**Requirements:** [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) (or later).

Default iterations: 50,000,000.

---

## Output

- **Console** — Short summary (accuracy, accumulation, performance, speedup).
- **FBPRO_Benchmark_Report.html** — Written in the current directory; open in a browser for the full report.
