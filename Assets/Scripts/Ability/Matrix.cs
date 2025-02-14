using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Matrix
{
    public List<MatrixRow> matrix = new List<MatrixRow>() { new MatrixRow()};
    public Matrix RotateMatrix()
    {
        if (matrix == null || matrix.Count == 0 || matrix[0].values == null || matrix[0].values.Count == 0)
            return null;

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
        var newMatrix = new Matrix();
        newMatrix.matrix = rotatedMatrix;
        return newMatrix;
    }
}
[System.Serializable]
public class MatrixRow
{
    public List<int> values = new List<int>() {0};
}