using UnityEngine;

public class BoardLogOutput : MonoBehaviour
{
    private string log;
    public void Output(string log)
    {
        this.log = log;
        Debug.Log(log);
    }
}