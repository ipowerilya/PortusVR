using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabMenuUI : MonoBehaviour
{
    public HubOverview hubOverview;

    public GameObject buttonPrefab;
    public float buttonSpacing = 20f;
    public float buttonOffset = 0;

    public void Start()
    {
        var canvas = GetComponent<Canvas>();
        var labs = hubOverview.labs;
        
        if (labs.Count > 0)
        {
            hubOverview.SetCurrentLabIndex(0);
        }

        for (int i = 0; i < labs.Count; i++)
        {
            var obj = Instantiate(
                buttonPrefab,
                canvas.transform
            );
            var rect = obj.GetComponent<RectTransform>();
            rect.localPosition = Vector3.down*(buttonOffset + (rect.rect.height + buttonSpacing)*i);

            var button = obj.GetComponent<Button>();
            var labIndex = i;
            button.onClick.AddListener(() => {
                hubOverview.SetCurrentLabIndex(labIndex);
            });

            if (i == hubOverview.GetCurrentLabIndex())
            {
                var colorBlock = button.colors;
                colorBlock.normalColor = Color.green;
                colorBlock.highlightedColor = Color.green;
                button.colors = colorBlock;
            }

            var text = button.GetComponentInChildren<Text>();
            text.text = hubOverview.labs[i].labName;
        }
    }
}
