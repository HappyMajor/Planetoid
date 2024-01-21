using Planetoid.Livestock;

public class ConstructionUtils
{
    public static void FinishConstruction(Construction construction)
    {
        construction.Progress = construction.EndProgress;
        Livestock livestock = new Livestock();
        construction.AddWorker(livestock);
        construction.AddProgress(0);
    }
}