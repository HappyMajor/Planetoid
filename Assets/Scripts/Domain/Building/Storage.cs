using System.Collections.Generic;

public class Storage : Building
{
    private List<IMaterialStack> storedMaterials = new List<IMaterialStack>();
    public List<IMaterialStack> StoredMaterials { get => storedMaterials; set => storedMaterials = value; }
    public Storage() : base() {
        this.StructureHitpoints = 100;
        this.ArmorHitpoints = 100;
        this.ShieldHitpoints = 100;
        this.Height = 1;
        this.Width = 1;
    }
    public int HasAmountInStock(int materialId)
    {
        int amount = 0;
        foreach (IMaterialStack stored in storedMaterials)
        {
            if (stored.MaterialId == materialId) amount += stored.Amount;
        }
        return amount;
    }

    public bool HasInStock(MaterialStack materialStack)
    {
        int hasAmount = 0;
        foreach(IMaterialStack stored in storedMaterials)
        {
            if(stored.MaterialId == materialStack.MaterialId)
            {
                hasAmount += stored.Amount;
            }
            if(hasAmount >= materialStack.Amount)
            {
                return true;
            }
        }
        return false;
    }
}