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
    Tuple<string, int> selectedTableLocation = null;

    delegate void onElementClick(GameObject button);

    public void Awake()
    {
        group = GetComponentInChildren<GridLayoutGroup>();
    }

    public void SetTable(MetricTable table) 
    {
        this.table = table;
    }

    GameObject AppendElement(LayoutGroup group, string text, onElementClick callback = null)
    {
        var obj = Instantiate(elementPrefab, group.transform);
        obj.GetComponentInChildren<Text>().text = text;
        var button = obj.GetComponentInChildren<Button>();
        if (callback != null) button.onClick.AddListener(() => { callback(obj); });
        else button.interactable = false;
        return obj;
    }

    void AddHeading()
    {
        AppendElement(group, "№");
        foreach (var key in table.orderedKeys)
        {
            AppendElement(group, key);
        }
    }

    void ClearRows()
    {
        for (int i = group.transform.childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(group.transform.GetChild(i).gameObject);
        } 
    }

    static float GetStandardDeviation(IReadOnlyList<float> values)
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
            AppendElement(group, "Mean");
            foreach (var key in table.orderedKeys)
            {
                var values = table.GetConstColumn(key);
                if (values.Count == 0)
                {
                    AppendElement(group, "empty");
                }
                else
                {
                    AppendElement(group, values.Average().ToString());
                }
            }
            AppendElement(group, "Sigma");
            foreach (var key in table.orderedKeys)
            {
                var values = table.GetConstColumn(key);
                if (values.Count == 0)
                {
                    AppendElement(group, "empty");
                }
                else
                {
                    AppendElement(group, GetStandardDeviation(values).ToString());
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
            AppendElement(group, (i+1).ToString());
            foreach (var key in table.orderedKeys)
            {
                var columnKey = key;
                var row = i;
                var column = table.GetConstColumn(key);
                var obj = AppendElement(group, column[i].ToString(), callback: (GameObject element) => {
                    selectedTableLocation = new Tuple<string, int>(columnKey, row);
                    UpdateTable();
                });
                if (selectedTableLocation != null && selectedTableLocation.Item1 == columnKey && selectedTableLocation.Item2 == row)
                {
                    var button = obj.GetComponentInChildren<Button>();
                    var colors = button.colors;
                    var color = new Color(1f, 1f, 1f, 0.125f);
                    colors.normalColor = color;
                    colors.highlightedColor = color;
                    button.colors = colors;
                }
            }

            //Destroy(elementGroup);
        }
    }

    public void SetSelectedTableValue(float value)
    {
        if (selectedTableLocation != null) table.AddMetricByIndex(selectedTableLocation.Item1, selectedTableLocation.Item2, value);
        UpdateTable();
    }
}
