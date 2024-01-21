public class ConstructionFactory : AbstractBuildingFactory
{
    public override Building GetBuilding()
    {

        return new Construction();
    }
}