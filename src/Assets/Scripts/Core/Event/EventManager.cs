using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EventParam
{
    public EventDefine Define;
    public float SendTime;
    public object Param1, Param2, Param3, Param4;
    public EventParam(EventDefine define, float sendtime, object param1, object param2, object param3, object param4)
    {
        Define = define;
        SendTime = sendtime;
        Param1 = param1;
        Param2 = param2;
        Param3 = param3;
        Param4 = param4;
    }
}

public class EventFunction
{
    public EventManager.EventFunctionDelegate function;
    public float SendTime;
}
public class EventManager
{
    public delegate void EventDelegate(EventDefine define, object param1, object param2, object param3, object param4);
    public delegate void EventFunctionDelegate();

    static EventManager instance;
    public static EventManager Instance
    {
        get
        {
            if (instance == null)
                instance = new EventManager();
            return instance;
        }
    }

    Dictionary<EventDefine, List<EventDelegate>> EventDic = new Dictionary<EventDefine, List<EventDelegate>>();
    List<EventParam> NextUpdateEvent = new List<EventParam>();
    List<EventFunction> functionList = new List<EventFunction>();

    public void RegisterEvent(EventDefine define, EventDelegate func)
    {
        if (!EventDic.ContainsKey(define))
        {
            EventDic.Add(define, new List<EventDelegate>());
        }
        EventDic[define].Add(func);
    }

    public void UnRegisterEvent(EventDefine define, EventDelegate func)
    {
        if (!EventDic.ContainsKey(define))
        {
            return;
        }
        EventDic[define].Remove(func);

    }

    void DellEvent(EventDefine define, object param1, object param2, object param3, object param4)
    {
        List<EventDelegate> funclist;
        try
        {
            funclist = EventDic[define];
        }
        catch (System.Exception ex)
        {
            //LogManager.Log("NoDelegateRecvThisEvent:" + define.ToString());
            return;
        }
        for (int i = 0; i < funclist.Count; i++)
        {
            funclist[i](define, param1, param2, param3, param4);
        }
    }
    //sendtime<0：立即发送，=0下次Update时发送，>0：sendtime时间后发送
    public void SendEvent(EventDefine define, float sendtime, object param1 = null, object param2 = null, object param3 = null, object param4 = null)
    {
        if (sendtime>=0)
        {
            NextUpdateEvent.Add(new EventParam(define, sendtime, param1, param2, param3, param4));
        }
        else
        {
            DellEvent(define, param1, param2, param3, param4);
        }
        

    }

    public void Update()
    {
        List<EventParam> newNextUpdateEvent = new List<EventParam>();
        for (int i = 0; i < NextUpdateEvent.Count;i++ )
        {
            if (NextUpdateEvent[i].SendTime <= Time.deltaTime)
            {
                DellEvent(NextUpdateEvent[i].Define, NextUpdateEvent[i].Param1, NextUpdateEvent[i].Param2, NextUpdateEvent[i].Param3, NextUpdateEvent[i].Param4);
            }
            else
            {
                NextUpdateEvent[i].SendTime -= Time.deltaTime;
                newNextUpdateEvent.Add(NextUpdateEvent[i]);
            }
        }
        NextUpdateEvent = newNextUpdateEvent;

        List<EventFunction> newFunctionList = new List<EventFunction>();
        for (int i = 0; i < functionList.Count; i++)
        {
            if (functionList[i].SendTime<=Time.deltaTime)
            {
                functionList[i].function();
            }
            else
            {
                functionList[i].SendTime -= Time.deltaTime;
                newFunctionList.Add(functionList[i]);
            }
        }
        functionList = newFunctionList;
    }

    public void Invoke(EventFunctionDelegate func,float delay=0)
    {
       EventFunction eventfunc=new EventFunction();
        eventfunc.SendTime = delay;
        eventfunc.function = func;
        functionList.Add(eventfunc);
    }
}
