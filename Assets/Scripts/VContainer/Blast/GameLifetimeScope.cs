using VContainer;
using VContainer.Unity;
public class GameLifetimeScope : LifetimeScope
{ 
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponentInHierarchy<GameGrid>().AsSelf();
        builder.RegisterComponentInHierarchy<AudioController>().AsSelf();
        builder.RegisterComponentInHierarchy<ParticlesController>().AsSelf();
        builder.RegisterComponentInHierarchy<ResizeBorders>().AsSelf();
        builder.RegisterComponentInHierarchy<DropFillController>().AsSelf();
        builder.RegisterComponentInHierarchy<ItemFactory>().AsSelf(); 
        builder.RegisterComponentInHierarchy<MatchController>().AsSelf();
        builder.RegisterComponentInHierarchy<HintController>().AsSelf();
        builder.RegisterComponentInHierarchy<ItemImageRepo>().AsSelf();
        builder.RegisterComponentInHierarchy<LevelController>().AsSelf();
        builder.RegisterComponentInHierarchy<GameController>().AsSelf();
        
        builder.RegisterComponentInHierarchy<GameInjector>().AsSelf();
    }
}