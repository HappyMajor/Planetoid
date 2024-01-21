public class ShelterFactory : AbstractBuildingFactory
{
    public override Building GetBuilding()
    {
        return new Shelter();
    }
}