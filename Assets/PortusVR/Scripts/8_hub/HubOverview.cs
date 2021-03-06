using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//tag TaskSystem
public class HubOverview : MonoBehaviour
{
    public List<Lab> labs;
    public LabOverviewUI labOverviewUI;
    public LabMenuUI labMenuUI;
    public bool disableSceneLoading = false;
    public Material DefaultSkybox;
    public GameObject taskSystemRoot;

    public Transform playerTransform; // for scene loading
    public BNG.ScreenFader playerScreenFader; // for screen fading during scene preloading (should have alpha 255 initially)

    public int currentLabIndex = 0;
    string loadedScene = null;

    private void Start()
    {
        if (!disableSceneLoading)
        {
            StartCoroutine(AsyncPreloadScenes());
        }
        else
        {
            playerScreenFader.SetFadeLevel(0f);
            TeleportTaskSystemSoItActuallyProbablyWorks();
            UpdateTaskManager();
        }
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

    public void DeleteCurrentLabData()
    {
        GetCurrentLab().DeleteAllTasks();
        UpdateUI();
    }

    public void UpdateUI()
    {
        labOverviewUI.SetLab(GetCurrentLab());
        labMenuUI.UpdateUI(currentLabIndex);
        UpdateTaskManager();
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

    void UpdateTaskManager()
    {
        GameObject.FindGameObjectWithTag("TaskManager").GetComponent<LabTasksManager>().SetLab(GetCurrentLab());
    }

    public void ToggleAutoComputation ()
    {
        GetCurrentLab().enabledAutoComputation = !GetCurrentLab().enabledAutoComputation;
        UpdateUI();
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
        TeleportTaskSystemSoItActuallyProbablyWorks();
        UpdateTaskManager();
    }

    // TODO delete elements from lab (gaged objects)
    IEnumerator AsyncUnloadScene()
    {
        GetCurrentLab().SaveDoneTasks();
        UpdateUI();
        AsyncOperation task = SceneManager.UnloadSceneAsync(loadedScene);
        while (!task.isDone)
        {
            yield return null;
        }
        loadedScene = null;
        RenderSettings.skybox = DefaultSkybox;
    }

    IEnumerator AsyncPreloadScenes()
    {
        Debug.Log("Starting scene preloading");
        foreach (var lab in labs)
        {
            var sceneName = lab.internalName;
            AsyncOperation loadTask = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Debug.Log("Loading scene " + lab.internalName);
            while (!loadTask.isDone) yield return null;

            Debug.Log("Unloading scene " + lab.internalName);
            AsyncOperation unloadTask = SceneManager.UnloadSceneAsync(sceneName);
            while (!unloadTask.isDone) yield return null;
        }
        Debug.Log("Scene preloading finished");
        playerScreenFader.DoFadeOut();
    }

    public void TeleportTaskSystemSoItActuallyProbablyWorks()
    {
        var targetTransform = GameObject.FindGameObjectsWithTag("TaskSystemTarget")[0].transform;
        taskSystemRoot.transform.position = targetTransform.position;
        taskSystemRoot.transform.rotation = targetTransform.rotation;
        taskSystemRoot.transform.localScale = targetTransform.localScale;
    }
}
