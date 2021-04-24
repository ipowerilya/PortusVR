using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class HubOverview : MonoBehaviour
{
    public List<Lab> labs;
    public LabOverviewUI labOverviewUI;
    public LabMenuUI labMenuUI;
    public bool DEBUGdisableSceneLoading = false;
    public bool DEBUGdisableЬailSending = true;
    public Material DefaultSkybox;
    public SendMailByMailMessage mailSender;

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
        if (!DEBUGdisableSceneLoading && loadedScene == null)
        {
            StartCoroutine(AsyncLoadScene());
        }
    }

    public void UnloadScene()
    {
        // TODO change player position check  to collider check
        if (!DEBUGdisableSceneLoading && loadedScene != null && playerTransform.position.z < 0) 
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

    public void DumpAllTablesAndSendByMail(string recipient)
    {
        if (DEBUGdisableЬailSending)
        {
            Debug.Log("Sending mails disabled");
            return;

        }
        char delimeter = ',';
        char lineSeparator = '\n';
        var absPath = Application.persistentDataPath + "/" + "username" + "_all_labs_results.csv";
        Debug.Log("dumping table " + absPath);
        var file = File.Open(absPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
        file.SetLength(0); // flush
        var writer = new StreamWriter(file);

        foreach (var lab in labs)
        {
            writer.Write(lab.labName);
            writer.Write(delimeter);
            writer.Write(lineSeparator);

            foreach (var task in lab.tasks)
            {
                writer.Write(lab.labName);
                writer.Write(delimeter);
                writer.Write(lineSeparator);

                var taskTable = task.table;
                var rowCount = taskTable.GetMaxListCount();
                foreach (var key in taskTable.orderedKeys)
                {
                    writer.Write(key);
                    writer.Write(delimeter);
                }
                writer.Write(lineSeparator);
                for (int i = 0; i < rowCount; ++i)
                {
                    for (int j = 0; j < taskTable.rawTable.Count; ++j)
                    {
                        if (taskTable.rawTable[j].Count > i)
                            writer.Write(taskTable.rawTable[j][i]);
                        else
                            writer.Write("");
                        writer.Write(delimeter);
                    }
                    writer.Write(lineSeparator);
                }
            }
        }
        writer.Close();
        mailSender.SendEmail(recipient, absPath);
    }

}
