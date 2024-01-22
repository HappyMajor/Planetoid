using Frictionless;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetoidMP
{
    [RequireComponent(typeof(ISelectable))]
    [RequireComponent(typeof(Ownable))]
    public class PlanetController : NetworkBehaviour
    {
        private ISelectable selectable;
        private Ownable ownable;

        [SyncVar] 
        public int livestockAmount = 0;

        [SyncVar]
        public int maxLivestockAmount = 0;

        [SyncVar]
        public int energy = 0;

        [SyncVar]
        public int metal = 0;

        [SyncVar]
        public int kryptonite = 0;


        public override void OnStartClient()
        {
            this.ownable = GetComponent<Ownable>(); 
            this.selectable = GetComponent<ISelectable>();

            this.selectable.onSelect += OnSelect;
            this.selectable.onDeSelect += OnDeselect;
        }

        public void OnSelect()
        {
            if(this.ownable.IsOwnedByMe())
            {
                Debug.Log("isServer: " + this.isServer);
                Debug.Log("ON SELECT LLOLOLL:  " + this.livestockAmount);
                MessageRouter.RaiseMessage<SelectedOwnPlanetEvent>(new SelectedOwnPlanetEvent(livestockAmount,10,69,69));
            }
        }

        public void OnDeselect()
        {
            if (this.ownable.IsOwnedByMe())
            {
                MessageRouter.RaiseMessage<DeSelectedOwnPlanetEvent>(new DeSelectedOwnPlanetEvent());
            }
        }
    }
}
