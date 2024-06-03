using UnityEngine;

public class Weasel : MonoBehaviour
{
    public int NeededVectors;
    public Artefact Award;

    private int _collectedVectors;
    private Enemy _enemy;
    void Start()
    {
        _enemy = gameObject.GetComponent<Enemy>();
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
                _enemy.Die();
            }
        }
    }
    
}
