using System.Collections.Generic;
using Planetoid.Livestock;
using Planetoid.Logging;
using UnityEngine;
public class Construction : Building
{
    [SerializeField]
    private float progress = 0f;
    [SerializeField]
    private float endProgress = 100f;
    private List<Livestock> workers = new List<Livestock>();
    public delegate void ConstructionFinishedDelegate(Construction construction);
    public event ConstructionFinishedDelegate ConstructionFinishedEvent;
    public float Progress { get => progress; set => progress = value; }
    public string BuildsTo { get => buildsTo; set => buildsTo = value; }
    public float EndProgress { get => endProgress; set => endProgress = value; }

    [SerializeField]
    private string buildsTo;
    public Construction() : base()  {
        this.StructureHitpoints = 100;
        this.ArmorHitpoints = 0;
        this.ShieldHitpoints = 0;
        this.Height = 1;
        this.Width = 1;
        this.EndProgress = 10;
        this.Progress = 0;
        this.Init();
    }

    public Construction(int width, int height) : base()
    {
        this.StructureHitpoints = 100;
        this.ArmorHitpoints = 0;
        this.ShieldHitpoints = 0;
        this.Height = height;
        this.Width = width;
        this.EndProgress = 10;
        this.Progress = 0;
        this.Init();
    }

    private void Init()
    {
        this.ConstructionFinishedEvent += this.OnConstructionFinished;
    }

    public void AddWorker(Livestock livestock)
    {
        this.workers.Add(livestock);
    }

    public void RemoveWorker(Livestock livestock)
    {
        this.workers.Remove(livestock);
    }

    public override void Update(float deltaTimeMillis)
    {
        this.AddProgress(deltaTimeMillis);
    }

    public void AddProgress(float deltaTimeMillis)
    {
        foreach(Livestock livestock in this.workers)
        {
            this.progress+= this.CalculateProgressInput(livestock, deltaTimeMillis);
            if(this.progress >= this.EndProgress)
            {
                this.ConstructionFinishedEvent?.Invoke(this);
            }
        }
    }

    private float CalculateProgressInput(Livestock livestock, float deltaTimeMillis)
    {
        Attribute attribute = livestock.GetAttribute(AttributeType.CRAFT);
        if (attribute == null) return 0f;

        //the amount of progress scales with Craft Per Second
        return attribute.Value * (deltaTimeMillis/1000);
    }

    public void OnConstructionFinished(Construction construction)
    {
    }
}