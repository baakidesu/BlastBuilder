using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        
        builder.RegisterComponentInHierarchy<GameInjector>().AsSelf();
        
        builder.RegisterComponentInHierarchy<GameGrid>().AsSelf();
        
        builder.RegisterComponentInHierarchy<LevelController>().AsSelf();
        
        builder.RegisterComponentInHierarchy<ResizeBorders>().AsSelf();
        
        builder.Register<DropFillController>(Lifetime.Singleton);
        builder.Register<ItemFactory>(Lifetime.Singleton);
        builder.Register<MatchController>(Lifetime.Singleton);
    }
}