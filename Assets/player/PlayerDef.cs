using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Experimental.U2D;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerDef : MonoBehaviour
{


    public LayerMask layer;
    private Collider2D ball_collider;
    const float offset = 0.5f;
    UnityEngine.Vector2 colliderSize = new UnityEngine.Vector2(2, 1);
   // private int dir = 0;
    private float globalDir = 0;
    private Movement ball_movement;
    private bool stuned = false;
    private float stunTime = 0;
    PlayerMovement playerMovement;
    private float time = 0;
    PlayerInput playerInput;
    private Vector2 globalVec;
    private string ballState = "";
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerInput = GetComponent<PlayerInput>();
        
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
            playerMovement.Move(new Vector2(0,0));
        }
        
        
    }

    public void def(Vector2 from)
    {

        globalVec = from;
        var to = Vector2.zero;
        var angle = Vector2.Angle(from, to);
        
         
            ball_collider = Physics2D.OverlapBox(
                new Vector3(this.transform.position.x + from.x,this.transform.position.y + from.y, 0)
                , colliderSize
                , angle, layer);


            if (ball_collider)
            {
                Debug.Log("In");
                ball_movement = ball_collider.GetComponentInParent<Movement>();
                ballState = ball_movement.getState();
                var hurtLevel = ball_movement.checkState();
                
                if (ballState == "_third" || ballState == "_second")
                {   
                    pushPlayer(hurtLevel);
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

    void pushPlayer(float hurtLv)
    {
        
        Vector2 pushThere = playerInput.getAnalogPos().normalized * hurtLv;
        playerMovement.setDisableMovement(true,pushThere);
    }

    void disableArmor()
    {
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(this.transform.position.x+globalVec.x,this.transform.position.y+globalVec.y,0), colliderSize);
        Gizmos.DrawLine(this.transform.position, (Vector2)this.transform.position + globalVec);
    }
}
