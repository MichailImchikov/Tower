using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AttackZoneConfig))]
public class AttackZoneEditor : Editor
{
    private AttackZoneConfig zone;

    private void OnEnable()
    {
        zone = (AttackZoneConfig)target;

        // Проверка и инициализация матрицы, если она пустая
        if (zone.matrix == null || zone.matrix.Count == 0)
        {
            zone.matrix = new List<AttackZoneConfig.MatrixRow>
            {
                new AttackZoneConfig.MatrixRow { values = new List<int> { 0, 0 } },
                new AttackZoneConfig.MatrixRow { values = new List<int> { 0, 0 } }
            };
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Отображаем матрицу
        EditorGUILayout.LabelField("Matrix", EditorStyles.boldLabel);
        for (int i = 0; i < zone.matrix.Count; i++)
        {
            if (zone.matrix[i] == null)
            {
                zone.matrix[i] = new AttackZoneConfig.MatrixRow { values = new List<int>() };
            }

            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < zone.matrix[i].values.Count; j++)
            {
                zone.matrix[i].values[j] = EditorGUILayout.IntField(zone.matrix[i].values[j], GUILayout.Width(50));
            }
            EditorGUILayout.EndHorizontal();
        }

        // Кнопки для добавления строк и столбцов
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Row"))
        {
            AddRow();
        }
        if (GUILayout.Button("Add Column"))
        {
            AddColumn();
        }
        EditorGUILayout.EndHorizontal();

        // Кнопка для поворота матрицы
        if (GUILayout.Button("Rotate Matrix"))
        {
            zone.RotateMatrix();
            EditorUtility.SetDirty(zone); // Помечаем объект как измененный
        }

        if (GUILayout.Button("Remove Row") && zone.matrix.Count > 1)
        {
            RemoveRow();
        }
        if (GUILayout.Button("Remove Column") && GetColumnCount() > 1)
        {
            RemoveColumn();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(zone); // Помечаем объект как измененный
        }

        serializedObject.ApplyModifiedProperties();
    }

    private int GetColumnCount()
    {
        return zone.matrix.Count > 0 ? zone.matrix[0].values.Count : 0;
    }

    private void RemoveRow()
    {
        if (zone.matrix.Count > 1)
        {
            zone.matrix.RemoveAt(zone.matrix.Count - 1);
        }
    }

    private void RemoveColumn()
    {
        int columnCount = GetColumnCount();
        if (columnCount > 1)
        {
            foreach (var row in zone.matrix)
            {
                if (row != null && row.values.Count > 0)
                {
                    row.values.RemoveAt(row.values.Count - 1);
                }
            }
        }
    }

    private void AddRow()
    {
        int columnCount = GetColumnCount();
        var newRow = new AttackZoneConfig.MatrixRow { values = new List<int>(new int[columnCount]) };
        zone.matrix.Add(newRow);
    }

    private void AddColumn()
    {
        foreach (var row in zone.matrix)
        {
            if (row != null)
            {
                row.values.Add(0);
            }
        }
    }
}