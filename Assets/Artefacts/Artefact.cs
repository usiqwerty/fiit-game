using UnityEngine;

public class Artefact : MonoBehaviour
{
    /// <summary>Ключ, соответствующий этому артефакту.</summary>
    public string KeyName;
    private const float DropSpeed = 5f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if (KeySystem.ContainsKey(KeyName))
            Destroy(gameObject);
    }
    
    public void OnGrab()
    {
        KeySystem.GrabArtefact(this);
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
        transform.position = new Vector2(x + 1, y + 1);
        gameObject.SetActive(true);
        _rb.velocity = new Vector3(DropSpeed, DropSpeed, 0);
    }
}
