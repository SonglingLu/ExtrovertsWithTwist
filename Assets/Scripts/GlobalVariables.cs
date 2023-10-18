using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class GlobalVariables
{
    private static bool showMainMenu = true;

    public static bool getShowMainMenu()
    {
        return showMainMenu;
    }

    public static void setShowMainMenu(bool status)
    {
        showMainMenu = status;
    }

    private static string[] scenes = {
        "Menu",
        "Level 0",
        "Level 1",
        "Level 2"
    };

    public static bool existScene(string sceneName) {
        return scenes.Contains(sceneName);
    }

    private static Dictionary<string, bool> levelStatus = new Dictionary<string, bool>(){
        {"Level 0", true},
        {"Level 1", false},
        {"Level 2", false}
    };

    public static bool getLevelStatus(string level) {
        return levelStatus[level];
    }

    public static void unlockLevel(string level) {
        levelStatus[level] = true;
    }
}
