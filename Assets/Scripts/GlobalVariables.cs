using System.Collections;
using System.Collections.Generic;
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
}
