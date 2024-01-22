using Mirror;
using PlanetoidMP;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityMP
{
    public class ConstructionController : NetworkBehaviour, IBuildingController
    {
        public Construction Construction { get => construction; set => OnModelSetServer(value); }
        private readonly PlanetoidLogger logger = new(typeof(ConstructionController), LogLevel.DEBUG);

        private Construction construction;
        private PlanetController planetController;
        private BuildingContext buildingContext;

        private SpriteMaskController spriteMaskController;

        public void Start()
        {
            this.spriteMaskController = transform.GetComponentInChildren<SpriteMaskController>();
            this.planetController = transform.GetComponentInParent<PlanetController>();
            this.buildingContext = transform.GetComponentInParent<BuildingContext>(); 

            if(this.planetController == null)
            {
                throw new System.Exception("No planetcontroller found!");
            }

            if(this.buildingContext == null)
            {
                throw new System.Exception("No building context  found!");
            }

        }

        public void OnModelSetServer(Construction model)
        {
            this.construction = model;
            logger.Log("OnModelSetServer: " + model.BuildsTo);
            OnModelSyncedClient(null, model);
            this.construction.ConstructionFinishedEvent += OnConstructionFinished;
            
        }

        public void OnConstructionFinished(Construction construction)
        {
            if(this.planetController.isServer)
            {
                PolarPosition polarPosition = this.buildingContext.GetPolarpositionOfBuilding(construction);
                if(polarPosition == null)
                {
                    throw new System.Exception("For some dumb reason construction has no position / is probably not in the list of buildings / maybe this was called from the client??");
                } else
                {
                    this.buildingContext.SpawnBuilding(Buildings.GetBuilding(this.construction.BuildsTo), polarPosition.Phi, polarPosition.R);
                    this.buildingContext.UnspawnBuilding(this.construction);
                }
            }
        }

        public void Update()
        {
            if (this.construction != null)
            {
                if(this.construction.Progress < this.construction.EndProgress)
                {
                    //Debug.Log("AddProgress");
                    this.construction.AddProgress(Time.deltaTime * 1000);
                    //Debug.Log("Progress: " + this.construction.Progress);
                    //Debug.Log("EndProgress " + this.construction.EndProgress);
                    this.spriteMaskController.slide = this.construction.Progress / this.construction.EndProgress;
                }
            }
        }

        public void OnModelSyncedClient(Construction oldConstruction, Construction newConstruction)
        {
            Debug.Log("Builds to is: " + construction.BuildsTo);
            if(construction.BuildsTo != null)
            {
                BuildingPrefabResolver resolver = Component.FindAnyObjectByType<BuildingPrefabResolver>();
                GameObject buildsToPrefab = resolver.Resolve(Buildings.GetBuilding(construction.BuildsTo));
                Sprite sprite = buildsToPrefab.GetComponentInChildren<SpriteRenderer>().sprite;
                Vector3 localPosSprite = buildsToPrefab.GetComponentInChildren<SpriteRenderer>().transform.localPosition;
                transform.GetComponentInChildren<SpriteMask>().sprite = sprite;
                transform.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
                transform.GetComponentInChildren<SpriteRenderer>().transform.localPosition = localPosSprite;
                transform.GetComponentInChildren<SpriteMaskController>().Init();

                Planetoid.Livestock.Livestock worker = new Planetoid.Livestock.Livestock();
                worker.AddAttribute(new Planetoid.Livestock.Attribute(Planetoid.Livestock.AttributeType.CRAFT, 1));
;                   this.construction.AddWorker(worker);
            } else
            {
                Debug.LogError("ConstructionController OnModelSyncedClient Construction has not BuildsTO. Bad Data provided!");
            }
        }

        public void SetBuilding(Building building)
        {
            if(building is Construction construction)
            {
                this.OnModelSetServer(construction);    
            } else
            {
                throw new System.Exception("Bad data provided is not construction but is: " + building.GetType());  
            }
        }
    }
}
