using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Text.RegularExpressions;

/// <summary>
/// Helps to automatically add a run script for the Xcode build phases
/// which strips the simulator architecture codes when archiving 
/// </summary>
public class SuperAwesomeBuildProcessor
{
    [PostProcessBuild]
    public static void Process(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS)
        {
            AddRunScript(path);
        }
    }

    /// <summary>
    /// Adds a run script to the Xcode project 
    /// </summary>
    /// <param name="path">Path of the Xcode project</param>
    private static void AddRunScript(string path)
    {
        var projectPath = PBXProject.GetPBXProjectPath(path);
        var project = new PBXProject();
        project.ReadFromFile(projectPath);

        var mainTarget = project.GetUnityMainTargetGuid();
        var buildPhaseName = "SuperAwesome Strip Frameworks";

        // Only add when the script has not added yet
        if (FindBuildPhaseByName(project, mainTarget, buildPhaseName) == null)
        {
            Debug.Log(buildPhaseName + " is added now");
            var fileName = "strip-frameworks.sh";
            var filePath = "Assets/SuperAwesome/Editor/" + fileName;
            var shellScript = "./" + fileName;

            // Add new phase to the Xcode project
            project.AddShellScriptBuildPhase(mainTarget, buildPhaseName, "/bin/sh", shellScript);

            // Copy the script file to the project
            File.Copy(filePath, Path.Combine(path, fileName), true);
            var fileGuid = project.AddFile(fileName, fileName);
            project.AddFileToBuild(mainTarget, fileGuid);

            // Update Project
            project.WriteToFile(PBXProject.GetPBXProjectPath(path));

            EnableRunOnlyWhenInstalling(path, buildPhaseName);
        }
        else
        {
            // Phase is already there
            Debug.Log(buildPhaseName + " already added");
        }
    }

    private static string FindBuildPhaseByName(PBXProject project, string target, string name)
    {
        var phases = project.GetAllBuildPhasesForTarget(target);
        foreach (string phase in phases)
        {
            string phaseName = project.GetBuildPhaseName(phase);
            if (phaseName == name)
            {
                return phase;
            }
        }
        return null;
    }

    /// <summary>
    /// Enables `Run script only when installing` option for a build phase
    /// </summary>
    /// <param name="path">Path for the Xcode project</param>
    /// <param name="buildPhaseName">The name of the build phase to be updated</param>
    private static void EnableRunOnlyWhenInstalling(string path, string buildPhaseName)
    {
        var pbxProjContents = File.ReadAllText(PBXProject.GetPBXProjectPath(path));

        var pattern = GetPattern(buildPhaseName, "0");
        var regex = new Regex(pattern);
        var match = regex.Match(pbxProjContents);

        if (match.Success)
        {
            pbxProjContents = regex.Replace(pbxProjContents, GetPattern(buildPhaseName, "1"));
            File.WriteAllText(PBXProject.GetPBXProjectPath(path), pbxProjContents);
            Debug.Log(buildPhaseName + " is updated to enable run only");
        }
        else
        {
            Debug.Log(buildPhaseName + " could not be enabled");
        }
    }

    private static string GetPattern(string buildPhaseName, string value)
    {
        return string.Format("name = \"{0}\";\n\t\t\trunOnlyForDeploymentPostprocessing = {1};", buildPhaseName, value);
    }
}
