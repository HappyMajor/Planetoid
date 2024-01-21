using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Universe 
{
    private long lastTick = 0;
    private long TICK_INTERVAL = 100; //seconds
    private HashSet<Action> actionList = new HashSet<Action>();

    [SerializeField]
    private static List<WorldEntity> worldEntities = new List<WorldEntity>();
    [SerializeField]
    private List<Planet> planets = new List<Planet>();
    [SerializeField]
    private List<Sun> suns = new List<Sun>();
    [SerializeField]
    private List<SolarSystem> solarSystems = new List<SolarSystem>();
    public List<Planet> Planets { get => planets; set => planets = value; }
    public List<Sun> Suns { get => suns; set => suns = value; }
    public List<SolarSystem> SolarSystems { get => solarSystems; set => solarSystems = value; }
    public static List<WorldEntity> WorldEntities { get => worldEntities; set => worldEntities = value; }

    public void Tick()
    {
        if(CurrentTimeMillis() > this.TICK_INTERVAL + this.lastTick)
        {
            foreach(WorldEntity entity in worldEntities)
            {
                entity.Update(CurrentTimeMillis() - lastTick);
            }
            foreach(Action task in actionList)
            {
                task.Invoke();
            }
            this.lastTick = CurrentTimeMillis(); //in milliseconds
        } 
    }

    private static DateTime JanFirst1970 = new DateTime(1970, 1, 1);
    public static long CurrentTimeMillis()
    {
        return (long)((DateTime.Now.ToUniversalTime() - JanFirst1970).TotalMilliseconds + 0.5);
    }

    public void RegisterJob(Action action)
    {
        this.actionList.Add(action);
    }

    public void UnregisterJob(Action action)
    {
        this.actionList.Remove(action);
    }
}
