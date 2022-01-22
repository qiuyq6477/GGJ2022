//
// ===========================================
//  AnimComponent.cs
//
//  Author:
//       YI <yyii@live.cn>
//  Created On Thursday, 20 August 2020 14:21:06
//
// ===========================================
//


using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 动画状态控制器
/// 2020.8.22之后摔倒才进入物理学
/// </summary>
public class AnimComponent : MonoBehaviour
{
    protected Agent Owner
    {
        get;
        private set;
    }

    protected AnimFSM FSM
    {
        get;
        private set;
    }

    protected Animator animator;

   
    private void Awake()
    {
        Owner = GetComponent<Agent>();
        //animator = GetComponentInChildren<Animator> ();
    }

    /// <summary>
    /// 激活
    /// </summary>
    private void OnEnable()
    {
        if (FSM == null)
        {
            FSM = new AnimFSMDefaultSolo(Owner);
            FSM.Initialize(animator);
        }
        FSM.Activate();
        Owner.AddHandler(HandleAction);
    }

    /// <summary>
    /// 反激活
    /// </summary>
    private void OnDisable()
    {
        if(FSM != null)
        {
            FSM.Reset();
            FSM.Deactivate();
        }
        Owner.RemoveHandler(HandleAction);
    }


    private void HandleAction(AgentAction action)
    {
        if (!enabled)
        {
            return;
        }
        if (!action.IsActive())
        {
            return;
        }

        FSM.DoAction(action);
        //没有人接管这个Action
        if (action.IsActive())
        {
            action.SetFailed();
        }
    }

    private void FixedUpdate()
    {
        FSM.FixedUpdateAnimStates();
    }
    
    private void Update()
    {
        FSM.UpdateAnimStates();
    }

    private void LateUpdate()
    {
        FSM.LateUpdateAnimStates();
    }
    
}