using System.Collections.Generic;

public class Blueprints
{
    private static Blueprints instance;
    private BlueprintFactory blueprintFactory;
    private BuildingStore buildingStore;
    private Blueprints() { 
        buildingStore = new BuildingStore();
        buildingStore.RegisterFactory(new ShelterFactory(), BuildingType.SHELTER);
        buildingStore.RegisterFactory(new HQFactory(), BuildingType.HQ);
        buildingStore.RegisterFactory(new ConstructionFactory(), BuildingType.CONSTRUCTION);
        blueprintFactory = new BlueprintFactory(buildingStore);
    }

    public static Blueprints GetInstance()
    {
        if(instance == null) { instance = new Blueprints(); }
        return instance;
    }

    public Blueprint GetBlueprintById(string id)
    {
        return blueprintFactory.CreateBlueprint(id);
    }
}