using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ARPGDemo.Character;
using UnityEngine.AI;
using AI.Perception;
namespace AI.FSM
{
    /// <summary>
    /// 状态机
    /// </summary>
    public class BaseFSM:MonoBehaviour
    {
        #region  1.0
        private ARPGDemo.Character.CharacterAnimation chAnim;
        private FSMState currentState;
        private FSMState defaultState;
        private List<FSMState> states = new List<FSMState>();
        
        
        public FSMStateID currentStateid;
        public FSMStateID defaultStateid;
        #endregion

        #region  2.0
        //AI配置文件 状态转换表
        public string aiConfigFile = "AI_01.txt";
        public AnimationParams animParams;
        public CharacterStatus chState;
        #endregion  

        #region  3.0
        //关注的目标标签tag
        public string[] targetTags = { "Player" };
        //关注的目标物体
        public Transform targetObj = null;
        //视距
        public float sightDistance = 10;
        //追逐速度
        public float moveSpeed = 5;
        //网格寻路
        private NavMeshAgent navAgent;
        #endregion

        # region 4.0
        private CharacterSkillSystem chSkillSys;

        public void AutoUseSkill()
        {
            chSkillSys.UseRandomSkill();
        }

        #endregion

        #region 5.0 定义寻路需要的字段和方法
        //路点数组
        public Transform[] wayPoints;
        //巡逻到达的距离
        public float patrolArrivalDistance = 0.3f;
        //速度
        public float walkSpeed = 2;
        //是否完成
        public bool isPatrolComplete = false;
        //巡逻方式
        public PatrolMode patrolMode = PatrolMode.Once;
        #endregion

        #region 6.0引入智能感知
        private SightSensor sightSensor;//1 引入 2注册事件


        #endregion

        private void Awake()
        {
            ConfigFSM();
        }

        public void Start()
        {
            InitDeaultState();
            chState = GetComponent<CharacterStatus>();
            chAnim = GetComponent<ARPGDemo.Character.CharacterAnimation>();
            navAgent = GetComponent<NavMeshAgent>();
            chSkillSys = GetComponent<CharacterSkillSystem>();
            sightSensor = GetComponent<SightSensor>();

            //if (sightSensor != null)
            //{
            //    sightSensor.OnPerception += sightSensor_OnPerception;
            //    sightSensor.OnNonPerception += sightSensor_OnNonPerception;
            //}
        }

        private void Update()
        {
            
            currentState.Reason(this);
            
            currentState.Action(this);


        }

        private void OnEnable()
        {
            
            InvokeRepeating("ResetTarget", 0, 0.2f);
        }

        private void OnDisable()
        {
            //InitDeaultState();
            CancelInvoke("ResetTarget");

        }

        //方法
        //重置目标
        private void ResetTarget()
        {
            ////1.物体没tag标记用射线找，有tag标记用标记找，性能高
            ////找出标记为tag的所有物体
            List<GameObject> listTargets = new List<GameObject>();

            for (int i = 0; i < targetTags.Length; i++)
            {
                var targets = GameObject.FindGameObjectsWithTag(targetTags[i]);
                if (targets != null && targets.Length > 0)
                {
                    listTargets.AddRange(targets);
                }

            }

            if (listTargets.Count == 0)
            {
                return;
            }


            ////2.过滤指定半径，HP>0,角度范围
            var enemys = listTargets.FindAll((go) =>
            (Vector3.Distance(transform.position, go.transform.position) < sightDistance)
            && (go.gameObject.GetComponent<CharacterStatus>().HP > 0));

            if (enemys == null || enemys.Count == 0)
            {
                return;
            }

            ////3.根据攻击类型返回干个或多个


            targetObj = ArrayHelper.Min(enemys.ToArray(), (e) => Vector3.Distance(transform.position, e.gameObject.transform.position)).transform;
           

        }

        //向目标移动
        public void MoveToTarget(Vector3 target,float moveSpeed,float stopDistance)
        {
            navAgent.speed = moveSpeed;
            navAgent.stoppingDistance = stopDistance;//chState.attackDistance;
            navAgent.SetDestination(target);

        }

        //停止移动
        public void StopMove()
        {
            navAgent.enabled = false;
            navAgent.enabled = true;
        }

        



        //指定默认状态
        private void InitDeaultState()
        {
            //根据窗口指定的默认状态编号，为三个字段赋值初始化
            defaultState = states.Find(s => s.stateid == defaultStateid);
            currentState = defaultState;
            currentStateid = defaultStateid;

        }



        public void PlayAnimation(string anim)
        {
            chAnim.PlayAnimation(anim);
        }

        //调用AI配置文件 确定 条件 状态的映射关系
        private void ConfigFSM()
        {
            #region 方法一 硬编码
            //1.创建状态对象
            //IdleState idle = new IdleState();
            //DeadState dead = new DeadState();
            //PursuitState pursuit = new PursuitState();

            //idle.AddTrigger(FSMTriggerID.NoHealth, FSMStateID.Dead);
            //idle.AddTrigger(FSMTriggerID.SawPlayer, FSMStateID.Pursuit);

            //pursuit.AddTrigger(FSMTriggerID.NoHealth, FSMStateID.Dead);
            //pursuit.AddTrigger(FSMTriggerID.ReachPlayer, FSMStateID.Attacking);
            //pursuit.AddTrigger(FSMTriggerID.LosePlayer, FSMStateID.Default);

            //states.Add(idle);

            //states.Add(dead);

            //states.Add(pursuit);
            #endregion

            #region 方法二 使用AI配置文件
            //读取配置文件中得信息到字典中
            var dic = AIConfigurationReader.Load(aiConfigFile);

            //1.创建状态对象
            foreach (var stateName in dic.Keys)
            {
                var typeObj = Type.GetType("AI.FSM." + stateName + "State");
                var stateObj = Activator.CreateInstance(typeObj) as FSMState;

                //2.添加条件映射
                foreach (var triggerid in dic[stateName].Keys)
                {
                    var trigger = (FSMTriggerID)(Enum.Parse(typeof(FSMTriggerID), triggerid));
                    var state = (FSMStateID)(Enum.Parse(typeof(FSMStateID), dic[stateName][triggerid]));

                    stateObj.AddTrigger(trigger, state);
                }
                //3.放入状态集合
                states.Add(stateObj);
            }
            
            
            #endregion
        }
        public void ChangeActiveState(FSMTriggerID triggerid)
        {
            //1.根据当前条件 得到下一个状态
            var nextStateId = currentState.GetOutputState(triggerid);


            if(nextStateId == FSMStateID.None)
            {
                return;
            }

            FSMState nextState = null;
            if(nextStateId == FSMStateID.Default)
            {
                nextState = defaultState;
            }
            else
            {
                nextState = states.Find(s => s.stateid == nextStateId);
            }
            //2.退出状态
            currentState.ExitState(this);  

            //更新当前状态和编号
            currentStateid = nextStateId;  
            currentState = nextState;
            //3.进入下一个状态

            currentState.EnterState(this);
        }

        //public void sightSensor_OnPerception(List<AbstractTrigger> listTrigger)
        //{
        //    targetObj = null;
        //    var tempList = listTrigger.FindAll(p => Array.IndexOf(targetTags, p.tag) >= 0);
        //    if (tempList.Count > 0)
        //    {
        //        tempList =
        //            tempList.FindAll(p => p.GetComponent<CharacterStatus>().HP > 0);
        //        if (tempList.Count > 0)
        //        {
        //            targetObj = ArrayHelper.Min(tempList.ToArray(),
        //                p => Vector3.Distance(p.transform.position,
        //                    transform.position)).transform;
        //        }
        //    }
        //}
        //public void sightSensor_OnNonPerception()
        //{
        //    targetObj = null;
        //}
    }
}
