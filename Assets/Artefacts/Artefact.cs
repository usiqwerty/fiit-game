using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Artefact : MonoBehaviour
{
    public string Name;
    private const float DropSpeed = 5f;
    private Rigidbody2D _rb;
    private GameObject _player;

    void Start()
    {
        //артефакт изначально лежит в комнате
        if (ArtefactStorage.ContainsKey(Name))
            Destroy(gameObject);
    }
    private void Awake()
    {
        //артефакт создаётся в процессе игры
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }
    public void OnGrab()
    {
        ArtefactStorage.GrabArtefact(this);
        gameObject.SetActive(false);
        _rb.velocity = Vector2.zero;
    }

    /// <summary>
    /// Анимация выпадения артефакта
    /// </summary>
    /// <param name="x">координата игрока</param>
    /// <param name="y">координата игрока</param>
    public void OnDrop(float x, float y)
    {
        transform.position = new Vector3(x + 1, y + 1, 0);
        _rb.velocity = new Vector2(DropSpeed, DropSpeed);
    }

    void OnGUI()
    {
        var pos = transform.position;
        
        if ((_player.transform.position - pos).magnitude < 2)
        {
            GUI.Label(new Rect(100, 160, 200, 30), Name);
        }
    }
}