using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    public LayerMask layer;

    Collider2D ball;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ball = Physics2D.OverlapBox(transform.position, transform.localScale, 0, layer);
        if(ball) ball.GetComponentInParent<Movement>().bounce();
    }

   
}
