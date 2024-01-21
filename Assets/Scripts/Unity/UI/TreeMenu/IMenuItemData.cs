using System.Collections.Generic;
using UnityEngine;

public interface IMenuItemData
{
    List<IMenuItemData> Children { get; }
    IMenuItemData Parent { get; }
}