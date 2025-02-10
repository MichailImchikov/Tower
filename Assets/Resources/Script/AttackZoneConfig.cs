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

    // ћетод CreateInstance дл€ инициализации матрицы
    public static AttackZoneConfig Create()
    {
        var config = ScriptableObject.CreateInstance<AttackZoneConfig>();
        config.InitializeMatrix();
        return config;
    }

    private void InitializeMatrix()
    {
        // »нициализаци€ начальной матрицы 2x2
        matrix.Add(new MatrixRow { values = new List<int> { 0, 0 } });
        matrix.Add(new MatrixRow { values = new List<int> { 0, 0 } });
    }

    /// <summary>
    /// ѕоворачивает матрицу на 90 градусов по часовой стрелке.
    /// </summary>
    public void RotateMatrix()
    {
        if (matrix == null || matrix.Count == 0 || matrix[0].values == null || matrix[0].values.Count == 0)
            return;

        int rows = matrix.Count;
        int cols = matrix[0].values.Count;

        // —оздаем новую матрицу с повернутыми значени€ми
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

        // «амен€ем старую матрицу на новую
        matrix = rotatedMatrix;
    }
}