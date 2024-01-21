using System.Collections.Generic;

public class Blueprints
{
    private static Blueprints instance;
    private BlueprintFactory blueprintFactory;
    private BuildingStore buildingStore;
    private Blueprints() { 
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