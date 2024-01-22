using PlanetoidMP;
using UnityEngine;

public class ShelterController : AbstractBuildingController<Shelter>
{
    private PlanetController planetController;
    public void Start()
    {
        this.planetController = transform.GetComponentInParent<PlanetController>(); 
    }
    public override void OnBuildingDataSet(Shelter building)
    {
        building.onLivestockRechargeEvent += OnLiveStockRecharge;
    }

    public void FixedUpdate()
    {
        this.building.Update(Time.fixedDeltaTime);
    }

    public void OnLiveStockRecharge(int amount)
    {
        Debug.Log("OnLiveStockRecharge OnLiveStockRecharge");
        if(this.planetController.isServer)
        {
            Debug.Log("ON SERVER OnLiveStockRecharge:" + amount);
            this.planetController.livestockAmount += amount;
            Debug.Log("new amount: " + this.planetController.livestockAmount);
        }
    }
}