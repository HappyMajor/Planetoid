using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBangController : MonoBehaviour
{
    public float universeWidth = 100f;
    public float universeHeight = 100f;
    public int planetsAmount = 12;
    public int sunsAmount = 1;
    public int solarSystemsAmount = 1;

    [SerializeField]
    private BigBang bigBang;
    public BigBang BigBang { get => bigBang; set => bigBang = value; }
    public GameObject universePrefab;
    public GameObject gamemodePrefab;

    public void SetModel(BigBang bigBang)
    {
        this.bigBang = bigBang;
        this.bigBang.UniverseSize = universeWidth;
        this.bigBang.SolarSystemsAmount = this.solarSystemsAmount;
        this.bigBang.PlanetAmount = this.planetsAmount;
    }

    public void Start()
    {
        Debug.Log("BigBangController On Start Server");
        this.SetModel(new BigBang());
        this.bigBang.StartBigBang();
        UniverseController universeController = this.CreateUniverseController();
        universeController.SetModel(this.bigBang.Universe);
        GameModeController gameMode = this.CreateGameMode();
        gameMode.StartGame(universeController);
    }

    public UniverseController CreateUniverseController()
    {
        GameObject universe = GameObject.Instantiate(universePrefab);
        UniverseController universeController = universe.GetComponent<UniverseController>();
        return universeController;
    }

    public GameModeController CreateGameMode()
    {
        GameObject gameMode = GameObject.Instantiate(gamemodePrefab);
        gameMode.GetComponent<GameModeModel>().SetModel(new GameMode());
        return gameMode.GetComponent<GameModeController>();
    }
}
