using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed = 1;
    void Start()
    {
        //transform = GetComponent<Transform>();
    }
    
    void Update()
    {
        var absSpeed = 10;
        var newspeed = new Vector2 (0, 0);
        if (Input.GetKey(KeyCode.UpArrow))
            newspeed.y = absSpeed;
        if (Input.GetKey(KeyCode.DownArrow))
            newspeed.y = -absSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
            newspeed.x = -absSpeed;
        if (Input.GetKey(KeyCode.RightArrow))
            newspeed.x = absSpeed;
            
        var old = transform.position;
        transform.position = new Vector2(old.x + newspeed.x * Time.deltaTime, old.y + newspeed.y * Time.deltaTime);
    
    }
}
