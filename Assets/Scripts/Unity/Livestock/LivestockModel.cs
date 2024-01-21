using Planetoid.Livestock;

public class LivestockModel : Model<Livestock>
{
    private void Start()
    {
        if(this.initializeSelf)
        {
            this.DomainModel = new Livestock();
        }
    }
}