using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    float leftStickOffset = 0.1f;
    [SerializeField]
    float rightStickOffset = 0.1f;

    [SerializeField]
    PlayerMovement playerMovement;
    Player player;
    int playerNum;

    PlayerAttack playerAttack;
    PlayerDef playerDef;

    bool attacked = false;
    bool deffed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();

        playerAttack = GetComponent<PlayerAttack>();
        playerDef = GetComponent<PlayerDef>();

        if(player != null)
        {
            playerNum = player.playerNum;
        }
        else
        {
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xMovementAxis = Input.GetAxis("Joy" + playerNum + "X");
        float yMovementAxis = Input.GetAxis("Joy" + playerNum + "Y");

        
        if (Mathf.Abs(xMovementAxis) < leftStickOffset) xMovementAxis = 0;
        if (Mathf.Abs(yMovementAxis) < leftStickOffset) yMovementAxis = 0;

        Vector2 dir = new Vector2(xMovementAxis, -yMovementAxis);

        playerMovement.Move(dir);


        if (Input.GetAxis("Joy" + playerNum + "AttackDash") > 0.2f)
        {
            deffed = false;
            if (Input.GetAxis("Joy" + playerNum + "AttackDash") > 0 && !attacked)
            {
                float aimDir = Input.GetAxis("Joy" + playerNum + "AimX");
                int aimInt = 0;

                if (aimDir - rightStickOffset > 0)
                {
                    attacked = true;
                    aimInt = 1;
                }
                else if (aimDir + rightStickOffset < 0)
                {
                    attacked = true;
                    aimInt = -1;
                }
                playerAttack.Attack(aimInt);

            }
            
        }
        else if (Input.GetAxis("Joy" + playerNum + "AttackDash") < -0.2f)
        {
            
            attacked = false;

            if (Input.GetAxis("Joy" + playerNum + "AttackDash") < 0 && !deffed)
            {

                
                float aimDir = Input.GetAxis("Joy" + playerNum + "AimX");
                int aimInt = 0;

                if (aimDir - rightStickOffset > 0)
                {
                    deffed = true;
                    aimInt = 1;
                }
                else if (aimDir + rightStickOffset < 0)
                {
                    deffed = true;
                    aimInt = -1;
                }
                Debug.Log("exec player def");
                playerDef.def(aimInt);
            }
        }
        else
        {
            attacked = false;
            deffed = false;
        }



    }
}
