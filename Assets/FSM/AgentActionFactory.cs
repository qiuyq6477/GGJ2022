using System.Collections.Generic;
using FSM;
using UnityEngine;

public delegate void AgentActionHandler(AgentAction a);

public static class AgentActionFactory
{
    static Queue<AgentAction>[] unusedActions = new Queue<AgentAction>[(int)EnmLSActionType.COUNT];

    static AgentActionFactory()
    {
        for (EnmLSActionType i = 0; i < EnmLSActionType.COUNT; i++)
        {
            unusedActions[(int)i] = new Queue<AgentAction>();
        }
    }

    public static AgentAction Create(EnmLSActionType type)
    {
        int index = (int)type;
        AgentAction a = null;
        if (unusedActions[index].Count > 0)
        {
            a = unusedActions[index].Dequeue();
        }
        else
        {
            switch (type)
            {
                case EnmLSActionType.IDLE:
                    a = new AgentActionIdle();
                    break;
                case EnmLSActionType.MOVE:
                    a = new AgentActionMove();
                    break;
                case EnmLSActionType.JUMP:
                    a = new AgentActionJump();
                    break;
                case EnmLSActionType.CLIMB:
                    a = new AgentActionClimb();
                    break;
                default:
                    Debug.LogError("no AgentAction to create, type: " + type);
                    return null;
            }
        }
        a.Reset();
        a.SetActive();
        return a;
    }

    public static void Collect(AgentAction action)
    {
        action.SetUnused();
        unusedActions[(int)action.Type].Enqueue(action);
    }


    /// <summary>
    /// 如果遇到需要频繁注册的action，则使用这个
    /// </summary>
    static Dictionary<int, AgentActionHandler> handlerMap = new Dictionary<int, AgentActionHandler>();
    public static void RegisterActionHandler(int id, AgentActionHandler actionHandler)
    {
        handlerMap[id] = actionHandler;
    }

    public static AgentActionHandler GetActionHandler(int id)
    {
        return handlerMap[id];
    }

    public static void Dispose()
    {
        for (EnmLSActionType i = 0; i < EnmLSActionType.COUNT; i++)
        {
            unusedActions[(int)i].Clear();
        }
        handlerMap.Clear();
    }
}
