using System.Collections;
using UnityEngine;

public class CelestialWalkerIdle : ICelestialWalkerState
{
    float MIN_TIME_IDLE_BEFORE_WALK = 0f;
    float MAX_TIME_IDLE_BEFORE_WALK = 6f;

    public void OnEnter(CelestialWalker walker)
    {
        walker.StartCoroutine(Walk(walker));
        walker.StandUpright();
    }

    public IEnumerator Walk(CelestialWalker celestialWalker)
    {
        float random = Random.Range(MIN_TIME_IDLE_BEFORE_WALK, MAX_TIME_IDLE_BEFORE_WALK);
        yield return new WaitForSeconds(random);
        celestialWalker.ChangeState(new CelestialWalkerWalkState());
    }

    public void OnExit(CelestialWalker walker)
    {

    }

    public void OnUpdate(CelestialWalker walker)
    {

    }
}