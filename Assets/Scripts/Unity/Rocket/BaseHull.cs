using UnityEngine;

public class BaseHull : MonoBehaviour, IHull
{
    public Transform warheadPosition;
    public Transform thrusterPosition;
    public Transform extraThrusterPosition;
    public Transform advancedCUPosition;
    public Transform fuelTankPosition;
    public BaseMissileCU baseMissileCU;

    public void Start()
    {
        if(baseMissileCU.Thruster != null)
        {
            ((MonoBehaviour)baseMissileCU.Thruster).transform.localPosition = thrusterPosition.localPosition;
        }

        if(baseMissileCU.Warhead != null)
        {
            ((MonoBehaviour)baseMissileCU.Warhead).transform.localPosition = warheadPosition.localPosition;
        }

        if(baseMissileCU.AdvancedCU != null) 
        { 
            ((MonoBehaviour)baseMissileCU.AdvancedCU).transform.localPosition = advancedCUPosition.localPosition;
        }

        if(baseMissileCU.ExtraThruster != null)
        {
            ((MonoBehaviour)baseMissileCU.ExtraThruster).transform.localPosition = extraThrusterPosition.localPosition;
        }

        if(baseMissileCU.FuelTank != null)
        {
            ((MonoBehaviour)baseMissileCU.FuelTank).transform.localPosition = fuelTankPosition.localPosition;
        }
    }
}