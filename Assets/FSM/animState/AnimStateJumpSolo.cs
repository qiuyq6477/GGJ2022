using System;
using FSM;
using UnityEngine;

public class AnimStateJumpSolo : AnimStateDefault
{
    public AnimStateJumpSolo(Animator animator, Agent agent, AnimFSM layer, EnmLSActionType type) : base(animator,
        agent,
        layer, type)
    {
    }


    public override bool HandleNewAction(AgentAction action)
    {
        if (Owner.BlackBoard.OnGround)
        {
            if (action.Type == EnmLSActionType.MOVE ||
                action.Type == EnmLSActionType.IDLE)
            {
                return false;
            }
        }

        return true;
    }

    public override void OnActivate(AgentAction action)
    {
        base.OnActivate(action);
        Owner.Rigidbody.velocity = new Vector2(Owner.Rigidbody.velocity.x, 6);
        Debug.Log("jump Enter");
    }


    /// <summary>
    /// 动画过程中
    /// </summary>
    protected override void FixedUpdate()
    {
        Debug.Log("move update");
        if (Math.Abs(Owner.BlackBoard.InputHorizontal) > 0)
        {
            transform.position += Vector3.right * Owner.BlackBoard.InputHorizontal * Time.deltaTime;
        }

        Owner.Rigidbody.velocity =
            new Vector2(Owner.Rigidbody.velocity.x, Mathf.Clamp(Owner.Rigidbody.velocity.y, -6f, 6f));

        if (Owner.BlackBoard.OnGround)
        {
            SetFinished(true);
        }
    }


    public override void OnDeactivate()
    {
        Debug.Log("move exit");
        base.OnDeactivate();
    }
}