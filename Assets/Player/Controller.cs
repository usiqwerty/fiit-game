using System;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 10.0f;

    private Rigidbody2D _body;
    private float _horizontal;
    private float _vertical;

    private float _prevDropTime;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Q) && ArtefactStorage.Count > 0)
        {
            if (!(Time.time - _prevDropTime > 0.1)) return;
            
            _prevDropTime = Time.time;

            var pos = transform.position;
            ArtefactStorage.DropLastKey(pos.x, pos.y);
        }
    }

    void FixedUpdate()
    {
        var vertivalSpeed = _vertical * speed;
        var horizontalSpeed = _horizontal * speed;
        if (_horizontal != 0 && _vertical != 0)
        {
            vertivalSpeed *= 0.7f;
            horizontalSpeed *= 0.7f;
        }

        _body.velocity = new Vector2(horizontalSpeed, vertivalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Artefact"))
        {
            var artefact = other.gameObject.GetComponent<Artefact>();
            artefact.OnGrab();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            var enemy = other.gameObject.GetComponent<Enemy>();

            if (ArtefactStorage.Artefacts.Any(artefact => enemy.TryDie(artefact)))
            {
                //killed enemy
            }
            else
            {
                print(ArtefactStorage.Artefacts);
                print(enemy);
                Die();
            }
        }
    }

    private void Die()
    {
        throw new NotImplementedException();
    }
}