using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerMovement : MonoBehaviour
{

    Vector2 movementDirection;

    [SerializeField]
    [Range(0.05f, 1)]
    float movementSpeed;

    private float fraction;
    Animator animator;
    
    //push player
    public bool pushPlayer = false;
    private Vector2 globalPushThere;
    private Vector2 lastPos;
    

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (!pushPlayer)
        {
            transform.Translate(movementDirection * movementSpeed);
            //Debug.Log(movementDirection.x * movementSpeed);
            animator.SetFloat("move", Mathf.Abs(movementDirection.x * movementSpeed * 10));
        }

        else if((Vector2)transform.position == globalPushThere)
        {
            setDisableMovement(false , Vector2.zero);
        }

        else
        {
//            Vector2 dist = lastPos - (Vector2) this.transform.position;
//            float fraction = dist.magnitude/((Vector2)this.transform.position - globalPushThere).magnitude;

            if (Time.time - fraction <= 1)
            {
                transform.position = Vector2.Lerp(lastPos, globalPushThere, Time.time - fraction);
            }

            else
            {
                this.pushPlayer = false;
            }

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.transform.position,globalPushThere);
    }

    public void Move(Vector2 dir)
    {
        movementDirection = dir;
    }


    public void setDisableMovement(bool yes,Vector2 pushThere)
    {
        this.lastPos = (Vector2)this.transform.position;
        this.globalPushThere = pushThere;
        this.pushPlayer = yes;
        this.fraction = Time.time;
    }
}
