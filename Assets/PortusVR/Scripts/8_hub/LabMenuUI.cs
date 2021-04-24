using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabMenuUI : MonoBehaviour
{
    List<Lab> labs;
    
    public VerticalLayoutGroup group;
    public GameObject buttonPrefab;
    public delegate void OnCurLabIndexChange(int x);
    public OnCurLabIndexChange LabCallback;

    public void SetLabs(List<Lab> labs)
    {
        this.labs = labs;
    }

    public void UpdateUI(int selectedLab)
    {
        ClearUI();

        for (int i = 0; i < labs.Count; i++)
        {
            var button = Instantiate(
                buttonPrefab,
                group.transform
            ).GetComponent<Button>();

            var labIndex = i;
            button.onClick.AddListener(() => {
                LabCallback(labIndex);
            });

            if (i == selectedLab)
            {
                var colorBlock = button.colors;
                colorBlock.normalColor = Color.green;
                colorBlock.highlightedColor = Color.green;
                button.colors = colorBlock;
            }

            var text = button.GetComponentInChildren<Text>();
            text.text = labs[i].labName;
        }
    }

    void ClearUI()
    {
        for (int i = group.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(group.transform.GetChild(i).gameObject);
        }
    }
}
