using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;
using VContainer;

public class GameLifetimeScopeMainLevel : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<MapController>().AsSelf();
        builder.RegisterComponentInHierarchy<InputController>().AsSelf();
    }
}
