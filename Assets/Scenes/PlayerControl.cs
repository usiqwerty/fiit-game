using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private float speed = 1;
    //private Transform transform;
    void Start()
    {
        //transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        var old = transform.position;
        transform.position = new Vector2(old.x + speed * Time.deltaTime, old.y+speed * Time.deltaTime);
    
    }
}
