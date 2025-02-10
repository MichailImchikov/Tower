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

        //// Проверка и инициализация матрицы, если она пустая
        //if (zone.attackZone.matrix == null || zone.attackZone.matrix.Count == 0)
        //{
        //    zone.attackZone.matrix = new List<AttackZoneConfig.MatrixRow>
        //    {
        //        new AttackZoneConfig.MatrixRow { values = new List<int> { 0, 0 } },
        //        new AttackZoneConfig.MatrixRow { values = new List<int> { 0, 0 } }
        //    };
        //}
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Отображаем матрицу
        EditorGUILayout.LabelField("Matrix", EditorStyles.boldLabel);
        for (int i = 0; i < zone.attackZone.matrix.Count; i++)
        {
            if (zone.attackZone.matrix[i] == null)
            {
                zone.attackZone.matrix[i] = new MatrixRow { values = new List<int>() };
            }

            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < zone.attackZone.matrix[i].values.Count; j++)
            {
                zone.attackZone.matrix[i].values[j] = EditorGUILayout.IntField(zone.attackZone.matrix[i].values[j], GUILayout.Width(50));
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
            zone.attackZone.RotateMatrix();
            EditorUtility.SetDirty(zone); // Помечаем объект как измененный
        }

        if (GUILayout.Button("Remove Row") && zone.attackZone.matrix.Count > 1)
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
        return zone.attackZone.matrix.Count > 0 ? zone.attackZone.matrix[0].values.Count : 0;
    }

    private void RemoveRow()
    {
        if (zone.attackZone.matrix.Count > 1)
        {
            zone.attackZone.matrix.RemoveAt(zone.attackZone.matrix.Count - 1);
        }
    }

    private void RemoveColumn()
    {
        int columnCount = GetColumnCount();
        if (columnCount > 1)
        {
            foreach (var row in zone.attackZone.matrix)
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
        var newRow = new MatrixRow { values = new List<int>(new int[columnCount]) };
        zone.attackZone.matrix.Add(newRow);
    }


    private void AddColumn()
    {
        foreach (var row in zone.attackZone.matrix)
        {
            if (row != null)
            {
                row.values.Add(0);
            }
        }
    }
}