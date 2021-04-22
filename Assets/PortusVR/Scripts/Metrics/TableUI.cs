using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    public MetricTable table;
    public RectTransform rowsOffset;
    public GameObject rowGroupPrefab;
    public GameObject rowPrefab;
    public GameObject elementPrefab;

    VerticalLayoutGroup rowGroup;

    void Start()
    {
        InitTable();
        AddHeading();
    }

    HorizontalLayoutGroup AppendRow()
    {
        return Instantiate(rowPrefab, rowGroup.transform).GetComponent<HorizontalLayoutGroup>();
    }

    void AppendText(HorizontalLayoutGroup group, string text)
    {
        Instantiate(elementPrefab, group.transform).GetComponent<Text>();
    }

    void InitTable()
    {
        rowGroup = Instantiate(rowGroupPrefab, rowsOffset.transform).GetComponent<VerticalLayoutGroup>();
    }

    void AddHeading()
    {
        var titleGroup = AppendRow();
        AppendText(titleGroup, "№");
        foreach (var key in table.orderedKeys)
        {
            AppendText(titleGroup, key);
        }
    }

    void ClearRows()
    {
        Destroy(rowGroup);
    }

    void UpdateTable()
    {
        ClearRows();
        InitTable();
        AddHeading();
    
        for (int i = 0; i < table.rawTable[0].Count; ++i)
        {
            var elementGroup = AppendRow();
            for (int j = 0; j < table.rawTable.Count; ++j)
            {
                
            }
        }
    }
}
