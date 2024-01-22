using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using static UnityEngine.EventSystems.EventTrigger;

public class SelectedOwnPlanetEvent
{
    public int livestockAmount;

    public int maxLivestockAmount;

    public int energy;
    public int metal;

    public SelectedOwnPlanetEvent(int livestockAmount, int maxLivestockAmount,int energy,int metal)
    {
        this.metal = metal;
        this.energy = energy;
        this.livestockAmount = livestockAmount;
        this.maxLivestockAmount = maxLivestockAmount;
    }
}