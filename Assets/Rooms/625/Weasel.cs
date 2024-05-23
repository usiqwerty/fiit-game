using UnityEngine;

public class Weasel : MonoBehaviour
{
    public int NeededVectors;
    public Artefact Award;
    public float Speed;
    
    private int _collectedVectors;
    private GameObject _player;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = gameObject.GetComponent<Rigidbody2D>();
        
        //TODO: если убит, то не спавнить
    }
    
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
            _collectedVectors++;
            
            if (_collectedVectors == NeededVectors)
            {
                var pos = transform.position;
                var clone = Instantiate(Award.gameObject);
                clone.GetComponent<Artefact>().OnDrop(pos.x, pos.y);
                Die();
            }
        }
    }

    private void Die()
    {
        //TODO: анимация?
        Destroy(gameObject);
    }
}
