using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于主动检测与外部的交互 （当前是否脚踩在地面等等）
public class AgentDecterComponent : MonoBehaviour
{
    private Agent owner;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        owner = GetComponent<Agent>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        owner.BlackBoard.OnGround = false;
        RaycastHit[] hit = Physics.RaycastAll(transform.position, Vector2.down, 0.2f);
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.transform == transform)
                {
                    continue;
                }

                // if (hit[i].collider 是地面)
                // {

                owner.BlackBoard.OnGround = true;
                break;


                // }
            }
        }
    }
}