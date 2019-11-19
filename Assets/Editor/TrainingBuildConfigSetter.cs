using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Constants;
using UnityEditor;

/// <summary>
///     Adds the given define symbols to PlayerSettings define symbols.
///     Just add your own define symbols to the Symbols property at the below.
/// </summary>
[InitializeOnLoad]
// ReSharper disable once CheckNamespace
public static class TrainingBuildConfigSetter
{
    private const string MenuName = "Mode/Training";

    private static bool IsEnabled
    {
        get => EditorPrefs.GetBool(MenuName, true);
        set => EditorPrefs.SetBool(MenuName, value);
    }

    private static readonly string[] TrainingSymbols =
    {
        GameConfig.TrainingMode
    };

    private static readonly string[] GameplaySymbols =
    {
        GameConfig.GameplayMode
    };

    [MenuItem(MenuName)]
    private static void ToggleAction()
    {
        if (IsEnabled)
        {
            OnToggleOff();
        }
        else
        {
            OnToggleOn();
        }

        IsEnabled = !IsEnabled;
        Menu.SetChecked(MenuName, IsEnabled);
    }

    [MenuItem(MenuName, true)]
    private static bool ValidateToggleAction()
    {
        Menu.SetChecked(MenuName, IsEnabled);
        return true;
    }

    private static void OnToggleOn()
    {
        var allDefines = GetDefinedSymbols();
        allDefines = allDefines.Union(TrainingSymbols).Except(GameplaySymbols);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(
            EditorUserBuildSettings.selectedBuildTargetGroup,
            string.Join(";", allDefines.ToArray())
        );
    }

    private static void OnToggleOff()
    {
        var allDefines = GetDefinedSymbols();
        allDefines = allDefines.Except(TrainingSymbols).Union(GameplaySymbols);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(
            EditorUserBuildSettings.selectedBuildTargetGroup,
            string.Join(";", allDefines.ToArray())
        );
    }

    private static IEnumerable<string> GetDefinedSymbols()
    {
        var definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        return definesString.Split(';').AsEnumerable();
    }
}