# How to sync this repository with GitHub

After adding the prebuilt BENCHMARK exe or making any other changes, use these steps to push to GitHub.

## 1. Open the repo root

```powershell
cd D:\FBPRO-DLL-DEMO
```

Make sure you are in the **root** of the demo repo (where `README.md`, `EULA.md`, and the `BENCHMARK` folder are), not inside `BENCHMARK`.

## 2. See what will be committed

```powershell
git status
```

You should see:
- Modified: `.gitignore`, `README.md`, `BENCHMARK/README.md`
- New (if not yet added): `BENCHMARK/bin/Release/net6.0/` with `BENCHMARK.exe`, `BENCHMARK.dll`, `FBPRO.dll`, and other runtime files

## 3. Add all changes and commit

```powershell
git add .
git commit -m "Add prebuilt BENCHMARK exe; document run options (exe vs build from source)"
```

If you only want to add specific files:

```powershell
git add .gitignore README.md BENCHMARK/README.md
git add BENCHMARK/bin/Release/net6.0/BENCHMARK.exe
git add BENCHMARK/bin/Release/net6.0/BENCHMARK.dll
git add BENCHMARK/bin/Release/net6.0/BENCHMARK.deps.json
git add BENCHMARK/bin/Release/net6.0/BENCHMARK.runtimeconfig.json
git add BENCHMARK/bin/Release/net6.0/FBPRO.dll
git add BENCHMARK/bin/Release/net6.0/FBPRO.xml
git commit -m "Add prebuilt BENCHMARK exe; document run options"
```

## 4. Push to GitHub

If the remote is already set (you ran `git remote add origin ...` before):

```powershell
git push origin main
```

If this is the first time or you need to set the remote:

```powershell
git remote add origin https://github.com/SergeyNT2006/FBPRO-DLL-DEMO.git
git branch -M main
git push -u origin main
```

## 5. Verify on GitHub

Open: **https://github.com/SergeyNT2006/FBPRO-DLL-DEMO**

Check that:
- Root `README.md` describes both ways to run the benchmark (prebuilt exe and build from source).
- Under `BENCHMARK/bin/Release/net6.0/` there are `BENCHMARK.exe`, `FBPRO.dll`, and the other runtime files.

Users can then either run `BENCHMARK.exe` from that folder or build from source with `dotnet run --project BENCHMARK`.
