public class BuilderFactory
{
    private BlueprintRepository blueprintRepository;
    private BuildingStore buildingStore;
    public BuilderFactory(BlueprintRepository blueprintRepository, BuildingStore buildingStore) {
        this.blueprintRepository = blueprintRepository;
        this.buildingStore = buildingStore;
    }
    public Builder CreateBuilder(IBuildingContext buildingContext)
    {
        Builder builder = new Builder(buildingContext, this.blueprintRepository, this.buildingStore);
        return builder;
    }
}