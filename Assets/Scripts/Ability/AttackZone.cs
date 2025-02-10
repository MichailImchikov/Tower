using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class AttackZone
{
    public List<MatrixRow> matrix = new List<MatrixRow>() { new MatrixRow()};
    public AttackZone RotateMatrix()
    {
        if (matrix == null || matrix.Count == 0 || matrix[0].values == null || matrix[0].values.Count == 0)
            return null;

        int rows = matrix.Count;
        int cols = matrix[0].values.Count;

        // Создаем новую матрицу с повернутыми значениями
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

        // Заменяем старую матрицу на новую
        var newMatrix = new AttackZone();
        newMatrix.matrix = rotatedMatrix;
        return newMatrix;
    }
}
[System.Serializable]
public class MatrixRow
{
    public List<int> values = new List<int>() {0};
}
public static class AttackZoneExtensions
{
    public static List<PointMap> GetAttackPoints(this AttackZone attackZone, PointMap basePoint)

    {
        if (attackZone == null || attackZone.matrix == null || attackZone.matrix.Count == 0)
            return new List<PointMap>();

        int rows = attackZone.matrix.Count;
        int cols = attackZone.matrix[0].values.Count;

        List<PointMap> result = new List<PointMap>();

        // Проходим по всей матрице
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (attackZone.matrix[r].values[c] == 1) // Проверяем, если значение равно 1
                {
                    // Вычисляем абсолютные координаты относительно базовой точки
                    Vector3Int relativePosition = new Vector3Int(c - (cols / 2), (rows / 2) - r, 0);
                    Vector3Int absolutePosition = basePoint.PointToMap + relativePosition;

                    // Создаем новую точку PointMap
                    PointMap attackPoint = GameState.Instance.GetNewPoint(absolutePosition);

                    // Добавляем точку в результат
                    result.Add(attackPoint);
                }
            }
        }

        return result;
    }
}