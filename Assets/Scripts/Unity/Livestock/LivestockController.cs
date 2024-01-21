using Planetoid.Livestock;

public class LivestockController : Controller<Livestock>
{
    public override void OnModelChange(Livestock model)
    {
        transform.position = model.Position;
    }
}