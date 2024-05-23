using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weasel : MonoBehaviour
{
    public int NeededVectors;
    public Artefact Award;
    public float Speed;
    
    private int collectedVectors;
    private GameObject _player;
    private Rigidbody2D _rb;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        //TODO: если убит, то не спавнить
    }

    // Update is called once per frame
    void Update()
    {
        var path = _player.transform.position - _rb.transform.position;
        _rb.velocity = Speed * path.normalized;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyArtefact"))
        {
            Destroy(other.gameObject);
            collectedVectors++;
            print($"{collectedVectors} of {NeededVectors}");
            if (collectedVectors == NeededVectors)
            {
                var pos = transform.position;
                Award.OnDrop(pos.x, pos.y);
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
