using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System; // for debug
using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    public RectTransform rowsOffset;
    public GameObject rowPrefab;
    public GameObject elementPrefab;

    VerticalLayoutGroup rowGroup;

    MetricTable table;

    public void Awake()
    {
        rowGroup = GetComponentInChildren<VerticalLayoutGroup>();
    }

    HorizontalLayoutGroup AppendRow()
    {
        return Instantiate(rowPrefab, rowGroup.transform).GetComponent<HorizontalLayoutGroup>();
    }

    public void SetTable(MetricTable table) 
    {
        this.table = table;
    }

    void AppendText(HorizontalLayoutGroup group, string text)
    {
        Instantiate(elementPrefab, group.transform).GetComponent<Text>().text = text;
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
        for (int i = rowGroup.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(rowGroup.transform.GetChild(i).gameObject);
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
            var elementGroupMean = AppendRow();
            AppendText(elementGroupMean, "Mean");
            for (int i = 0; i < table.rawTable.Count; ++i)
            {
                var values = table.rawTable[i];
                if (values.Count == 0)
                {
                    AppendText(elementGroupMean, "empty");
                }
                else
                {
                    AppendText(elementGroupMean, values.Average().ToString());
                }
            }
            var elementGroupSigma = AppendRow();
            AppendText(elementGroupSigma, "Sigma");
            for (int i = 0; i < table.rawTable.Count; ++i)
            {
                var values = table.rawTable[i];
                if (values.Count == 0)
                {
                    AppendText(elementGroupMean, "empty");
                }
                else
                {
                    AppendText(elementGroupSigma, GetStandardDeviation(values).ToString());
                }
            }
        }

    }

    public void ResetTable()
    {
        ClearRows();
        AddHeading();
    }

    public void UpdateTable()
    {
        ResetTable();
        AppendStatistics();
        for (int i = 0; i < table.GetMaxListCount(); ++i)
        {
            var elementGroup = AppendRow();
            AppendText(elementGroup, (i+1).ToString());
            for (int j = 0; j < table.rawTable.Count; ++j)
            {
                if (table.rawTable[j].Count > i)
                    AppendText(elementGroup, table.rawTable[j][i].ToString());
                else
                    AppendText(elementGroup, "empty");
            }

            //Destroy(elementGroup);
        }
    }
}
