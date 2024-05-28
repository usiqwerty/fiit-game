using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Name;
    public float Speed;
    public Artefact[] DroppableAward;
    public Artefact[] Weaknesses;
    public bool FollowPlayer;

    /// <summary>Точки по которым патрулирует враг</summary>
    public Vector2[] PatrolPoints;
    private int _currentPatrolPoint;

    private GameObject _player;
    private Rigidbody2D _rb;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        if (GameProgress.EnemiesKilled.Contains(Name))
            Destroy(gameObject);
    }

    void Update()
    {
        if (FollowPlayer)
        {
            var path = _player.transform.position - _rb.transform.position;
            _rb.velocity = Speed * path.normalized;
        }
        else if (PatrolPoints.Length > 0)
        {
            var path = PatrolPoints[_currentPatrolPoint] - (Vector2)_rb.transform.position;
            if (path.magnitude < Speed / 100)
                _currentPatrolPoint = (_currentPatrolPoint + 1) % PatrolPoints.Length;
            _rb.velocity = Speed * path.normalized;
        }
        else
            _rb.velocity = Vector2.zero;
    }

    public bool TryDie(Artefact artefact)
    {
        if (!Weaknesses.Any(wkns => wkns.Name == artefact.Name)) return false;
        Die();
        return true;
    }

    private void Die()
    {
        DropAllAwards();
        Destroy(gameObject);
        GameProgress.EnemiesKilled.Add(Name);
    }

    public void DropAllAwards()
    {
        foreach (var award in DroppableAward)
        {
            var pos = transform.position;
            var clone = Instantiate(award.gameObject);
            var artefact = clone.GetComponent<Artefact>();
            artefact.OnDrop(pos.x, pos.y);
        }
    }
}