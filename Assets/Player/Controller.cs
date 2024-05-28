using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    public float speed = 10.0f;
    public string CurrentScene { get; private set; }


    private Rigidbody2D _body;
    private float _horizontal;
    private float _vertical;

    private DoorScript _targetDoor;
    private bool _isDoorEnabled;

    private float _prevDropTime;


    public void Initialize(string scene, Vector2 position)
    {
        CurrentScene = scene;
        gameObject.transform.position = position;
    }

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (_isDoorEnabled && Input.GetKey(KeyCode.E))
        {
            DontDestroyOnLoad(gameObject);
            // foreach (var artefact in ArtefactStorage.Artefacts)
            // {
            //     DontDestroyOnLoad(artefact);
            //     artefact.gameObject.SetActive(false);
            // }
            SceneManager.LoadScene(_targetDoor.SceneName);
            CurrentScene = _targetDoor.SceneName;
            gameObject.transform.position = _targetDoor.TargetPlayerPosition;
        }
        else if (Input.GetKey(KeyCode.Q) && ArtefactStorage.Count > 0)
        {
            if (!(Time.time - _prevDropTime > 0.1)) return;
            
            _prevDropTime = Time.time;
            var pos = transform.position;
            ArtefactStorage.DropLastKey(pos.x, pos.y);
        }
    }

    //TODO: почему два апдейта
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") || collision.CompareTag("DeanDoor"))
        {
            _targetDoor = collision.GetComponent<DoorScript>();
            _isDoorEnabled = ArtefactStorage.ContainsKeys(_targetDoor.RequiredKeys);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door") || collision.CompareTag("DeanDoor"))
        {
            if (_targetDoor == collision.GetComponent<DoorScript>())
            {
                _targetDoor = null;
                _isDoorEnabled = false;
            }
        }
    }

    //TODO: этого не должно быть здесь
    void OnGUI()
    {
        if (_targetDoor != null)
        {
            //TODO: переместить в GUI в юнити
            GUI.Label(new Rect(100, 100, 200, 200), _targetDoor.Text);
            if (_isDoorEnabled)
                GUI.Label(new Rect(100, 130, 200, 200), "Нажмите Е, чтобы войти");
            else
                GUI.Label(new Rect(100, 130, 200, 200), _targetDoor.Warning);
        }
    }
}