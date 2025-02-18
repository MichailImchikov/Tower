using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
[System.Serializable]
public class Matrix
{
    public List<MatrixRow> matrix = new List<MatrixRow>() { new MatrixRow()};
}
[System.Serializable]
public class MatrixRow
{
    public List<int> values = new List<int>() {0};
}