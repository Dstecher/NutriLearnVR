using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller class for the Scene Manager. Allows resetting and changing current scene.
/// </summary>
public class SceneManagerController : MonoBehaviour
{
    public void ResetScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // load current scene again for resetting
    }

    // Currently, only two scenes are implemented, so that scene change is easy. More advanced version needs dialogue for scene selection
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

    /// <summary>
    /// Method for loading Market Scene (Full Scene) specifically
    /// </summary>
    void LoadMarketScene()
    {
        SceneManager.LoadScene("MarketScene");
    }

    /// <summary>
    /// Method for loading Market Scene Reduced (Reduced Scene) specifically
    /// </summary>
    void LoadMarketSceneReduced()
    {
        SceneManager.LoadScene("MarketSceneReduced");
    }
}
