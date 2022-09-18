using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE
    {
        PATROL, CHASE, FIRE
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }
    
    public STATE name;
    protected EVENT stage;
    protected GameObject oppTank;
    protected Animator anim;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    public State(GameObject _oppTank, NavMeshAgent _agent, Animator _anim, Transform _player)
    {
        oppTank = _oppTank;
        agent = _agent;
        anim = _anim;
        player = _player;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public State Process()
    {
        //Debug.Log(stage);
        if(stage == EVENT.ENTER) Enter();
        if(stage == EVENT.UPDATE) Update();
        if(stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

}

public class Patrol : State
{
    float viewDistance = 300.0f;
    float fieldOfViewAngle = 120.0f;
    public bool key = true;
    int nexttarget = 0;
    public Patrol(GameObject _oppTank, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_oppTank, _agent, _anim, _player)
    {
        name = STATE.PATROL;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        Patroling();
        if(IsCanSee())
        //if(Random.Range(0, 100) < 10)
        {
            //Debug.Log("start Chase");
            nextState = new Chase(oppTank, agent, anim, player);
            stage = EVENT.EXIT;
            //Debug.Log(stage);
            return;
        }
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    bool IsCanSee()
    {
        var direction = player.position - agent.transform.position;
        // 计算当前对象的位置到目标点的方向与当前对象的前进方向的夹角
        float angle = Vector3.Angle(direction, agent.transform.forward);
        if ((agent.transform.position - player.position).sqrMagnitude < viewDistance/2)
        {
            return true;
        }
        else if ((agent.transform.position - player.position).sqrMagnitude < viewDistance && angle < fieldOfViewAngle * 0.5f)
        {
            return true;
        }
        return false;

    }

    void Patroling()
    {
        
        if(key)
        {
            agent.SetDestination(GameEnv.Singleton.Wps[0].transform.position);
            key = false;
        }
        
        if ((agent.transform.position - GameEnv.Singleton.Wps[nexttarget].transform.position).sqrMagnitude < 1*1)
        {
            nexttarget = (nexttarget + 1) % 7;
            agent.SetDestination(GameEnv.Singleton.Wps[nexttarget].transform.position);
        } 
        //if((agent.transform.position - GameEnv.Singleton.Wps[0].transform.position).sqrMagnitude < 1*1)
        //{
        //    agent.SetDestination(GameEnv.Singleton.Wps[4].transform.position);
        //}
    }
}


public class Chase : State
{
    float GoneDistance;
    float FireDistance;

    public Chase(GameObject _oppTank, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_oppTank, _agent, _anim, _player)
    {
        name = STATE.CHASE;
    }

    public override void Enter()
    {
        //Debug.Log("Enter Chase");
        GoneDistance = 500.0f;
        FireDistance = 200f;
        base.Enter();
    }

    public override void Update()
    {
        StartChase();
        if(IsEnemyGone())
        {
            //Debug.Log("Enter Patrol");
            nextState = new Patrol(oppTank, agent, anim, player);
            stage = EVENT.EXIT;
            return;
        }
        else if(IsCanFire())
        {
            nextState = new Fire(oppTank, agent, anim, player);
            stage = EVENT.EXIT;
            return;
        }
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    bool IsEnemyGone()
    {
        if ((agent.transform.position - player.position).sqrMagnitude > GoneDistance)
        {
            return true;
        }
        return false;
    }

    bool IsCanFire()
    {
        if ((agent.transform.position - player.position).sqrMagnitude < FireDistance)
        {
            return true;
        }
        return false;

    }

    void StartChase()
    {
        agent.SetDestination(player.position);
    }
}


public class Fire : State
{
    float FireDistance;

    public Fire(GameObject _oppTank, NavMeshAgent _agent, Animator _anim, Transform _player)
                    : base(_oppTank, _agent, _anim, _player)
    {
        name = STATE.FIRE;
    }

    public override void Enter()
    {
        FireDistance = 30f;
        base.Enter();
    }

    public override void Update()
    {
        
        if (agent.GetComponent<EnemyTankProperiy>().health>0){
            ChaseAndOpenFire();
        }
        if(IsTooFar())
        {
            nextState = new Chase(oppTank, agent, anim, player);
            stage = EVENT.EXIT;
            return ;
        }
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }

    bool IsTooFar()
    {
        if ((agent.transform.position - player.position).sqrMagnitude > FireDistance)
        {
            return true;
        }
        return false;
    }

    void ChaseAndOpenFire()
    {
        agent.SetDestination(player.position);
        agent.GetComponentInChildren<EnemyShell>().fire();
    }
}
