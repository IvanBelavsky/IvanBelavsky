using UnityEngine;
using Zenject;

public class SceneTransactionInstaller : MonoInstaller
{
    [SerializeField] private PanelFade _fadePanel;

    public override void InstallBindings()
    {
        Container.Bind<PanelFade>().FromInstance(_fadePanel);

        BindSaveService();
        BindSceneService();
        BindTranslateService();
    }

    private void BindTranslateService()
    {
        Container.Bind<TranslateService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.TranslateService)
            .AsSingle();
    }

    private void BindSaveService()
    {
        Container.Bind<SaveService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.SaveService)
            .AsSingle();
    }

    private void BindSceneService()
    {
        Container.Bind<SceneService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.SceneService)
            .AsSingle();
    }
}