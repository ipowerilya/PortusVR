using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ResetScene : MonoBehaviour
{
    public GameObject[] preservedObjects;
    // Start is called before the first frame update

    public void ResetSceneFunc()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    /* public void ResetSceneFunc()
    {
        foreach (var obj in preservedObjects) 
        {
            DontDestroyOnLoad(obj);
        }
        StartCoroutine(AsyncLoadScene());
    }*/

    /*    Nye Rabotayet
        IEnumerator AsyncLoadScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            var currentScene = SceneManager.GetActiveScene();
            foreach (var obj in preservedObjects)
            {
                SceneManager.MoveGameObjectToScene(obj, currentScene);
            }
            SceneManager.UnloadSceneAsync(currentScene);
        }*/
}
