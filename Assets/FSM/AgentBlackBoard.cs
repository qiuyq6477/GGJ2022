//
// ===========================================
//  AgentBlackBoard.cs
//
//  Author:
//       YI <yyii@live.cn>
//  Created On Thursday, 20 August 2020 14:30:12
//
// ===========================================
//

using System;
using System.Collections.Generic;
using System.Text;
using FSM;
using UnityEngine;


public class AgentBlackBoard : MonoBehaviour
{
    public Vector3 InputDir = Vector3.zero;
    public float InputHorizontal = 0;
    //是否在地面
    public bool OnGround;
    
    //是否在梯子上
    public bool OnLadder;

}