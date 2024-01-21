using System.Collections.Generic;

public interface IBlueprintRepository
{
    public List<Blueprint> GetAllBlueprints();
    public Blueprint GetBlueprintById(string id);
}