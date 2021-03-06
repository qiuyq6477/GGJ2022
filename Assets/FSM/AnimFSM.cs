using System.Collections.Generic;
using UnityEngine;
using FSM;


public enum EAnimLayer
{
    Default = 0,
    Adden,
}

public abstract class AnimFSM
{
    protected AnimState[] animStates = new AnimState[(int)EnmLSActionType.COUNT];

    protected Agent Owner
    {
        get;
        private set;
    }
    protected virtual AnimState DefaultAnimState
    {
        get;
        set;
    }

    /*
    public AnimState CurrentAnimState
    {
        get;
        private set;
    }
    */

    public AnimState[] CurrentAnimStates
    {
        get;
        private set;
    }

    public EAnimLayer AnimLayer
    {
        get;
        protected set;
    }
    
    
    protected Animator animator;

    protected AnimFSM(Agent agent)
    {
        Owner = agent;
    }

    public virtual void Initialize(Animator anim)
    {
        animator = anim;
        CurrentAnimStates = new AnimState[(int)EnmLSActionType.COUNT];
    }

    public virtual void Activate()
    {
        CurrentAnimStates[(int)DefaultAnimState.type] = DefaultAnimState;
        DefaultAnimState.OnActivate(null);
    }
    
    public virtual void Deactivate()
    {
        for (int i = 0; i < CurrentAnimStates.Length; i++)
        {
            CurrentAnimStates[i] = null;
        }
    }
    
    public virtual void UpdateAnimStates()
    {
        for (int i = 0; i < CurrentAnimStates.Length; i++)
        {
            var curAnimState = CurrentAnimStates[i];
            if (curAnimState != null && !curAnimState.IsFinished())
            {
                curAnimState.CallUpdate();
            }
        }
    }
    
    public virtual void FixedUpdateAnimStates()
    {
        for (int i = 0; i < CurrentAnimStates.Length; i++)
        {
            var curAnimState = CurrentAnimStates[i];
            if (curAnimState != null && !curAnimState.IsFinished())
            {
                curAnimState.CallFixedUpdate();
            }
        }
    } 

	public void LateUpdateAnimStates()
    {
        
        for (int i = 0; i < CurrentAnimStates.Length; i++)
        {
            var curAnimState = CurrentAnimStates[i];
            if (curAnimState != null && curAnimState.IsFinished())
            {
                CurrentAnimStates[i] = null;
                curAnimState.OnDeactivate();
            }
            
            if(curAnimState != null)
            {
                curAnimState.CallLateUpdate();
            }
        }

        bool needSwitchToDefault = true;
        for (int i = 0; i < CurrentAnimStates.Length; i++)
        {
            var curAnimState = CurrentAnimStates[i];
            if(curAnimState != null)
            {
                if (!curAnimState.TryAdden(null))
                {
                    needSwitchToDefault = false;
                }
            }
        }
        
        if (needSwitchToDefault)
        {
            SwitchToDefault();
        }
    }

    private void SwitchToDefault()
    {
        CurrentAnimStates[(int) DefaultAnimState.type] = DefaultAnimState;
        DefaultAnimState.OnActivate(null);
    }

    public void Reset()
    {
        for (int i = 0; i < CurrentAnimStates.Length; i++)
        {
            var curAnimState = CurrentAnimStates[i];
            if (curAnimState != null && !curAnimState.IsFinished())
            {
                curAnimState.Reset();
            }
            CurrentAnimStates[i] = null;
        }
    }

    /// <summary>
    /// ????????????
    /// </summary>
    public void DoAction(AgentAction action)
    {
        // Debug.Log("ANIM:" + action.Type.ToString());
        //????????????????????????????????????
        var newState = animStates[(int) action.Type];
        bool isNewAdden = newState != null && newState.TryAdden(action);

        bool needExec = false;
        bool allHandlePass = true;
        //???????????????????????????????????????????????????????????????
        for (int i = 0; i < CurrentAnimStates.Length; i++)
        {
            var curAnimState = CurrentAnimStates[i];
            if (curAnimState == null) continue;

            bool isCurAdden = curAnimState.TryAdden(action);
            if (!isCurAdden) allHandlePass = false;
  
            bool interrupt = !curAnimState.HandleNewAction(action);

            bool isNetSwitch = action.CheckEnvironmentForSCInterrupt()
                               && action.Type != curAnimState.type //??????????????????????????????????????????????????????
                               && !isCurAdden 
                               && !isNewAdden;
            if (isNetSwitch)
            {
                //??????move ??????climbup
                //Debug.Log("NET:" + action.Type + " ?????? " + CurrentAnimStates[i].type + "  " + action.Process);
                curAnimState.OnDeactivate();
                CurrentAnimStates[i] = null;
                needExec = true;
            }
            else if ((!isCurAdden && !isNewAdden) || (isCurAdden && isNewAdden))
            {
                if (interrupt)
                {
                    //Debug.Log("LOC:" + action.Type + " ?????? " + CurrentAnimStates[i].type + "  " + action.Process);
                    curAnimState.OnDeactivate();
                    CurrentAnimStates[i] = null;
                    needExec = true;
                }
            }
            else if (!isCurAdden && isNewAdden)
            {
                var state = CurrentAnimStates[(int) action.Type];
                if (state == null)
                {
                    needExec = true;
                }
            }
            else if (isCurAdden && !isNewAdden)
            {
                
            }
        }

        // ????????????action
        if ((allHandlePass || needExec) && newState != null)
        {
            CurrentAnimStates[(int) action.Type] = newState;
            newState.OnActivate(action);
        }
    }
    
}
