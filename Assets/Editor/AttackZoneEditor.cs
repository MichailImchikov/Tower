using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MatrixConfig))]
public class MatrixEditor : Editor
{
    private MatrixConfig zone;

    private void OnEnable()
    {
        zone = (MatrixConfig)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Отображаем матрицу
        EditorGUILayout.LabelField("Matrix", EditorStyles.boldLabel);
        for (int i = 0; i < zone.matrix.matrix.Count; i++)
        {
            if (zone.matrix.matrix[i] == null)
            {
                zone.matrix.matrix[i] = new MatrixRow { values = new List<int>() };
            }

            EditorGUILayout.BeginHorizontal();
            for (int j = 0; j < zone.matrix.matrix[i].values.Count; j++)
            {
                zone.matrix.matrix[i].values[j] = EditorGUILayout.IntField(zone.matrix.matrix[i].values[j], GUILayout.Width(50));
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
            RotateMatrix();
            //EditorUtility.SetDirty(zone); // Помечаем объект как измененный
        }

        if (GUILayout.Button("Remove Row") && zone.matrix.matrix.Count > 1)
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
        return zone.matrix.matrix.Count > 0 ? zone.matrix.matrix[0].values.Count : 0;
    }

    private void RemoveRow()
    {
        if (zone.matrix.matrix.Count > 1)
        {
            zone.matrix.matrix.RemoveAt(zone.matrix.matrix.Count - 1);
        }
    }

    private void RemoveColumn()
    {
        int columnCount = GetColumnCount();
        if (columnCount > 1)
        {
            foreach (var row in zone.matrix.matrix)
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
        zone.matrix.matrix.Add(newRow);
    }


    private void AddColumn()
    {
        foreach (var row in zone.matrix.matrix)
        {
            if (row != null)
            {
                row.values.Add(0);
            }
        }
    }
    private void RotateMatrix()
    {
        int rowCount = zone.matrix.matrix.Count;
        if (rowCount == 0) return;

        int columnCount = zone.matrix.matrix[0].values.Count;

        // Создаем новую матрицу с перевернутыми размерами
        List<MatrixRow> newMatrix = new List<MatrixRow>();

        for (int j = 0; j < columnCount; j++)
        {
            MatrixRow newRow = new MatrixRow { values = new List<int>() };
            for (int i = rowCount - 1; i >= 0; i--)
            {
                newRow.values.Add(zone.matrix.matrix[i].values[j]);
            }
            newMatrix.Add(newRow);
        }

        // Заменяем старую матрицу на новую
        zone.matrix.matrix = newMatrix;

        // Помечаем объект как измененный
        EditorUtility.SetDirty(zone);
    }
}
[CustomEditor(typeof(AttackZoneConfig))]
public class AttackZoneEditor : MatrixEditor
{
}
[CustomEditor(typeof(AttackAreaConfig))]
public class AttackAreaEditor : MatrixEditor { }