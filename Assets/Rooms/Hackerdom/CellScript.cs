using System;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public GridScript Grid;
    public Vector2Int Position;

    private bool Status;
    private SpriteRenderer _sr;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")
            || !Grid.EditEnabled)
            return;
        Status = !Status;
        if (Status)
            _sr.color = Color.black;
        else
            _sr.color = Color.white;
        Grid.SetCell(Position, Status);
    }

    public void SetComplete()
    {
        if (_sr.color == Color.black)
            _sr.color = Color.green;
    }
}
