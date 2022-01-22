//
// ===========================================
//  AnimState
//
//  Author:
//       YI <yyii@live.cn>
//  Created On 2020/8/20 2:13 下午
//
// ===========================================
//

using FSM;
using UnityEngine;

public abstract class AnimState
{
    public EnmLSActionType type;
    protected Agent Owner
    {
        get;
        private set;
    }

    protected bool isFinished = true;
    protected Transform transform;
    protected Animator animator;
    protected Rigidbody rigidbody;
    protected Rigidbody chestRigidbody;
    protected AnimFSM layer;
    protected AgentProfile.AnimStateConf conf;
    protected AgentAction thisAction;

    /// <summary>
    /// 获取某个通用的值
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected virtual float GetVal(int idx)
    {
        return conf.CommonConfs[idx].val;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    protected virtual AnimationCurve GetCurve(int idx)
    {
        return conf.CurveConfs[idx].curve;
    }

    public AnimState(Animator animator, Agent agent, AnimFSM layer, EnmLSActionType type)
    {
        Owner = agent;
        this.layer = layer;
        transform = agent.transform;
        //rigidbody = agent.GetComponent<Rigidbody>();
        this.type = type;
        this.animator = animator;
        conf = agent.Profile.Confs[(int) type];

    }

    public virtual void OnActivate(AgentAction ac)
    {
        thisAction = ac;
        if (thisAction != null)
        {
            thisAction.SetRun();
        }
        isFinished = false;
    }

    public virtual void OnDeactivate()
    {
        isFinished = true;
        if (thisAction != null && thisAction.IsActiveOrRun())
        {
            thisAction.SetSuccess();
        }
        thisAction = null;
    }

    public virtual void Reset()
    {
        isFinished = true;
    }

    /// <summary>
    /// 正在当前这个状态的时候，又进来一个action
    /// 内部条件判断与准备，返回执行成功与否
    /// false新action打断旧的，反之不打断
    /// </summary>
    public virtual bool HandleNewAction(AgentAction action)
    {
        return false;
    }

    public virtual void CallFixedUpdate()
    {
        //Owner.BlackBoard.CurrTheoryVelocity = rigidbody.velocity;
        FixedUpdate();
    }

    public virtual void CallUpdate()
    {
        Update();
    }

    public void CallLateUpdate()
    {
        LateUpdate();
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Update()
    {

    }

    protected virtual void LateUpdate()
    {
    }


    public virtual bool IsFinished()
    {
        return isFinished;
    }

    public virtual void SetFinished(bool finished)
    {
        isFinished = finished;
    }


    public abstract bool TryAdden(AgentAction action);


}
