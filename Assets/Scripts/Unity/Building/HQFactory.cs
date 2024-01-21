public class HQFactory : AbstractBuildingFactory
{
    public override Building GetBuilding()
    {
        return new HQ();
    }
}