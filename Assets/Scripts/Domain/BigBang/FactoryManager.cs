public class FactoryManager
{
    private BuildingStore buildingStore;
    private BlueprintRepository blueprintRepository;
    private PlanetFactory planetFactory;
    public BuildingStore BuildingStore { get => buildingStore; private set => buildingStore = value; }
    public BlueprintRepository BlueprintRepository { get => blueprintRepository; private set => blueprintRepository = value; }
    public PlanetFactory PlanetFactory { get => planetFactory; private set => planetFactory = value; }

    public FactoryManager() {
        BuildingStore buildingStore = new BuildingStore();
        ShelterFactory shelterFactory = new ShelterFactory();
        HQFactory hQFactory = new HQFactory();
        ConstructionFactory constructionFactory = new ConstructionFactory();
        buildingStore.RegisterFactory(shelterFactory, BuildingType.SHELTER);
        buildingStore.RegisterFactory(hQFactory, BuildingType.HQ);
        buildingStore.RegisterFactory(constructionFactory, BuildingType.CONSTRUCTION);

        BlueprintFactoryFactory blueprintFactoryFactory = new BlueprintFactoryFactory(buildingStore);
        BlueprintFactory blueprintFactory = blueprintFactoryFactory.CreateFactory();
        BlueprintRepository blueprintRepository = new BlueprintRepository(blueprintFactory);

        PlanetFactory planetFactory = new PlanetFactory(blueprintRepository,buildingStore);

        this.buildingStore = buildingStore;
        this.blueprintRepository = blueprintRepository;
        this.planetFactory = planetFactory;
    }


}