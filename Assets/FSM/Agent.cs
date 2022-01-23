using System;
using System.Collections.Generic;
using System.Text;
using FSM;
using UnityEngine;


/// <summary>
/// 主角代理
/// </summary>
public class Agent : MonoBehaviour
{
    /// <summary>
    /// 持有者
    /// </summary>
    [NonSerialized] public Agent Owner;

    [NonSerialized] public AgentBlackBoard BlackBoard = new AgentBlackBoard();
    [NonSerialized] public Rigidbody2D Rigidbody;
    [NonSerialized] public GrabComponent grabComponent;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        grabComponent = GetComponent<GrabComponent>();
    }


    /// <summary>
    /// 玩家当前的行为
    /// </summary>
    [NonSerialized] public EnmLSActionType _ActionType = EnmLSActionType.IDLE;

    public EnmLSActionType ActionType
    {
        get { return _ActionType; }
        set
        {
            if (_ActionType != value)
            {
                // if (PlayerId == GameKernal.Instance.BlackBoard.MyUid)
                // {
                //     Debug.Log("ACTION: " + _ActionType + " -> " + value);
                // }
                LastActionType = _ActionType;
                _ActionType = value;
            }
        }
    }

    /// <summary>
    /// 记录上一次的action，以便在行为转换后做一些平滑操作
    /// </summary>
    [NonSerialized] public EnmLSActionType LastActionType = EnmLSActionType.IDLE;

    /// <summary>
    /// 玩家当前的行为叠加层
    /// </summary>
    [NonSerialized] public int ActionAddenType = 0;

    public void ActionAddenTypeToString(StringBuilder buffer)
    {
        buffer.Clear();
        for (int i = 0; i < (int) EnmLSActionType.COUNT; i++)
        {
            if ((ActionAddenType & (1 << i)) != 0)
            {
                if (buffer.Length > 0)
                {
                    buffer.Append("|");
                }

                buffer.Append(((EnmLSActionType) i).ToString());
            }
        }
    }


    //--------------------------------//--------------------------------//--------------------------------

    public AgentActionHandler Handler;

    /// <summary>
    /// 三个类型的优先级
    /// </summary>
    private List<AgentActionHandler>[] ActionHandlers = new List<AgentActionHandler>[]
    {
        new List<AgentActionHandler>(),
        new List<AgentActionHandler>(),
        new List<AgentActionHandler>()
    };

    private List<AgentAction> activeActions = new List<AgentAction>();
    public const int PRIORITY_HIGH = 0;
    public const int PRIORITY_MID = 1;
    public const int PRIORITY_LOW = 2;

    /// <summary>
    /// 添加action回调
    /// </summary>
    public void AddHandler(AgentActionHandler handler, int priority = PRIORITY_MID)
    {
#if UNITY_EDITOR
        for (int i = 0; i < ActionHandlers.Length; i++)
        {
            if (ActionHandlers[i].Contains(handler))
            {
                UnityEngine.Debug.LogError("-------------------ActionHandlers.Contains(handler)");
                return;
            }
        }
#endif
        ActionHandlers[priority].Add(handler);
    }

    /// <summary>
    /// 移除action回调
    /// </summary>
    public void RemoveHandler(AgentActionHandler handler)
    {
        for (int i = 0; i < ActionHandlers.Length; i++)
        {
            int index = ActionHandlers[i].IndexOf(handler);
            if (index != -1)
            {
                ActionHandlers[i][index] = null;
            }
        }
    }


    /// <summary>
    /// 按照优先级执行
    /// </summary>
    public void AddAction(AgentAction action)
    {
        action.Owner = Owner;
        activeActions.Add(action);


        if (!action.CheckEnvironmentForExec())
        {
            action.SetFailed();
            return;
        }

        actionQueue.Enqueue(action);
        if (!isLockQuene)
        {
            PeekAction();
        }
    }

    private bool isLockQuene = false;

    //待执行的Action队列
    private Queue<AgentAction> actionQueue = new Queue<AgentAction>();

    private void PeekAction()
    {
        isLockQuene = true;
        var action = actionQueue.Dequeue();
        for (int pri = 0; pri < ActionHandlers.Length; pri++)
        {
            for (int i = 0; i < ActionHandlers[pri].Count; i++)
            {
                AgentActionHandler actionHandler = ActionHandlers[pri][i];
                if (actionHandler != null)
                {
                    actionHandler.Invoke(action);
                }
            }
        }

        if (actionQueue.Count > 0)
        {
            PeekAction();
        }

        isLockQuene = false;
    }

    private List<AgentAction> needCollectActions = new List<AgentAction>();

    public void PostUpdate()
    {
        //相当于Action都需要等待一帧才真实回收
        for (int i = 0; i < needCollectActions.Count; i++)
        {
            var action = needCollectActions[i];
            AgentActionFactory.Collect(action);
        }

        needCollectActions.Clear();


        for (int i = activeActions.Count - 1; i >= 0; i--)
        {
            var action = activeActions[i];
            if (action.IsActiveOrRun())
            {
                continue;
            }

            needCollectActions.Add(action);
            activeActions.RemoveAt(i);
        }

        for (int i = 0; i < ActionHandlers.Length; i++)
        {
            for (int j = ActionHandlers[i].Count - 1; j >= 0; j--)
            {
                if (ActionHandlers[i][j] == null)
                {
                    ActionHandlers[i].RemoveAt(j);
                }
            }
        }
    }

    private void LateUpdate()
    {
        PostUpdate();
    }

    public AgentProfile Profile;
}