using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void ChangeScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MarketScene")
        {
            LoadMarketSceneReduced();
        }
        else
        {
            LoadMarketScene();
        }
    }

    void LoadMarketScene()
    {
        SceneManager.LoadScene("MarketScene");
    }

    void LoadMarketSceneReduced()
    {
        SceneManager.LoadScene("MarketSceneReduced");
    }
}
