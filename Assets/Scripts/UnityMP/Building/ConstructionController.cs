using Mirror;
using PlanetoidMP;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityMP
{
    public class ConstructionController : NetworkBehaviour
    {
        public Construction Construction { get => construction; set => OnModelSetServer(value); }
        private readonly PlanetoidLogger logger = new(typeof(ConstructionController), LogLevel.DEBUG);

        [SerializeField]
        [SyncVar (hook = "OnModelSyncedClient")]
        private Construction construction;

        private SpriteMaskController spriteMaskController;

        public void Start()
        {
            this.spriteMaskController = transform.GetComponentInChildren<SpriteMaskController>();
        }

        [Server]
        public void OnModelSetServer(Construction model)
        {
            this.construction = model;
            logger.Log("OnModelSetServer: " + model.BuildsTo);
        }

        public void Update()
        {
            if (this.construction != null)
            {
                //Debug.Log("AddProgress");
                this.construction.AddProgress(Time.deltaTime * 1000);
                //Debug.Log("Progress: " + this.construction.Progress);
                //Debug.Log("EndProgress " + this.construction.EndProgress);
                this.spriteMaskController.slide = this.construction.Progress / this.construction.EndProgress;
            }
        }

        [Client]
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
    }
}
