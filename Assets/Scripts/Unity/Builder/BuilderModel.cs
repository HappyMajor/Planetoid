public class BuilderModel : Model<Builder>
{
    public void Build(string blueprintId)
    {
        this.DomainModel.Build(blueprintId);
    }
}