using System.Collections.Generic;

public class PlanetFactory
{
    private BlueprintRepository blueprintRepository;
    private BuildingStore buildingStore;
    public PlanetFactory(BlueprintRepository blueprintRepository, BuildingStore buildingStore) {
        this.blueprintRepository = blueprintRepository;
        this.buildingStore = buildingStore;
    }
    public List<Planet> Planets { get; set; } = new List<Planet>();

    public Planet CreatePlanet()
    {
        PlanetBuildingContext planetBuildingContext = new PlanetBuildingContext();
        Builder builder = new Builder(planetBuildingContext, blueprintRepository, buildingStore);
        builder.AvailableBlueprints.Add(BlueprintFactory.ID_HQ);
        Planet planet = new Planet(builder);
        planetBuildingContext.Planet = planet;
        return planet;
    }
}