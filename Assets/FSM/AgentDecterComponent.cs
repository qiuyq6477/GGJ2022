using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于主动检测与外部的交互 （当前是否脚踩在地面等等）
public class AgentDecterComponent : MonoBehaviour
{
    private Agent owner;

    // Start is called before the first frame update
    void Start()
    {
        owner = GetComponent<Agent>();
    }

    // Update is called once per frame
    void Update()
    {
        owner.BlackBoard.OnGround = false;

        RaycastHit2D[] hit =
            Physics2D.RaycastAll(transform.position, Vector3.down, 0.5f, 1 << LayerMask.NameToLayer("Floor"));

        if (hit.Length > 0)
        {
            Debug.Log("在地面");
            owner.BlackBoard.OnGround = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //如果是梯子
        // if (other)
        // {
        //     owner.BlackBoard.OnLadder = true;
        // }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        //如果是梯子
        // if (other)
        // {
        //     owner.BlackBoard.OnLadder = false;
        // }
    }
}