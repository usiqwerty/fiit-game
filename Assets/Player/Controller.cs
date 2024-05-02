using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    private Rigidbody2D _body;

    private float _horizontal;
    private float _vertical;

    private DoorScript _targetDoor;
    private bool _isDoorEnabled;

    /// <summary>Скорость движения персонажа.</summary>
    public float Speed = 10.0f;

    public string CurrentScene { get; private set; }

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
            SceneManager.LoadScene(_targetDoor.SceneName);
            CurrentScene = _targetDoor.SceneName;
            gameObject.transform.position = _targetDoor.TargetPlayerPosition;
        }
    }

    void FixedUpdate()
    {
        var vertivalSpeed = _vertical * Speed;
        var horizontalSpeed = _horizontal * Speed;
        if (_horizontal != 0 && _vertical != 0)
        {
            vertivalSpeed *= 0.7f;
            horizontalSpeed *= 0.7f;
        }
        _body.velocity = new Vector2(horizontalSpeed, vertivalSpeed);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            _targetDoor = collision.GetComponent<DoorScript>();
            _isDoorEnabled = KeySystem.ContainsKeys(_targetDoor.RequiredKeys);
            return;
        }
        if (collision.CompareTag("Artefact"))
        {
            var artefact = collision.GetComponent<ArtefactScript>();
            KeySystem.AddKey(artefact.KeyName);
            //TODO: анимация взятия артефакта
            Destroy(artefact.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            if (_targetDoor == collision.GetComponent<DoorScript>())
            {
                _targetDoor = null;
                _isDoorEnabled = false;
            }
        }
    }

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
