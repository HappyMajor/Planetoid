using System.Collections.Generic;

public class BlueprintService
{
    private IBlueprintRepository blueprintRepository;
    public BlueprintService(IBlueprintRepository blueprintRepository) {
        this.blueprintRepository = blueprintRepository;
    }
    public List<Blueprint> GetAllBlueprints()
    {
        return this.blueprintRepository.GetAllBlueprints();
    }

    public Blueprint GetBlueprint(string id)
    {
        return this.blueprintRepository.GetBlueprintById(id);
    }
}