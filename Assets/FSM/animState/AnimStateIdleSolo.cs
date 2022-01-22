using FSM;
using UnityEngine;

public class AnimStateIdleSolo : AnimStateDefault
{
    public AnimStateIdleSolo(Animator animator, Agent agent, AnimFSM layer, EnmLSActionType type) : base(animator, agent, layer, type)
    {
    }
    
    protected override float GetVal(int idx)
    {
        return base.GetVal(idx);
    }

    public override bool HandleNewAction(AgentAction action)
    {
        
        if (action.Type == EnmLSActionType.IDLE)
        {
            return true;
        }
        return false;
    }

    public override void OnActivate(AgentAction action)
    {
        Debug.Log("idle Enter");
        base.OnActivate(action);
    }

    protected override void FixedUpdate()
    {
    }

    public override void OnDeactivate()
    {
        Debug.Log("idle Exit");
        base.OnDeactivate();
    }

}