using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        builder.RegisterComponentInHierarchy<DropFillController>().AsSelf();
        builder.RegisterComponentInHierarchy<ItemFactory>().AsSelf(); //??????
        builder.Register<MatchController>(Lifetime.Singleton);
        
    }
}