using FSM;
using UnityEngine;

public class AnimFSMDefaultSolo : AnimFSM
{
    public AnimFSMDefaultSolo(Agent agent) : base(agent)
    {
        AnimLayer = EAnimLayer.Default;
    }

    public override void Initialize(Animator anim)
    {
        base.Initialize(anim);
        animStates[(int) EnmLSActionType.IDLE] = new AnimStateIdleSolo(anim, Owner, this, EnmLSActionType.IDLE);
        animStates[(int) EnmLSActionType.MOVE] = new AnimStateMoveSolo(anim, Owner, this, EnmLSActionType.MOVE);
        animStates[(int) EnmLSActionType.JUMP] = new AnimStateJumpSolo(anim, Owner, this, EnmLSActionType.JUMP);
        animStates[(int) EnmLSActionType.CLIMB] = new AnimStateClimbSolo(anim, Owner, this, EnmLSActionType.CLIMB);

        DefaultAnimState = animStates[(int) EnmLSActionType.IDLE];
    }

    /*
    protected override AnimState DefaultAnimState
    {
        get
        {
            if (Owner.BlackBoard.InputDir.sqrMagnitude > 0.001f)
            {
                //以补发premove的方式来驱动比较合理？
                return animStates[(int) cs.EnmLSActionType.MOVE];
            }
            return animStates[(int) cs.EnmLSActionType.IDLE];
        }
    }
    */
}