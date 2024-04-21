using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindPauseService();
        BindAnaliticsService();
        BindGameInput();
    }
    
    private void BindPauseService()
    {
        Container.Bind<PauseService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.PauseService)
            .AsSingle();
    }
    
    private void BindGameInput()
    {
        Container.Bind<LoadingServicGame>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.LoadingSaveData)
            .AsSingle();
    }
    
    private void BindAnaliticsService()
    {
        Container.Bind<AnalyticsService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.AnaliticsService)
            .AsSingle();
    }
}