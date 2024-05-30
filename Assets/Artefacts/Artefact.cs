using UnityEngine;

public class Artefact : MonoBehaviour
{
    private MessageBoxEntry _messageBox;

    private bool _isShow = false;
    public string Name;
    private const float DropSpeed = 5f;
    private Rigidbody2D _rb;
    private GameObject _player;

    void Start()
    {
        //артефакт изначально лежит в комнате
        if (ArtefactStorage.ContainsKey(Name))
            Destroy(gameObject);
        _messageBox = GameObject.Find("MessageBox").GetComponent<MessageBox>().CreateEntry();
    }

    private void Awake()
    {
        //артефакт создаётся в процессе игры
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        _messageBox.Disable();
    }

    private void Update()
    {
        if (Name != string.Empty &&
            _isShow != ((_player.transform.position - transform.position).sqrMagnitude < 4))
        {
            _isShow = !_isShow;
            if (_isShow)
                _messageBox.Activate(Name);
            else
                _messageBox.Disable();
        }
    }

    public void OnGrab()
    {
        ArtefactStorage.GrabArtefact(this);
        gameObject.SetActive(false);
        _messageBox.Disable();
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
}