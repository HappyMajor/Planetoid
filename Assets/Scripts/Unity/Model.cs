using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Model<T> : ModelBehaviour
{
    [SerializeField]
    private T domainModel;

    private List<Controller<T>> controllers = new List<Controller<T>>();
    public T DomainModel { get => domainModel; set => this.SetModel(value); }
    public List<Controller<T>> Controllers { get => controllers; set => controllers = value; }
    public bool initializeSelf = false;

    public void AddController(Controller<T> controller)
    {
        this.controllers.Add(controller);
        if(this.domainModel != null)
        {
            controller.OnModelChange(this.domainModel);
        }
    }

    public void SetModel(T model)
    {
        this.domainModel = model;
        foreach(Controller<T> controller in this.controllers)
        {
            controller.OnModelChange(model);
        }
    }

    public List<Controller<T>> GetControllers()
    {
        return this.controllers;
    }
}
