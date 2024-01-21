using UnityEngine;
using Frictionless;
public class TestEventSystem : MonoBehaviour
{
    public void Start()
    {
        GenericsTest<PikachuEvent>.genericList.Add(new PikachuEvent());
        GenericsTest<GlumandaEvent>.genericList.Add(new GlumandaEvent());

        GenericsTest<GlumandaEvent>.Loop();
    }
}

public class PikachuEvent
{
    public string name = "pikachu";
}

public class GlumandaEvent
{
    public string name = "Glumanda";
}