using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Collider2D ball;

    public LayerMask layer;

    const float offset = 0.5f;

    Vector3 colliderOffset;
    Vector2 colliderSize = new Vector2(offset * 2, 3);

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //colliderOffset = transform.position;
    }

    public void Attack(int dir)
    {
        if (dir == 0) return;
        colliderOffset = new Vector3(transform.position.x + (dir * offset), transform.position.y, 0);
        ball = Physics2D.OverlapBox(colliderOffset, colliderSize, 0, layer);


        Movement ballMovement = null;
        if(ball) ballMovement = ball.GetComponentInParent<Movement>();

        if(ball != null)
        {
            ballMovement.bounce();
            ballMovement.move(ballMovement.getRb().velocity);
        }
        
        

        Debug.Log(ballMovement);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderOffset, colliderSize);
    }
}
