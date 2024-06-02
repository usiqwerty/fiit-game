using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridScript : MonoBehaviour
{
    public int Width;
    public int Height;
    public GameObject CellPrefab;

    public Vector2Int[] PassState;

    public GameObject CompleteObject;

    public bool EditEnabled { get; private set; }

    private CellScript[] _cells;
    private HashSet<Vector2Int> _passState;
    private List<Vector2Int> _correct;
    private List<Vector2Int> _missed;

    void Start()
    {
        _cells = new CellScript[Width * Height];
        for (var i = 0; i < Width; i++)
            for (var j = 0; j < Height; j++)
            {
                var cell = Instantiate(CellPrefab, transform).GetComponent<CellScript>();
                cell.Position = new Vector2Int(i, j);
                cell.transform.localPosition = new Vector2(i, j);
                cell.Grid = this;
                _cells[i * Height + j] = cell;
            }
        EditEnabled = true;
        _passState = new HashSet<Vector2Int>(PassState);
        _correct = new List<Vector2Int>();
        _missed = new List<Vector2Int>();
    }

    public void SetCell(Vector2Int position, bool value)
    {
        var list = _passState.Contains(position)
            ? _correct : _missed;
        if (value && !list.Contains(position))
            list.Add(position);
        else if (!value && list.Contains(position))
            list.Remove(position);
        if (_correct.Count == _passState.Count
            && _missed.Count == 0)
            Complete();
    }

    private void Complete()
    {
        EditEnabled = false;
        foreach (var cell in _cells)
            cell.SetComplete();
        CompleteObject.SetActive(true);
    }
}
