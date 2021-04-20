using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool disable = false;
    private string loadedScene = null;
    public Transform playerTransform;

    public void LoadScene(string sceneName)
    {
        if (!disable && loadedScene == null)
        {
            StartCoroutine(AsyncLoadScene(sceneName));
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (disable && loadedScene != null && playerTransform.position.z < 0)
        {
            StartCoroutine(AsyncUnloadScene(sceneName));
        }
    }

    public void LoadTest()
    {
        LoadScene("7_No_accel_speed");
    }

    public void UnloadTest()
    {
        UnloadScene("7_No_accel_speed");
    }

    IEnumerator AsyncLoadScene(string sceneName)
    {
        AsyncOperation task = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!task.isDone)
        {
            yield return null;
        }
        loadedScene = sceneName;
    }

    IEnumerator AsyncUnloadScene(string sceneName)
    {
        AsyncOperation task = SceneManager.UnloadSceneAsync(sceneName);
        while (!task.isDone)
        {
            yield return null;
        }
        loadedScene = null;
    }
}
