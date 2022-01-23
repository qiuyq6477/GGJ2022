using System;
using FSM;
using UnityEngine;

public class AnimStateMoveSolo : AnimStateDefault
{
    public AnimStateMoveSolo(Animator animator, Agent agent, AnimFSM layer, EnmLSActionType type) : base(animator,
        agent,
        layer, type)
    {
    }


    public override bool HandleNewAction(AgentAction action)
    {
        if (action.Type == EnmLSActionType.MOVE ||
            action.Type == EnmLSActionType.IDLE)
        {
            return true;
        }


        return false;
    }

    public override void OnActivate(AgentAction action)
    {
        base.OnActivate(action);
        Debug.Log("move Enter");
    }


    /// <summary>
    /// 动画过程中
    /// </summary>
    protected override void FixedUpdate()
    {
        if (Math.Abs(Owner.BlackBoard.InputHorizontal) > 0)
        {
            transform.position += Vector3.right * Owner.BlackBoard.InputHorizontal * Time.deltaTime * 5;
        }
        else
        {
            SetFinished(true);
        }
    }


    public override void OnDeactivate()
    {
        base.OnDeactivate();
    }
}