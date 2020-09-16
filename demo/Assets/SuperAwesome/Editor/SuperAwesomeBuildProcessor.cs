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

    private static void AddRunScript(string path)
    {
        var projectPath = PBXProject.GetPBXProjectPath(path);
        var project = new PBXProject();
        project.ReadFromFile(projectPath);

        var mainTarget = project.GetUnityMainTargetGuid();
        var scriptName = "SuperAwesome Strip Frameworks";

        // Only add when the script has not added yet
        if (FindBuildPhaseByName(project, mainTarget, scriptName) == null)
        {
            // Add new phase
            Debug.Log(scriptName + " is added now");
            var fileName = "strip-frameworks.sh";
            var filePath = "Assets/SuperAwesome/Editor/" + fileName;
            var shellScript = "./" + fileName;
            project.AddShellScriptBuildPhase(mainTarget, scriptName, "/bin/sh", shellScript);

            // Copy the script file to the project
            File.Copy(filePath, Path.Combine(path, fileName), true);
            var fileGuid = project.AddFile(fileName, fileName);
            project.AddFileToBuild(mainTarget, fileGuid);

            // Update Project
            project.WriteToFile(PBXProject.GetPBXProjectPath(path));

            EnableRunOnlyWhenInstalling(path, scriptName);
        }
        else
        {
            // Phase is already there
            Debug.Log(scriptName + " already added");
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

    private static void EnableRunOnlyWhenInstalling(string path, string scriptName)
    {
        var pbxProjContents = File.ReadAllText(PBXProject.GetPBXProjectPath(path));

        var pattern = GetPattern(scriptName, "0");
        var regex = new Regex(pattern);
        var match = regex.Match(pbxProjContents);

        if (match.Success)
        {
            pbxProjContents = regex.Replace(pbxProjContents, GetPattern(scriptName, "1"));
            File.WriteAllText(PBXProject.GetPBXProjectPath(path), pbxProjContents);
            Debug.Log(scriptName + " is updated to enable run only");
        }
        else
        {
            Debug.Log(scriptName + " could not be enabled");
        }
    }

    private static string GetPattern(string scriptName, string value)
    {
        return string.Format("name = \"{0}\";\n\t\t\trunOnlyForDeploymentPostprocessing = {1};", scriptName, value);
    }
}
