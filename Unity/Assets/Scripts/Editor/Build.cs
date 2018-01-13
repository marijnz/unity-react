using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Application = UnityEngine.Application;

public class Build : MonoBehaviour
{
	static readonly string ProjectPath = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));

	static readonly string apkPath = ProjectPath + "/Builds/UnityReact.apk";
	static readonly string buildPath = ProjectPath + "/Builds/UnityReact.apk/UnityReact";
	static readonly string exportPath = Path.GetFullPath(Path.Combine(ProjectPath, "..")) + "/React/UnityReactExport";

	[MenuItem("Build/Run Android %g", false, 1)]
	public static void DoBuild()
	{
		if(Directory.Exists(apkPath))
			Directory.Delete(apkPath, true);

		EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;

		var options = BuildOptions.AcceptExternalModificationsToPlayer;
		var status = BuildPipeline.BuildPlayer (
			GetEnabledScenes(),
			Path.Combine(ProjectPath, "Builds/UnityReact.apk"),
			BuildTarget.Android,
			options
		);

		if (!string.IsNullOrEmpty(status))
			throw new Exception("Build failed: " + status);

		Copy(Path.Combine(buildPath, "src"), Path.Combine(exportPath, "src"));
		Copy(Path.Combine(buildPath, "libs"), Path.Combine(exportPath, "libs"));

		BuildGradle();
	}

	static void Copy(string source, string destinationPath)
	{
		if(Directory.Exists(destinationPath))
			Directory.Delete(destinationPath, true);

		Directory.CreateDirectory(destinationPath);

		foreach (string dirPath in Directory.GetDirectories(source, "*",
			SearchOption.AllDirectories))
			Directory.CreateDirectory(dirPath.Replace(source, destinationPath));

		foreach (string newPath in Directory.GetFiles(source, "*.*",
			SearchOption.AllDirectories))
			File.Copy(newPath, newPath.Replace(source, destinationPath), true);
	}

	static void BuildGradle()
	{
		ProcessStartInfo startInfo = new ProcessStartInfo
		{
			FileName = "sh",
			Arguments = "build.sh",
			UseShellExecute = false,
			WorkingDirectory = ProjectPath,
			RedirectStandardError = true,
		};

		var process = new Process { StartInfo = startInfo};
		process.Start();
		process.WaitForExit(300000);
		AssetDatabase.Refresh();

		UnityEngine.Debug.LogFormat("Built dependencies (exit code: {0})", process.ExitCode);
		var errorOutput = process.StandardError.ReadToEnd().Trim();
		if(!string.IsNullOrEmpty(errorOutput))
			UnityEngine.Debug.LogWarningFormat("Warnings and potential errors:\n" + errorOutput);
	}

	static string[] GetEnabledScenes()
	{
		var scenes = EditorBuildSettings.scenes
			.Where(s => s.enabled)
			.Select(s => s.path)
			.ToArray();

		return scenes;
	}
}
