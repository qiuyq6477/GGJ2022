using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public class inpuas : MonoBehaviour
{
    private Agent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.BlackBoard.InputHorizontal = Input.GetAxis("Horizontal");
        agent.BlackBoard.InputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Mathf.Abs(agent.BlackBoard.InputHorizontal) > 0)
        {
            var a = AgentActionFactory.Create(EnmLSActionType.MOVE);
            agent.AddAction(a);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            var a = AgentActionFactory.Create(EnmLSActionType.JUMP);
            agent.AddAction(a);
        }
    }
}