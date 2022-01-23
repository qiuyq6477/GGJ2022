using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class inpuas : MonoBehaviour
{
    private Agent agent;

    //跳跃临时处理
    private int recordJumpFrame;

    private bool isRecordJump;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.BlackBoard.InputHorizontal = Input.GetAxis("Horizontal");
        agent.BlackBoard.InputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Mathf.Abs(agent.BlackBoard.InputHorizontal) > 0)
        {
            var a = AgentActionFactory.Create(EnmLSActionType.MOVE);
            agent.AddAction(a);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRecordJump = true;
            recordJumpFrame = 5;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            agent.grabComponent.TryGrab();
        }


        if (agent.BlackBoard.OnLadder && Input.GetKeyDown(KeyCode.W))
        {
            var a = AgentActionFactory.Create(EnmLSActionType.CLIMB);
            agent.AddAction(a);
        }
    }

    private void FixedUpdate()
    {
        if (isRecordJump)
        {
            recordJumpFrame--;
            if (agent.BlackBoard.OnGround && agent.ActionType != EnmLSActionType.JUMP)
            {
                var a = AgentActionFactory.Create(EnmLSActionType.JUMP);
                agent.AddAction(a);
                isRecordJump = false;
            }

            if (recordJumpFrame <= 0)
            {
                isRecordJump = false;
            }
        }
    }
}