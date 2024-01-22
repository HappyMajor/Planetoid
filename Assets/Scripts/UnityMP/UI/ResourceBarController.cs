using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Frictionless;

public class ResourceBarController : MonoBehaviour
{
    public TMP_Text metalAmount;
    public TMP_Text energyAmount;
    public TMP_Text livestockAmount;

    public void Start()
    {
        MessageRouter.AddHandler<SelectedOwnPlanetEvent>((ev) =>
        {
            gameObject.SetActive(true);
            this.metalAmount.text = ev.metal.ToString();
            this.energyAmount.text = ev.energy.ToString();
            this.livestockAmount.text = ev.livestockAmount.ToString();
        });

        MessageRouter.AddHandler<DeSelectedOwnPlanetEvent>((ev) =>
        {
            gameObject.SetActive(false);
        });
        gameObject.SetActive(false);
    }
}
