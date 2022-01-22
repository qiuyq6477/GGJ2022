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
        if (action.Type == EnmLSActionType.CLIMB)
        {
            return false;
        }

        return true;
    }

    public override void OnActivate(AgentAction action)
    {
        base.OnActivate(action);
        Owner.Rigidbody.velocity = new Vector2(Owner.Rigidbody.velocity.x, GetVal(0));
        Debug.Log("jump Enter");
    }


    /// <summary>
    /// 动画过程中
    /// </summary>
    protected override void FixedUpdate()
    {
        if (Math.Abs(Owner.BlackBoard.InputHorizontal) > 0)
        {
            transform.position += Vector3.right * Owner.BlackBoard.InputHorizontal * Time.deltaTime;
        }

        Owner.Rigidbody.velocity =
            new Vector2(Owner.Rigidbody.velocity.x, Mathf.Clamp(Owner.Rigidbody.velocity.y, -13f, 13f));

        if (Owner.BlackBoard.OnGround && Owner.Rigidbody.velocity.y <= 0)
        {
            SetFinished(true);
        }
    }


    public override void OnDeactivate()
    {
        Debug.Log("jump Exit");
        base.OnDeactivate();
    }
}