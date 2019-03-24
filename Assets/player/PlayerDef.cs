using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Schema;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerDef : MonoBehaviour
{


    public LayerMask layer;
    private Collider2D ball_collider;
    const float offset = 0.5f;
    UnityEngine.Vector2 colliderSize = new UnityEngine.Vector2(2 * offset, 3);
   // private int dir = 0;
    private float globalDir = 0;
    private Movement ball_movement;
    private bool stuned = false;
    private float stunTime = 0;
    PlayerMovement mov;
    private float time = 0;

    private string ballState = "";
    // Start is called before the first frame update
    void Start()
    {
        mov = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (stuned)
        {
            if (Time.time - time >= stunTime)
            {
                stuned = false;
            }
            mov.Move(new Vector2(0,0));
        }
        
        
    }

    public void def(int dir)
    {
        Debug.Log("dir: " + dir + 1);
        if (dir != 0)
        {
            Debug.Log(dir + " " + globalDir);
            globalDir = dir;
            ball_collider = Physics2D.OverlapBox(
                new Vector3(this.transform.position.x + globalDir * offset, this.transform.position.y, 0)
                , colliderSize
                , 0, layer);


            if (ball_collider)
            {
                ball_movement = ball_collider.GetComponentInParent<Movement>();
                ballState = ball_movement.getState();
                var hurtLevel = ball_movement.checkState();
                
                if (ballState == "_third" || ballState == "_second")
                {   
                    hurtPlayer(hurtLevel);
                }

                if (ballState == "_third")
                {
                    stunTime = hurtLevel;
                    time = Time.time;
                    stuned = true;
                    
                }

                ball_movement.bounce();

            }
        }



    }

    void hurtPlayer(float hurtLv)
    {
        Vector2 translator = ball_movement.getRb().velocity.normalized * hurtLv;
        this.transform.Translate(translator);
    }

    void disableArmor()
    {
        
    }

  

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x + globalDir * offset, this.transform.position.y , 0), colliderSize);
    }
}
