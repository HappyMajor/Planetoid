public class MaterialStack : IMaterialStack
{
    private int materialId;
    private string name;
    private int amount;

    public int Amount { get => amount; set => amount = value; }
    public string Name { get => name; set => name = value; }
    public int MaterialId { get => materialId; set => materialId = value; }

    public MaterialStack(int materialId, string name, int amount)
    {
        this.materialId = materialId;
        this.name = name;   
        this.amount = amount;
    }
}
