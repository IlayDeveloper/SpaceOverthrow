using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Records
{
    public const string FileName = "Records.json";
    public static Table table 
    {
        get
        {
            if (_table == null)
            {
                _table = GetTable();
            }

            return _table;
        }
    }
    private static Table _table;
    
    public static void SaveRecord(float timerPoints)
    {
        Row newRow = new Row(timerPoints);
        table.AddRow(newRow);
        string path = Path.Combine(Application.dataPath, FileName);
        string JSON = JsonUtility.ToJson(table);
        File.WriteAllText(path, JSON);
    }

    public static Row[] GetRecords()
    {
        return table.rows;
    }

    private static Table GetTable()
    {
        string path = Path.Combine(Application.dataPath, FileName);
        return JsonUtility.FromJson<Table>(File.ReadAllText(path));
    }

    [Serializable]
    public struct Row
    {
        public float value;

        public Row( float value)
        {
            this.value = value;
        }
    }
    
    [Serializable]
    public class Table
    {
        public const int MaxRowCount = 5;
        public Row[] rows;

        public void AddRow(Row newRow)
        {
            List<Row> newRows = new List<Row>();

            // Если значение больше значения из последей строки, то добавляем в таблицу
            if(newRow.value > rows[rows.Length-1].value)
            {
                bool added = false;

                // Определяем порядкоывй id для новой строки
                for (int i = 0; i < rows.Length-1; i++)
                {
                    if (rows[i].value < newRow.value)
                    {
                        newRows.Add(newRow);
                        added = true;
                        continue;
                    }
                    
                    if(!added)
                        newRows.Add(rows[i]);
                    else
                    {
                        newRows.Add(rows[i+1]);
                    }
                }
            }
        }
    }
}