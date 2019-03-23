using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    float leftStickOffset = 0.1f;

    [SerializeField]
    PlayerMovement playerMovement;
    Player player;
    int playerNum;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();

        if(player != null)
        {
            playerNum = player.playerNum;
        }
        else
        {
            Debug.Log("There is no Player object assigned");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xMovementAxis = Input.GetAxis("Joy" + playerNum + "X");
        float yMovementAxis = Input.GetAxis("Joy" + playerNum + "Y");

        
        if (Mathf.Abs(xMovementAxis) < leftStickOffset) xMovementAxis = 0;
        if (Mathf.Abs(yMovementAxis) < leftStickOffset) yMovementAxis = 0;

        Vector2 dir = new Vector2(xMovementAxis, -yMovementAxis);

        playerMovement.Move(dir);
        

        
    }
}
