public class BuilderService
{
    public void TryBuildOnPlanet(Player player, Planet planet, Blueprint blueprint)
    {
        planet.Builder.Build(blueprint.BlueprintId);
    }
}