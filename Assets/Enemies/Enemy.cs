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
    private GameObject _player;
    private Rigidbody2D _rb;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var path = _player.transform.position - _rb.transform.position;
        _rb.velocity = Speed * path.normalized;
    }

    public bool TryDie(Artefact artefact)
    {
        if (!Weaknesses.Contains(artefact)) return false;
        
        Die();
        return true;

    }

    private void Die()
    {
        foreach (var award in DroppableAward)
        {
            Instantiate(award);
            var pos = _rb.position;
            award.GetComponent<Artefact>().OnDrop(pos.x, pos.y);
        }
        
        Destroy(this.gameObject);
    }
}