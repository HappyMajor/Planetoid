using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Planetoid.DateTime;

public class TimerJobTests
{
    private long startTicks = 0;
    private bool callbackInvoked = false;

    [SetUp]
    public void Setup()
    {
        Planetoid.Logging.Logger.Instance().AttachLoggingProvider(new LogProvider());
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator ShouldExecuteIn10Seconds()
    {
        long DELAY = 10000;
        long TOLERANCE = 50;
        bool success = false;
        //startTicks = DateTime.Now.Ticks;
        MilliDateTime milliDateTime = new MilliDateTime();
        IJobExecutor jobExecutor = new DoLaterJobExecutor(() =>
        {
            success = milliDateTime.ElapsedMillis <= DELAY + TOLERANCE && milliDateTime.ElapsedMillis >= DELAY;
        }, DELAY);
        BaseJobScheduler baseJobScheduler = new(jobExecutor);
        baseJobScheduler.Schedule();
        while (milliDateTime.ElapsedMillis <= DELAY + TOLERANCE * 2)
        {
            yield return null;
        }

        Assert.IsTrue(success);
    }

    [UnityTest]
    public IEnumerator ShouldExecuteIn1Seconds()
    {
        long DELAY = 1000;
        long TOLERANCE = 50;
        bool success = false;
        //startTicks = DateTime.Now.Ticks;
        MilliDateTime milliDateTime = new MilliDateTime();
        IJobExecutor jobExecutor = new DoLaterJobExecutor(() =>
        {
            success = milliDateTime.ElapsedMillis <= DELAY + TOLERANCE && milliDateTime.ElapsedMillis >= DELAY;
        }, DELAY);
        BaseJobScheduler baseJobScheduler = new(jobExecutor);
        baseJobScheduler.Schedule();
        while (milliDateTime.ElapsedMillis <= DELAY + TOLERANCE * 2)
        {
            yield return null;
        }

        Assert.IsTrue(success);
    }

    [UnityTest]
    public IEnumerator ShouldExecuteEvery1Seconds10Times()
    {
        long DELAY = 1000;
        long TOLERANCE = 100;
        int REPITITIONS = 10;
        int elapsedRepitions = 0;
        long timeOfCallback = -1;
        //startTicks = DateTime.Now.Ticks;
        MilliDateTime milliDateTime = new MilliDateTime();
        IJobExecutor jobExecutor = new RepeatingJobExecutor(() =>
        {
            elapsedRepitions++;
            if (elapsedRepitions == 10)
            {
                timeOfCallback = milliDateTime.ElapsedMillis;
                Debug.Log("timeOfLastCallbacks: " + milliDateTime.ElapsedMillis);
            }
        }, DELAY, REPITITIONS);
        BaseJobScheduler baseJobScheduler = new(jobExecutor);
        baseJobScheduler.Schedule();
        while (milliDateTime.ElapsedMillis <= (DELAY * (REPITITIONS + 3)) + TOLERANCE * 2)
        {
            yield return null;
        }

        Debug.Log("repititions: " + elapsedRepitions);
        Debug.Log("time: " + timeOfCallback);
        Assert.AreEqual(REPITITIONS, elapsedRepitions);
        Assert.AreEqual(DELAY * REPITITIONS, timeOfCallback, TOLERANCE);
    }

    [UnityTest]
    public IEnumerator ShouldExecuteExactly10Times()
    {
        long DELAY = 100;
        long TOLERANCE = 50;
        int REPITITIONS = 10;
        int elapsedRepitions = 0;
        //startTicks = DateTime.Now.Ticks;
        MilliDateTime milliDateTime = new MilliDateTime();
        IJobExecutor jobExecutor = new RepeatingJobExecutor(() =>
        {
            elapsedRepitions++;
            Debug.Log("Repititions: " + elapsedRepitions);
        }, DELAY, REPITITIONS);
        BaseJobScheduler baseJobScheduler = new(jobExecutor);
        baseJobScheduler.Schedule();
        while (milliDateTime.ElapsedMillis <= (DELAY * (REPITITIONS + 3)) + TOLERANCE * 2)
        {
            yield return null;
        }

        Assert.AreEqual(REPITITIONS, elapsedRepitions);
    }
}
