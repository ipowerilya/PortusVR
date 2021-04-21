using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool disable = false;
    public Transform playerTransform;
    public string sceneName;

    string loadedScene = null;

    public void SetNextScene(string name)
    {
        sceneName = name;
    }

    public void LoadScene()
    {
        if (!disable && loadedScene == null)
        {
            StartCoroutine(AsyncLoadScene(sceneName));
        }
    }

    public void UnloadScene()
    {
        if (!disable && loadedScene != null && playerTransform.position.z < 0)
        {
            StartCoroutine(AsyncUnloadScene(loadedScene));
        }
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
