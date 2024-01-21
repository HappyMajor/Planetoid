using Mirror;

namespace UnityMP
{
    public abstract class MPBuildingController : NetworkBehaviour
    {
        [SyncVar (hook = "_OnModelSyncedClient")]
        private Building model;
        public Building Model { get => this.model; set => SetModel(value); }

        public void SetModel(Building model)
        {
            this.model = model;
            this._OnModelSetServer(model);
        }

        [Server]
        private void _OnModelSetServer(Building model)
        {
            this.OnModelSetServer(model);
        }

        [Client]
        private void _OnModelSyncedClient(Building oldModel, Building newModel)
        {
            this.OnModelSyncedClient(oldModel, newModel);
        }

        public abstract void OnModelSetServer(Building model);

        public abstract void OnModelSyncedClient(Building oldModel,  Building newModel);
    }
}