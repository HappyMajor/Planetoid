using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Controller<T> : MonoBehaviour
{
    [SerializeField]
    private ModelBehaviour _modelComponent;

    private Model<T> modelComponent;
    public Model<T> ModelComponent { get => modelComponent; set => modelComponent = value; }
    public abstract void OnModelChange(T model);


    public void Awake()
    {
        if(this._modelComponent != null)
        {
            this.modelComponent = (Model<T>)this._modelComponent;
            this.modelComponent.AddController(this);
        }
    }

    public static List<Controller<T>> GetControllers(T model)
    {
        ModelBehaviour[] allObjects = GameObject.FindObjectsOfType<ModelBehaviour>();
        foreach(ModelBehaviour obj in allObjects)
        {
            if(obj is Model<T>)
            {
                Model<T> m = (Model<T>)obj;
                return m.Controllers;
            }
        }
        return new List<Controller<T>>();
    }
}
