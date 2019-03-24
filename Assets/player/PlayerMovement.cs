using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 movementDirection;

    [SerializeField]
    [Range(0.05f, 1)]
    float movementSpeed;

    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.Translate(movementDirection * movementSpeed);
        Debug.Log(movementDirection.x * movementSpeed);
        animator.SetFloat("move", Mathf.Abs(movementDirection.x * movementSpeed * 10));

    }

    public void Move(Vector2 dir)
    {

        movementDirection = dir;
    }
}
