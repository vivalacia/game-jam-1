using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerDef : MonoBehaviour
{


    public LayerMask layer;
    private Collider2D ball_collider;
    private float offset;
    private int dir = 0;
    private float globalDir = 0;
    private Movement ball_movement;

    private string ballState;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(1))
        {
            dir = 1;
            def();
        }

        else if (Input.GetMouseButtonDown(2))
        {
            dir = -1;
            def();
        }

        
    }

    void def()
    {
        if (dir != 0)
        {
            globalDir = dir;
            ball_collider = Physics2D.OverlapBox(
                new Vector3(this.transform.position.x + globalDir, this.transform.position.y, 0)
                , new UnityEngine.Vector2(1, 3)
                , 0, layer);


            if (ball_collider)
            {
                ball_movement = ball_collider.GetComponentInParent<Movement>();
                ball_movement.bounce();
            }
        }



    }

    void stun()
    {
        PlayerMovement mov;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x + globalDir, this.transform.position.y , 0), new Vector3(1,3,0));
    }
}
