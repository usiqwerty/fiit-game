using UnityEngine;
using UnityEngine.Serialization;

public class Artefact : MonoBehaviour
{
    public string Name;
    private const float DropSpeed = 5f;
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        if (ArtefactStorage.ContainsKey(Name))
            Destroy(gameObject);
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
        transform.position = new Vector2(x + 1, y + 1);
        gameObject.SetActive(true);
        _rb.velocity = new Vector3(DropSpeed, DropSpeed, 0);
    }
}
