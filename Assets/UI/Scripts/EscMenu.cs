using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public void ClickResume()
    {
        Global.GetPlayer().GetHud().CloseEscMenu();
    }

    public void ClickRestart()
    {
        Global.GetPlayer().UnFreeze();
        GameSavingManager.instance.LoadToGame();
        Util.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClickQuit()
    {
        Global.GetPlayer().UnFreeze();
        GameSavingManager.instance.SaveGameData();
        Util.LoadScene("start");
    }
}
