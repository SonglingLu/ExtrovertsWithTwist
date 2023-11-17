using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using UnityEditor.Build.Reporting;
using UnityEngine;

public static class GlobalVariables
{
    private static bool showMainMenu = true;

    public static bool GetShowMainMenu() {
        return showMainMenu;
    }

    public static void SetShowMainMenu(bool status) {
        showMainMenu = status;
    }

    private static string[] scenes = {
        "Menu",
        "Level 0",
        "Level 1",
        "Level 2",
        "Level 3",
        "Level 4",
        "Level 5"
    };

    public static bool ExistScene(string sceneName) {
        return scenes.Contains(sceneName);
    }

    private static int highestLevel = 5;

    public static int GetHighestLevel() {
        return highestLevel;
    }

    private static int highestReachedLevel = 5;

    public static void SetHighestReachedLevel(int level) {
        highestReachedLevel = level;
    }

    public static int GetHighestReachedLevel() {
        return highestReachedLevel;
    }

    private static bool playerMovable = true;

    public static bool GetPlayerMovable() {
        return playerMovable;
    }

    public static void SetPlayerMovable(bool movable) {
        playerMovable = movable;
    }

    public static Boolean drawing = false;
    public static bool GetDrawing() {
        return drawing;
    }

    public static void SetDrawing(bool isDrawing) {
        drawing = isDrawing;
    }
}
