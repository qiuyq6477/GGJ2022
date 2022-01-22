//
// ===========================================
//  AgentProfile.cs
//
//  Author:
//       YI <yyii@live.cn>
//  Created On Monday, 12 October 2020 11:54:24
//
// ===========================================
//

using System;
using System.Collections.Generic;
using FSM;
using UnityEngine;

public enum RigType
{
}


/// <summary>
/// 用户预设角色数据,支持运行时修改，并生效(debug)
/// </summary>
[CreateAssetMenu]
public class AgentProfile : ScriptableObject
{
    public abstract class IConf
    {
        [HideInInspector]
        public bool isAdden = false;
    }

    [Serializable]
    public class RigConf
    {
        public RigType Type;
        public float val;
        public float smoothTime;
    }

    [Serializable]
    public class CommonConf
    {
        public string key;
        public float val;
    }
    
    [Serializable]
    public class CurveConf
    {
        public string key;
        public AnimationCurve curve;
    }
    
    [Serializable]
    public class ChestJointConf : IConf
    {
        public float positionSpring;
        public float positionDamper;
        public float smoothTime;
        public bool isEnable = true;
    }

    
    [Serializable]
    public class FrictionConf : IConf
    {
        public float dynamicFriction = 0.3f;
        public float staticFriction = 0.3f;
        public float bounciness = 0f;
        public PhysicMaterialCombine frictionCombine = PhysicMaterialCombine.Average;
        public PhysicMaterialCombine bounceCombine = PhysicMaterialCombine.Average;
        public bool isEnable = true;
    }
    
    /// <summary>
    /// 玩家在不同的状态下，同步参数可以发生修改
    /// </summary>
    [Serializable]
    public class SyncConf : IConf
    {
        /// <summary>
        /// 原则上，对于所有ping小于interpolationBackTime的对象，该对象将出现在所有屏幕的相同位置。
        /// 增加这个值，将使内插更可能被使用 这意味着同步的位置将更有可能是所有者所在的实际位置。
        /// 减少这个值，使外推更有可能被使用 这将增加响应性，但如果延迟峰值持续时间超过interpolationBackTime，位置将不太正确。
        /// </summary>
        public float interpolationBackTime = .1f;

        public float sendPositionThreshold = .1f;

        public float sendRotationThreshold = 3f;

        // public float sendScaleThreshold = 0.0f;

        public float sendVelocityThreshold = 0f;

        public float sendAngularVelocityThreshold = 0f;

        public float receivedPositionThreshold = 0.0f;

        public float receivedRotationThreshold = 0.0f;

        public float snapPositionThreshold = 0;

        public float snapRotationThreshold = 0;

        // public float snapScaleThreshold = 0;

        [Range(0.1f, 1)]
        public float positionLerpSpeed = .85f;

        [Range(0.1f, 1)]
        public float rotationLerpSpeed = .85f;

        // [Range(0, 1)]
        // public float scaleLerpSpeed = .85f;

        [Range(0, 5)]
        public float timeCorrectionSpeed = .1f;

        public float snapTimeThreshold = 3.0f;

        // public SyncMode syncPosition = SyncMode.XYZ;
        //
        // public SyncMode syncRotation = SyncMode.XYZ;
        //
        // //public SyncMode syncScale = SyncMode.NONE;
        //
        // public SyncMode syncVelocity = SyncMode.XYZ;
        //
        // public SyncMode syncAngularVelocity = SyncMode.NONE;

        // public bool automaticallyResetTime = false;

        public bool isSmoothingAuthorityChanges = false;

        public float sendRate = 17;

        public bool setVelocityInsteadOfPositionOnNonOwners = true;
        
        public float maxPositionDifferenceForVelocitySyncing = 10;

        public bool __setVelocityAndPosition = false;
        
        public bool __dontSyncPosition = false;
    }
    
    
    [Serializable]
    public class AnimStateConf
    {
        public string name;
        
        /// <summary>
        /// 通用参数
        /// </summary>
        public List<CommonConf> CommonConfs;
        
        /// <summary>
        /// 通用参数
        /// </summary>
        public List<CurveConf> CurveConfs;
        
        /// <summary>
        /// idle情况下的物理融合
        /// </summary>
        public List<RigConf> RigConf;

        /// <summary>
        /// idle情况下的中心关节参数调整
        /// </summary>
        public ChestJointConf ChestJointConf;

        /// <summary>
        /// 摩擦阻力config
        /// </summary>
        public FrictionConf FrictionConf;

        /// <summary>
        /// 同步性参数config
        /// </summary>
        public SyncConf SyncConf;

        /// <summary>
        /// 内部通用查询方式
        /// </summary>
        private Dictionary<string, CommonConf> _confs = new Dictionary<string, CommonConf>();
        public float this[string key]
        {
            get
            {
                if (_confs.Count == 0 && CommonConfs != null && CommonConfs.Count > 0)
                {
                    foreach (var conf in CommonConfs)
                    {
                        string kstr = conf.key.ToLower();
                        _confs[kstr] = conf;
                    }
                }
                CommonConf res;
                if (_confs.TryGetValue(key, out res))
                {
                    return res.val;
                }
                return 0;
            }
        }
    }
    
    [SerializeField]
    public float BufferOnGroundFD = 5;

    [SerializeField]
    public float MaxCollapseImpluseForce = 300;
    
    [SerializeField]
    public float CollapseImpluseFactor = 0.3f;
    
    [SerializeField]
    public float MaxCollapseAngle = 60;
    
    /// <summary>
    /// 当玩家被拉的时候的移动系数
    /// </summary>
    [SerializeField]
    public float PullMotionFactor = 0.1f;
    
    /// <summary>
    /// 当玩家被拉的时候的移动系数
    /// </summary>
    [SerializeField]
    public float MaxLostControlTime = 8f;

    [SerializeField]
    public AnimStateConf[] Confs = new AnimStateConf[(int)EnmLSActionType.COUNT];
}