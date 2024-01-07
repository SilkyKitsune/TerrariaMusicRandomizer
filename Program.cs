using System;
using C = System.Console;
using E = System.Environment;
using P = System.IO.Path;
using D = System.IO.Directory;
using F = System.IO.File;
using System.Collections.Generic;

namespace TerrariaMusicRandomizer;

public static class Program
{
    private const int TotalSongs = 91;

    private const string PathsFile = "Paths.txt";

    private static void Main()
    {
        if (!F.Exists(PathsFile))
        {
            C.WriteLine($"{PathsFile} could not be found! Creating file...\nUse this file to specify files/folders you wish to have copied from.");
            F.Create(PathsFile);
            return;
        }

        if (ValidatePaths(out string[] paths,
            F.ReadAllText(PathsFile).Split(E.NewLine, StringSplitOptions.RemoveEmptyEntries)))
        {
            C.WriteLine("Enter seed (leave blank for auto)");
            bool useCustomSeed = int.TryParse(C.ReadLine(), out int seed);

            if (!useCustomSeed)
            {
                DateTime now = DateTime.Now;
                seed = ((now.Year * 10000) + (now.Month * 100) + now.Day) ^ E.TickCount;
            }

            string packName = "TMR_" + seed,
                outputPath = @"Output\" + packName,
                jsonPath = outputPath + @"\pack.json",
                spoilerPath = outputPath + @"\" + packName + "_Spoiler.txt";
            D.CreateDirectory(outputPath);

            outputPath += @"\Content\Music";
            D.CreateDirectory(outputPath);
            outputPath += @"\Music_";

            string spoiler = $"--- TMR Spoiler Log ---\nSeed: {seed}\n-\n";

            Random r = new(seed);
            List<string> p = new(paths);
            for (int i = 0; i < TotalSongs && p.Count > 0; i++)
            {
                int n = r.Next(p.Count);
                string filePath = p[n], outPath = outputPath + (i + 1) + P.GetExtension(filePath);
                spoiler += $"{(MusicID)(i + 1)} => {P.GetFileName(filePath)}\n";
                F.Copy(filePath, outPath);
                p.RemoveAt(n);
            }
            F.WriteAllText(jsonPath, CreateJSON(packName, spoiler));
            F.WriteAllText(spoilerPath, spoiler);
            C.WriteLine(spoiler);
        }
        else C.WriteLine("No valid paths found!");
    }

    private static bool ValidatePaths(out string[] validatedPaths, params string[] paths)
    {
        validatedPaths = null;

        if (paths == null || paths.Length == 0) return false;
        
        List<string> p = new();
        foreach (string path in paths)
            if (F.Exists(path) && IsAudioFile(path)) p.Add(path);
            else if (D.Exists(path) && GetAllFiles(out string[] filePaths, path)) p.AddRange(filePaths);

        if (p.Count == 0) return false;

        validatedPaths = p.ToArray();
        return true;
    }

    private static bool GetAllFiles(out string[] filePaths, string path)
    {
        filePaths = null;

        if (!D.Exists(path)) return false;

        List<string> p = new();

        foreach (string file in D.GetFiles(path))
            if (IsAudioFile(file)) p.Add(file);

        foreach (string subDir in D.GetDirectories(path))
            if (GetAllFiles(out string[] filePaths1, subDir))
                foreach (string file in filePaths1)
                    if (IsAudioFile(file)) p.Add(file);

        if (p.Count == 0) return false;

        filePaths = p.ToArray();
        return true;
    }

    private static bool IsAudioFile(string path)
    {
        string ext = P.GetExtension(path);
        return F.Exists(path) && (ext.Equals(".wav") || ext.Equals(".mp3") || ext.Equals(".ogg"));
    }

    private static string CreateJSON(string packName, string spoiler) =>
        "{\r\n" +
        "    \"Name\": \"" + packName + "\",\r\n" +
        "    \"Author\": \"SilkyKitsune\",\r\n" +
        "    \"Description\": \"" + spoiler + "\",\r\n" +
        "    \"Version\": {\r\n" +
        "        \"major\": 1,\r\n" +
        "        \"minor\": 0\r\n" +
        "    }\r\n" +
        "}";
}