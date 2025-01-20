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
        builder.RegisterComponentInHierarchy<AudioController>().AsSelf();
        builder.RegisterComponentInHierarchy<ParticlesController>().AsSelf();
        
        builder.RegisterComponentInHierarchy<GameInjector>().AsSelf();
        
        
        builder.RegisterComponentInHierarchy<LevelController>().AsSelf();
        
        builder.RegisterComponentInHierarchy<ResizeBorders>().AsSelf();

        builder.RegisterComponentInHierarchy<DropFillController>().AsSelf();
        builder.RegisterComponentInHierarchy<ItemFactory>().AsSelf(); 
        builder.RegisterComponentInHierarchy<MatchController>().AsSelf();
        builder.RegisterComponentInHierarchy<HintController>().AsSelf();

        builder.RegisterComponentInHierarchy<ItemImageRepo>().AsSelf();
        
        builder.RegisterComponentInHierarchy<GameGrid>().AsSelf();

        builder.RegisterComponentInHierarchy<GameController>().AsSelf();
    }
}