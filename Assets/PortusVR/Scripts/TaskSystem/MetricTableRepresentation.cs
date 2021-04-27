using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System; // for debug
using UnityEngine;
using UnityEngine.UI;

public class MetricTableRepresentation : MonoBehaviour
{
    public GameObject elementPrefab;
    public float maxCellWidth = 200;
    GridLayoutGroup group;
    MetricTable table;

    public void Awake()
    {
        group = GetComponentInChildren<GridLayoutGroup>();
    }

    public void SetTable(MetricTable table) 
    {
        this.table = table;
    }

    void AppendText(LayoutGroup group, string text)
    {
        Instantiate(elementPrefab, group.transform).GetComponent<Text>().text = text;
    }

    void AddHeading()
    {
        AppendText(group, "№");
        foreach (var key in table.orderedKeys)
        {
            AppendText(group, key);
        }
    }

    void ClearRows()
    {
        for (int i = group.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(group.transform.GetChild(i).gameObject);
        } 
    }

    static float GetStandardDeviation(List<float> values)
    {
        float avg = values.Average();
        float sum = values.Sum(v => (v - avg) * (v - avg));
        float denominator = values.Count - 1;
        return denominator > 0.0 ? Mathf.Sqrt(sum / denominator) : -1;
    }

    void AppendStatistics()
    {
        if (table.GetMaxListCount() > 0)
        {
            AppendText(group, "Mean");
            for (int i = 0; i < table.rawTable.Count; ++i)
            {
                var values = table.rawTable[i];
                if (values.Count == 0)
                {
                    AppendText(group, "empty");
                }
                else
                {
                    AppendText(group, values.Average().ToString());
                }
            }
            AppendText(group, "Sigma");
            for (int i = 0; i < table.rawTable.Count; ++i)
            {
                var values = table.rawTable[i];
                if (values.Count == 0)
                {
                    AppendText(group, "empty");
                }
                else
                {
                    AppendText(group, GetStandardDeviation(values).ToString());
                }
            }
        }
    }

    public void ResetTable()
    {
        var colCount = table.orderedKeys.Count + 1;
        
        var cellSize = group.cellSize; 
        cellSize.x = Mathf.Min(gameObject.GetComponent<RectTransform>().rect.width / colCount, maxCellWidth);
        group.cellSize = cellSize;

        group.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        group.constraintCount = colCount;
        ClearRows();
        AddHeading();
    }

    public void UpdateTable()
    {
        ResetTable();
        AppendStatistics();
        for (int i = 0; i < table.GetMaxListCount(); ++i)
        {
            AppendText(group, (i+1).ToString());
            for (int j = 0; j < table.rawTable.Count; ++j)
            {
                if (table.rawTable[j].Count > i)
                    AppendText(group, table.rawTable[j][i].ToString());
                else
                    AppendText(group, "empty");
            }

            //Destroy(elementGroup);
        }
    }
}
