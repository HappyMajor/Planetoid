using System.Collections;
using UnityEngine;

public class CelestialWalkerWalkState : ICelestialWalkerState
{
    public float WALK_SPEED = 1f;
    public float MIN_WALK_LENGTH_UNTIL_TRANSITION = 1f;
    public float MAX_WALK_LENGTH_UNTIL_TRANSITION = 3f;
    public void OnEnter(CelestialWalker walker)
    {
        float len = Random.Range(MIN_WALK_LENGTH_UNTIL_TRANSITION, MAX_WALK_LENGTH_UNTIL_TRANSITION);
        int leftOrRight = Random.Range(0, 2);

        walker.Walk(len, leftOrRight == 1 ? -WALK_SPEED : WALK_SPEED, (float x, float y) =>
        {
            walker.transform.position = new Vector2(x, y);
            walker.StandUpright();
        }, walker.transform.position, () =>
        {
            walker.ChangeState(new CelestialWalkerIdle());
        });
    }

    public void OnExit(CelestialWalker walker)
    {
    }

    public void OnUpdate(CelestialWalker walker)
    {
    }
}