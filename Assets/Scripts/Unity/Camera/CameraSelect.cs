using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ISelectable))]
public class CameraSelect : MonoBehaviour
{
    [SerializeField]
    private ISelectable selectable;

    private bool isFollowing = false;
    private void Start()
    {
        this.selectable = GetComponent<ISelectable>();
    }
    public void Update()
    {
        if (selectable.Selected)
        {
            if (!isFollowing)
            {
                this.isFollowing = true;
                CameraController.GetInstance().Follow(transform);
            }
        }
        else
        {
            if (isFollowing)
            {
                this.isFollowing = false;
                Transform follow = CameraController.GetInstance().FollowTarget();
                if (follow != null)
                {
                    if(follow == transform)
                    {
                        CameraController.GetInstance().Follow(null);
                    }
                }
            }
        }
    }
}
