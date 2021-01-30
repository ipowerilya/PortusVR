using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] SceneNames;
    public int index = 0;
    Nullable<Scene> prevScene;
    public void SwitchScene()
    {
        StartCoroutine(AsyncSwitchScene());
    }

    IEnumerator AsyncSwitchScene()
    {
        if (prevScene.HasValue)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(prevScene.Value);
            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }
        var currSceneName = SceneNames[index];
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(currSceneName, LoadSceneMode.Additive); ;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        prevScene = SceneManager.GetSceneByName(currSceneName);
        index = (index + 1) % SceneNames.Length;
    }
}
