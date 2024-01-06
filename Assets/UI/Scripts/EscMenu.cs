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
        Util.LoadScene(SceneManager.GetActiveScene().name);
        GameSavingManager.instance.LoadGame();
    }

    public void ClickQuit()
    {
        Global.GetPlayer().UnFreeze();
        Util.LoadScene("start");
        GameSavingManager.instance.SaveGame();
    }
}
