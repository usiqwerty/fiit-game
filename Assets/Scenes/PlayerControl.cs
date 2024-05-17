using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private const float Speed = 10;
    private List<Artefact> _artefacts;
    private float _prevDropTime;
    void Start()
    {
        _prevDropTime = Time.time;
        _artefacts = new List<Artefact>();
    }

    private void GrabArtefact(GameObject artefactObject)
    {
        var art = artefactObject.GetComponent<Artefact>();
        _artefacts.Add(art);
        art.OnGrab();
        print($"Grabbed artefact {_artefacts[^1].type}, {_artefacts.Count}");
    }

    private void DropArtefact(Artefact artefact)
    {
        _artefacts.Remove(artefact);
        var pos = transform.position;
        // artefact.velocity = new Vector2(0.1f, 0.1f);
        artefact.OnDrop(pos.x, pos.y);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Artefact"))
        {
            // if (other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude==0)
            GrabArtefact(other.gameObject);
        }
    }

    void Update()
    {
        var newspeed = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            newspeed.y = Speed;
        if (Input.GetKey(KeyCode.DownArrow))
            newspeed.y = -Speed;
        if (Input.GetKey(KeyCode.LeftArrow))
            newspeed.x = -Speed;
        if (Input.GetKey(KeyCode.RightArrow))
            newspeed.x = Speed;

        if (Input.GetKey(KeyCode.Q) && _artefacts.Count > 0)
        {
            if (Time.time - _prevDropTime > 0.1)
            {
                _prevDropTime = Time.time;
                DropArtefact(_artefacts[^1]);
            }
        }
        
        var old = transform.position;
        transform.position = new Vector2(old.x + newspeed.x * Time.deltaTime, old.y + newspeed.y * Time.deltaTime);
    }
}