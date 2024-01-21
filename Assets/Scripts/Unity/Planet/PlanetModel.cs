public class PlanetModel : Model<Planet>
{
    public void Start()
    {
        if(this.initializeSelf)
        {
            this.DomainModel = new();
        }
    }
}