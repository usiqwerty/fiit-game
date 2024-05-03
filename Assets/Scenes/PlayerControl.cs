using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed = 1;
    private List<Artefact> _artefacts;

    void Start()
    {
        _artefacts = new List<Artefact>();
    }

    private void GrabArtefact(GameObject artefactObject)
    {
        _artefacts.Add(artefactObject.GetComponent<Artefact>());
        artefactObject.SetActive(false);
        print($"Grabbed artefact {_artefacts[^1].type}, {_artefacts.Count}");
    }

    private void DropArtefact(Artefact artefact)
    {
        _artefacts.Remove(artefact);
        var pos = transform.position;
        artefact.transform.position = new Vector2(pos.x + 2, pos.y + 2);
        artefact.gameObject.SetActive(true);
        
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Artefact"))
        {
            GrabArtefact(other.gameObject);
        }
    }

    void Update()
    {
        var absSpeed = 10;
        var newspeed = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            newspeed.y = absSpeed;
        if (Input.GetKey(KeyCode.DownArrow))
            newspeed.y = -absSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
            newspeed.x = -absSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
            newspeed.x = absSpeed;

        if (Input.GetKey(KeyCode.Q))
        {
            if (_artefacts.Count>0)
                DropArtefact(_artefacts[^1]);
        }
        
        var old = transform.position;
        transform.position = new Vector2(old.x + newspeed.x * Time.deltaTime, old.y + newspeed.y * Time.deltaTime);
    }
}