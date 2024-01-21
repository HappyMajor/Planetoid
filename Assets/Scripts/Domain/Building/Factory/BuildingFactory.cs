public class BuildingFactory
{
    public BuildingFactory() {

    }
    public static Building GetBuilding(BuildingType type)
    {
        switch(type)
        {
            case BuildingType.SHELTER:
                return new Shelter();
            case BuildingType.CONSTRUCTION: 
                return new Construction();
            case BuildingType.HQ:
                return new HQ();
            default: 
                return null;
        }
    }
}