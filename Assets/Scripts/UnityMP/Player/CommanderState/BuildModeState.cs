using UnityEngine;
public class BuildModeState : ICommanderState
{

    public void OnEnter(CommanderStateManager stateManager)
    {
        stateManager.ListenForEvents<BuildModeEndEvent>(this, (ev) =>
        {
            stateManager.NextState(new DefaultCommanderState());
        });

        this.SetCameraSelects(false);
        this.SetCameraController(false);
    }

    public void OnExit(CommanderStateManager stateManager)
    {
        stateManager.StartCoroutine(RoutineUtil.DoLater(0.125f, () =>
        {
            this.SetCameraSelects(true);
            this.SetCameraController(true);
        }));
    }

    public void OnUpdate(CommanderStateManager stateManager)
    {

    }

    private void SetCameraController(bool onoff)
    {
        CameraController.GetInstance().CanMoveWithArrowKeys = onoff;
    }

    private void SetCameraSelects(bool onoff)
    {
        CameraSelect[] cameraSelects = Component.FindObjectsByType<CameraSelect>(FindObjectsSortMode.InstanceID);
        foreach (CameraSelect cameraSelect in cameraSelects)
        {
            cameraSelect.enabled = onoff;
        }
        GlobalSelectable[] selectables = Component.FindObjectsByType<GlobalSelectable>(FindObjectsSortMode.InstanceID);
        foreach (ISelectable selectable in selectables)
        {
            selectable.MonoBehaviour.enabled = onoff;
        }
    }
}