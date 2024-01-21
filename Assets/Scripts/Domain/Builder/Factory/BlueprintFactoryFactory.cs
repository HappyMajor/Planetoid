public class BlueprintFactoryFactory
{
    private BuildingStore buildingStore;

    public BlueprintFactoryFactory(BuildingStore buildingStore)
    {
        this.buildingStore = buildingStore;
    }
    public BlueprintFactory CreateFactory()
    {
        return new BlueprintFactory(this.buildingStore);
    }
}