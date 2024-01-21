using NUnit.Framework;
using Planetoid.Livestock;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine;
using Moq;

public class ConstructionTests
{
    [SetUp]
    public void Setup()
    {
        Planetoid.Logging.Logger.Instance().AttachLoggingProvider(new LogProvider());
    }
    [UnityTest]
    public IEnumerator ShouldRaiseConstructionFinishedEvent()
    {
        bool eventWasFired = false;
        Construction construction = new Construction();
        construction.EndProgress = 1;
        construction.BuildsTo = new HQ();
        construction.ConstructionFinishedEvent += (construction) =>
        {
            eventWasFired = true;
        };

        Livestock livestock = new Livestock();
        livestock.AddAttribute(new Attribute(AttributeType.CRAFT, 1));
        
        construction.AddWorker(livestock);

        long deltaTimeMillis = 1000;

        construction.AddProgress(deltaTimeMillis);

        yield return null;

        Debug.Log("progress was: " + construction.Progress + " of " + construction.EndProgress);

        Assert.IsTrue(eventWasFired);
    }

    [UnityTest]
    public IEnumerator shouldHaveExactly100Progress()
    {
        Construction construction = new Construction();
        construction.EndProgress = 1;
        construction.BuildsTo = new HQ();

        Livestock livestock = new Livestock();
        livestock.AddAttribute(new Attribute(AttributeType.CRAFT, 1));

        construction.AddWorker(livestock);

        long deltaTimeMillis = 100000; //100 seconds

        construction.AddProgress(deltaTimeMillis);

        yield return null;

        Debug.Log("progress was: " + construction.Progress + " of " + 100);

        Assert.AreEqual(100, construction.Progress);
    }

    [UnityTest]
    public IEnumerator shouldHaveLessWithBadWorker()
    {
        float craftAbility = 0.5f;
        Construction construction = new Construction();
        construction.EndProgress = 1;
        construction.BuildsTo = new HQ();

        Livestock livestock = new Livestock();
        livestock.AddAttribute(new Attribute(AttributeType.CRAFT, craftAbility));

        construction.AddWorker(livestock);

        long deltaTimeMillis = 100000; //100 seconds

        construction.AddProgress(deltaTimeMillis);

        yield return null;

        Debug.Log("progress was: " + construction.Progress + " of " + 50);

        Assert.AreEqual(50, construction.Progress);
    }

    [UnityTest]
    public IEnumerator shouldHaveMoreWithBetterWorker()
    {
        float craftAbility = 2f;
        Construction construction = new Construction();
        construction.EndProgress = 1;
        construction.BuildsTo = new HQ();

        Livestock livestock = new Livestock();
        livestock.AddAttribute(new Attribute(AttributeType.CRAFT, craftAbility));

        construction.AddWorker(livestock);

        long deltaTimeMillis = 100000; //100 seconds

        construction.AddProgress(deltaTimeMillis);

        yield return null;

        Debug.Log("progress was: " + construction.Progress + " of " + 200);

        Assert.AreEqual(200, construction.Progress);
    }

    [UnityTest]
    public IEnumerator ShouldCallRaiseEventOnConstructionCompleted()
    {
        bool eventWasRaised = false;
        Construction construction = new Construction();
        construction.ConstructionFinishedEvent += (c) =>
        {
            eventWasRaised = true;
        };
        ConstructionUtils.FinishConstruction(construction);

        yield return null;
        Assert.IsTrue(eventWasRaised);
    }
}