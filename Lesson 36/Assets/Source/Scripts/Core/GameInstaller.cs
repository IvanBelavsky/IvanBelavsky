using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PauseService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.PauseService)
            .AsSingle();
        
        Container.Bind<ButtonsUI>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.UIButtonsService)
            .AsSingle();
    }
}
