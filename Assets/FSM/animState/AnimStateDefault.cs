using FSM;
using UnityEngine;

public abstract class AnimStateDefault : AnimState
{


    public AnimStateDefault(Animator animator, Agent agent, AnimFSM layer, EnmLSActionType type) : base(animator, agent, layer, type)
    {

    }

    public override void CallFixedUpdate()
    {
        base.CallFixedUpdate();
    }

    private EnmLSActionType debugType;
    public override void OnActivate(AgentAction ac)
    {
        if (ac != null && ac.Layer != EAnimLayer.Default)
        {
            Debug.LogError("请配置Action为EAnimLayer.Default");
        }
        debugType = type;
        // Debug.Log("进入：" + type);
        base.OnActivate(ac);
        Owner.ActionType = type;
        
    }

    
    
    public override void OnDeactivate()
    {
        base.OnDeactivate();
        // Debug.Log("退出：" + type);
        if (debugType != type)
        {
            Debug.LogError("行为状态切换异常");
        }
    }

    public override bool TryAdden(AgentAction action)
    {
        return false;
    }

}