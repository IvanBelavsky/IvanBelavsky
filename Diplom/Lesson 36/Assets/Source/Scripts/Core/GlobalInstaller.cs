using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindTranslateService();
        BindAnaliticsService();
        BindSaveService();
    }

    private void BindSaveService()
    {
        Container.Bind<SaveService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.SaveService)
            .AsSingle();
    }

    private void BindTranslateService()
    {
        Container.Bind<TranslateService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.TranslateService)
            .AsSingle();
    }
    
    private void BindAnaliticsService()
    {
        Container.Bind<AnalyticsService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.AnaliticsService)
            .AsSingle();
    }
}