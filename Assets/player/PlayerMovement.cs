using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 movementDirection;

    [SerializeField]
    [Range(0.05f, 1)]
    float movementSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(movementDirection * movementSpeed);


    }

    public void Move(Vector2 dir)
    {
        //Debug.Log("move: " + dir);
        movementDirection = dir;
    }
}
