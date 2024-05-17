using UnityEngine;

namespace Scenes
{
    public enum ArtefactName
    {
        Calculator,
        Differential,
    }

    public class Artefact : MonoBehaviour
    {
        public ArtefactName type;
        public bool spawnOnStart;
        private Rigidbody2D _rb;
        private const float DropSpeed = 5f;

        private void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }

        public void OnGrab()
        {
            gameObject.SetActive(false);
            _rb.velocity = Vector2.zero;
        }
        public void OnDrop(float x, float y)
        {
            transform.position = new Vector2(x + 1, y + 1);
            gameObject.SetActive(true);
            _rb.velocity = new Vector3(DropSpeed, DropSpeed, 0);
        }
    }
}