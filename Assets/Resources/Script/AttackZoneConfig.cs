using System.Collections.Generic;
using UnityEngine;

public class AttackZoneConfig : ScriptableObject
{
    [System.Serializable]
    public class MatrixRow
    {
        public List<int> values = new List<int>();
    }

    public List<MatrixRow> matrix = new List<MatrixRow>();

    // ����� CreateInstance ��� ������������� �������
    public static AttackZoneConfig Create()
    {
        var config = ScriptableObject.CreateInstance<AttackZoneConfig>();
        config.InitializeMatrix();
        return config;
    }

    private void InitializeMatrix()
    {
        // ������������� ��������� ������� 2x2
        matrix.Add(new MatrixRow { values = new List<int> { 0, 0 } });
        matrix.Add(new MatrixRow { values = new List<int> { 0, 0 } });
    }

    /// <summary>
    /// ������������ ������� �� 90 �������� �� ������� �������.
    /// </summary>
    public void RotateMatrix()
    {
        if (matrix == null || matrix.Count == 0 || matrix[0].values == null || matrix[0].values.Count == 0)
            return;

        int rows = matrix.Count;
        int cols = matrix[0].values.Count;

        // ������� ����� ������� � ����������� ����������
        var rotatedMatrix = new List<MatrixRow>();
        for (int c = 0; c < cols; c++)
        {
            var newRow = new MatrixRow { values = new List<int>() };
            for (int r = rows - 1; r >= 0; r--)
            {
                newRow.values.Add(matrix[r].values[c]);
            }
            rotatedMatrix.Add(newRow);
        }

        // �������� ������ ������� �� �����
        matrix = rotatedMatrix;
    }
}