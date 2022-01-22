using System;
using FSM;
using UnityEngine;

public class AnimStateClimbSolo : AnimStateDefault
{
    public AnimStateClimbSolo(Animator animator, Agent agent, AnimFSM layer, EnmLSActionType type) : base(animator,
        agent,
        layer, type)
    {
    }


    public override bool HandleNewAction(AgentAction action)
    {
        if (action.Type == EnmLSActionType.JUMP)
        {
            return false;
        }


        return false;
    }

    public override void OnActivate(AgentAction action)
    {
        base.OnActivate(action);
        rigidbody.useGravity = false;
    }


    /// <summary>
    /// 动画过程中
    /// </summary>
    protected override void FixedUpdate()
    {
        if (Owner.BlackBoard.OnLadder)
        {
            transform.position += Owner.BlackBoard.InputDir * (Time.deltaTime * 1);
        }
        else
        {
            SetFinished(true);
        }
    }


    public override void OnDeactivate()
    {
        base.OnDeactivate();
        rigidbody.useGravity = true;
    }
}