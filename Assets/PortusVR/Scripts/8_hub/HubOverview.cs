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
    bool LabSceneIsLoaded = false;


    private void Start()
    {
        
        foreach (var lab in labs)
        {
            lab.InitTasks();
        }
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
    }

    // TODO set scene to lab
    public void LoadScene()
    {
        if (!disableSceneLoading && !LabSceneIsLoaded)
        {
            StartCoroutine(AsyncLoadScene());
        }
    }

    public void UnloadScene()
    {
        // TODO change player position check  to collider check
        if (!disableSceneLoading && LabSceneIsLoaded && playerTransform.position.z < 0) 
        {
            StartCoroutine(AsyncUnloadScene());
        }
    }

    IEnumerator AsyncLoadScene()
    {
        AsyncOperation task = SceneManager.LoadSceneAsync(GetCurrentLab().internalName, LoadSceneMode.Additive);
        while (!task.isDone)
        {
            yield return null;
        }
        //RenderSettings.skybox = GetCurrentLab().associatedSkybox;
        LabSceneIsLoaded = true;
        GameObject.FindGameObjectsWithTag("TaskManager")[0].GetComponent<LabTasksManager>().SetLab(GetCurrentLab());
    }

    // TODO delete elements from lab (gaged objects)
    IEnumerator AsyncUnloadScene()
    {
        AsyncOperation task = SceneManager.UnloadSceneAsync(GetCurrentLab().internalName);
        while (!task.isDone)
        {
            yield return null;
        }
        //RenderSettings.skybox = DefaultSkybox;
        LabSceneIsLoaded = false;
    }

}
