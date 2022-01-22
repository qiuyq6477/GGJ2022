using System.Collections.Generic;
using FSM;
using UnityEngine;


public class AgentAction
{
    public enum State
    {
        ACTIVE,
        RUN,
        SUCCESS,
        FAILED,
        UNUSED,
    }
    
    
    public EnmLSActionType Type;

    //todo 非通用部分
    public Agent Owner;
    
    /// <summary>
    /// Action执行流程标记
    /// </summary>
    public int Process = 0;
    
    public State Status = State.ACTIVE;

    public EAnimLayer Layer;
    public AgentAction(EnmLSActionType type)
    {
        Type = type;
        Layer = EAnimLayer.Default;
    }


    /// <summary>
    /// 处于活跃状态
    /// </summary>
    /// <returns></returns>
    public bool IsActiveOrRun()
    {
        bool flag = Status == State.ACTIVE || Status == State.RUN;
        return flag;
    }

    public bool IsActive()
    {
        return Status == State.ACTIVE;
    }
    
    //处于被动画控制器捕获到
    public bool IsRun()
    {
        return Status == State.RUN;
    }

    public bool IsFailed()
    {
        return Status == State.FAILED;
    }

    public bool IsSuccess()
    {
        return Status == State.SUCCESS;
    }

    public bool IsUnused()
    {
        return Status == State.UNUSED;
    }
    
    public void SetSuccess()
    {      
        Status = State.SUCCESS;
    }
    
    public void SetFailed()
    {       
        Status = State.FAILED;
    }

    public void SetUnused()
    {
        Status = State.UNUSED;
    }

    public void SetActive()
    {
        Status = State.ACTIVE;
    }
    
    public void SetRun()
    {
        Status = State.RUN;
    }



    public virtual void Reset()
    {
        Process = 0;
    }

    public override string ToString()
    {
        return Type + " " + Status.ToString();
    }

    
    public AgentAction()
    {
    }


    /// <summary>
    /// 当前行为是否可以执行
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckEnvironmentForExec()
    {
        return true;
    }

    /// <summary>
    /// 当前行为是否具备强打断性质 todo 暂时保留 以后方便修改使用
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckEnvironmentForSCInterrupt()
    {
        // if ((Process & ((int)EProcess.S)) != 0 && (Process & ((int)EProcess.RC)) != 0)
        // {
        //     //服务器消息打断网络客户端
        //     return true;
        // }
        //
        // if ((Process & ((int)EProcess.LC)) != 0 && (Process & ((int)EProcess.S)) != 0)
        // {
        //     //本地消息打断服务器
        //     return true;
        // }
        return false;
    }
    
    /// <summary>
    /// 描述当前的消息，已经经过对应阶段的处理
    /// </summary>
    public enum EProcess : int
    {
        LC = 0b_0001,    //本地客户端
        S  = 0b_0010,    //服务器
        RC = 0b_0100,    //网络客户端
    }
    

}