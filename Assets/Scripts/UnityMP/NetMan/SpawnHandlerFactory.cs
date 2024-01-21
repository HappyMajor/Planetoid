using System.Collections.Generic;

public class SpawnHandlerFactory
{
    public Dictionary<string, ISpawnHandler> spawnHandlers = new();

    public SpawnHandlerFactory() {
        RegisterSpawnHandler("constructionSpawnHandler", new ConstructionSpawnHandler());
    }

    public void RegisterSpawnHandler(string spawnHandlerId, ISpawnHandler spawnHandler)
    {
        if(!spawnHandlers.ContainsKey(spawnHandlerId))
        {
            spawnHandlers.Add(spawnHandlerId, spawnHandler);
        }
    }

    public ISpawnHandler GetSpawnHandler(string spawnHandlerId)
    {
        if(spawnHandlers.ContainsKey(spawnHandlerId))
        {
            return spawnHandlers[spawnHandlerId];
        } else
        {
            throw new System.Exception("No spawnhandler found for id: " + spawnHandlerId);
        }
    }
}