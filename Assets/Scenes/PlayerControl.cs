using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// using Input = UnityEngine.Windows.Input;

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
