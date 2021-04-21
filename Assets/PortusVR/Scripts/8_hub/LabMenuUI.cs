using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabMenuUI : MonoBehaviour
{
    public LabMenu labMenu;

    public GameObject buttonPrefab;
    public float buttonSpacing = 20f;
    public float buttonOffset = 0;

    public Color buttonColorNormal = Color.white;
    public Color buttonColorHighlight = Color.green;

    Button selectedButton;

    ColorBlock normalColorBlock;
    ColorBlock highlightColorBlock;

    public void Start()
    {
        var canvas = GetComponent<Canvas>();
        var labs = labMenu.labs;
        
        if (labs.Count > 0)
        {
            labMenu.SelectLab(labs[0]);
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
            var lab = labs[i];
            button.onClick.AddListener(() => {
                labMenu.SelectLab(lab);
            });
            
            var text = button.GetComponentInChildren<Text>();
            text.text = labs[i].Name;
        }
    }
}
