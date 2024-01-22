using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : Building
{
    private int livestockAmount = 10;
    public float livestockPerSecond = 1f;
    public float currentLivestackRecharge = 0f;

    public delegate void OnLivestockRechargeDelegate(int amount);
    public event OnLivestockRechargeDelegate onLivestockRechargeEvent;

    public override void Update(float seconds)
    {

        this.currentLivestackRecharge += livestockPerSecond * seconds;
        if(this.currentLivestackRecharge >= 1)
        {
            Debug.Log("CALL EVENT: " + this.currentLivestackRecharge);
            int liveStockRechargeAmount = (int)currentLivestackRecharge;
            this.currentLivestackRecharge = this.currentLivestackRecharge % 1;
            this.onLivestockRechargeEvent?.Invoke(liveStockRechargeAmount);
        }
    }


    public Shelter() : base()
    {
    }
}
