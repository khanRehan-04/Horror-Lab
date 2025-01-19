using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    // Dictionary to store events by name
    private Dictionary<string, Action> eventDictionary = new Dictionary<string, Action>();
    private Dictionary<string, Action<object>> eventDictionaryWithParam = new Dictionary<string, Action<object>>();

    protected override void Awake()
    {
        base.Awake();
    }

    // Subscribe to an event
    public void Subscribe(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent += listener;
            eventDictionary[eventName] = thisEvent;
        }
        else
        {
            eventDictionary[eventName] = listener;
        }
    }

    // Subscribe to an event with a parameter
    public void Subscribe(string eventName, Action<object> listener)
    {
        if (eventDictionaryWithParam.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent += listener;
            eventDictionaryWithParam[eventName] = thisEvent;
        }
        else
        {
            eventDictionaryWithParam[eventName] = listener;
        }
    }

    // Unsubscribe from an event
    public void Unsubscribe(string eventName, Action listener)
    {
        if (eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null) eventDictionary.Remove(eventName);
            else eventDictionary[eventName] = thisEvent;
        }
    }

    // Unsubscribe from an event with a parameter
    public void Unsubscribe(string eventName, Action<object> listener)
    {
        if (eventDictionaryWithParam.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent -= listener;
            if (thisEvent == null) eventDictionaryWithParam.Remove(eventName);
            else eventDictionaryWithParam[eventName] = thisEvent;
        }
    }

    // Trigger an event
    public void TriggerEvent(string eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out Action thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    // Trigger an event with a parameter
    public void TriggerEvent(string eventName, object param)
    {
        if (eventDictionaryWithParam.TryGetValue(eventName, out Action<object> thisEvent))
        {
            thisEvent?.Invoke(param);
        }
    }
}
