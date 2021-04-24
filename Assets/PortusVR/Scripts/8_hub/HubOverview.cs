using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubOverview : MonoBehaviour
{
    public List<Lab> labs;
    public LabOverviewUI labOverviewUI;
    public LabMenuUI labMenuUI;
    public bool disableSceneLoading = false;
    public Material DefaultSkybox;

    public Transform playerTransform; // for scene loading


    int currentLabIndex = 0;
    string loadedScene = null;


    private void Start()
    {
        foreach (var lab in labs)
        {
            lab.InitTasks();
        }
        labMenuUI.SetLabs(labs);
        labMenuUI.LabCallback = (int labIndex) => { SetCurrentLabIndex(labIndex); };
        labMenuUI.UpdateUI(currentLabIndex);
        UpdateUI();
        RenderSettings.skybox = DefaultSkybox;
    }

    public Lab GetCurrentLab()
    {
        return labs[currentLabIndex];
    }

    public void SetCurrentLabIndex(int index)
    {
        currentLabIndex = index;
        UpdateUI();
    }

    public int GetCurrentLabIndex()
    {
        return currentLabIndex;
    }

    public void UpdateUI()
    {
        labOverviewUI.SetLab(GetCurrentLab());
        labMenuUI.UpdateUI(currentLabIndex);
    }

    // TODO set scene to lab
    public void LoadScene()
    {
        if (!disableSceneLoading && loadedScene == null)
        {
            StartCoroutine(AsyncLoadScene());
        }
    }

    public void UnloadScene()
    {
        // TODO change player position check  to collider check
        if (!disableSceneLoading && loadedScene != null && playerTransform.position.z < 0) 
        {
            StartCoroutine(AsyncUnloadScene());
        }
    }

    IEnumerator AsyncLoadScene()
    {
        var sceneName = GetCurrentLab().internalName;
        AsyncOperation task = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!task.isDone)
        {
            yield return null;
        }
        loadedScene = sceneName;
        RenderSettings.skybox = GetCurrentLab().associatedSkybox;
        GameObject.FindGameObjectsWithTag("TaskManager")[0].GetComponent<LabTasksManager>().SetLab(GetCurrentLab());
    }

    // TODO delete elements from lab (gaged objects)
    IEnumerator AsyncUnloadScene()
    {
        AsyncOperation task = SceneManager.UnloadSceneAsync(loadedScene);
        while (!task.isDone)
        {
            yield return null;
        }
        loadedScene = null;
        RenderSettings.skybox = DefaultSkybox;
    }

}
