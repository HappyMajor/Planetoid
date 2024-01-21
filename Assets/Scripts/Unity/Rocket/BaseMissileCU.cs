using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMissileCU : RigidBodyBehaviour, Launchable
{
    public IHull Hull { get => hull; set => hull = value; }
    public IFuelTank FuelTank { get => fuelTank; set => fuelTank = value; }
    public IAdvancedCU AdvancedCU { get => advancedCU; set => advancedCU = value; }
    public IExtraThruster ExtraThruster { get => extraThruster; set => extraThruster = value; }
    public IWarhead Warhead { get => warhead; set => warhead = value; }
    public IThruster Thruster { get => thruster; set => thruster = value; }
    public Vector2 Dir { get => dir; set => dir = value; }

    public string CUBrandName = "XSaturn CU Falcon PRO";
    public BoardLogOutput output;

    private IHull hull;
    private IFuelTank fuelTank;
    private IAdvancedCU advancedCU;
    private IExtraThruster extraThruster;
    private IWarhead warhead;
    private IThruster thruster;
    private string boardLog;
    private Vector2 dir;

    public void Awake() 
    {
        Debug.Log("START");
        this.LogToBoard("----- ++++ BOOTING " + this.CUBrandName + " ++++ -----");
        this.fuelTank = this.GetComponentInChildren<IFuelTank>();

        Debug.Log("FUEL TANK IS NULL? " + (this.fuelTank == null).ToString());
        Debug.Log("Fuel Tank: " + this.fuelTank.ToString());
        this.warhead = this.GetComponentInChildren<IWarhead>();
        this.thruster = this.GetComponentInChildren<IThruster>();
        this.extraThruster = this.GetComponentInChildren<IExtraThruster>();
        this.hull = this.GetComponentInChildren<IHull>();
    }

    public void PrintBoardLog()
    {
        if(this.output != null)
        {
            output.Output(this.boardLog);
        }
    }

    private void LogToBoard(string msg)
    {
        boardLog += "\n ---" + msg + " ---";
        PrintBoardLog();
    }

    private void LogErrorToBoard(string msg)
    {
        boardLog += "\n --- +|||!!!ERROR!!!|||+: " + msg + " ---";
        PrintBoardLog();
    }

    public void StartThruster()
    {
        this.thruster.StartThruster();
    }

    public void StopThruster()
    {
        this.thruster.StopThruster();
    }

    public void IncreaseThrusterIntensity(float amount)
    {
        this.thruster.IncreaseThrusterIntensity(amount);
    }

    public Fuel DrainFuel(Fuel fuel)
    {
        try
        {
            return this.fuelTank.DrainFuel(fuel);
        } catch(FuelEmptyException ex)
        {
            this.LogErrorToBoard("Fuel tank is empty");
            this.LogErrorToBoard("Stop Thrusters...");
            this.thruster.StopThruster();
            return new Fuel(FuelType.STANDARD, 0);
        } catch (FuelTypeUnavailableException ex)
        {
            this.LogErrorToBoard("Fuel Type unavailable: " + fuel.type.ToString());
            this.thruster.StopThruster();
            return new Fuel(FuelType.STANDARD, 0);
        }
    }

    public Rigidbody2D GetRigidBody() {
        return this.rb;
    }

    public void Launch(LaunchData launchData)
    {
        Debug.Log("LAUNCH");
        Debug.Log("Launch: FUel Tank: " + this.fuelTank.ToString());
        this.LogToBoard("Launch Missile");
        this.LogToBoard("Launch Data: " + launchData.ToString());
        this.Dir = launchData.Dir;
        this.LogToBoard("Add fuel to tank...");
        Fuel fuel = launchData.Fuel;
        this.fuelTank.AddFuel(fuel);
        this.LogToBoard("Start Thrusters...");
        this.StartThruster();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("HIT!!");
    }
}
