using System.Collections.Generic;

public class BlueprintRepository : IBlueprintRepository
{
    private BlueprintFactory blueprintFactory;
    public BlueprintRepository(BlueprintFactory blueprintFactory) {
        this.blueprintFactory = blueprintFactory;
    }
    public List<Blueprint> GetAllBlueprints()
    {
        List<Blueprint> blueprints = new List<Blueprint> {
            blueprintFactory.CreateBlueprint(BlueprintFactory.ID_SHELTER), blueprintFactory.CreateBlueprint(BlueprintFactory.ID_HQ)
        };
    
        return blueprints;
    }

    public Blueprint GetBlueprintById(string blueprintId)
    {
        return this.GetAllBlueprints().Find(x => x.BlueprintId == blueprintId);
    }
}