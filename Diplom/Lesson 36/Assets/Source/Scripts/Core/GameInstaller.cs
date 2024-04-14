using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [Header("Position")] [SerializeField] private Transform _pointButtonsUI;
    [SerializeField] private Transform _pointPlayer;
    [SerializeField] private Canvas _canvas;

    private ScoreUI _scorePrefab;
    private PlayerHealth _playerPrefab;
    private GameBehaviourUI _gameBehaviourUIPrefab;

    public override void InstallBindings()
    {
        BindPauseService();
        BindGameBehaviour();
        BindPlayerHealth();
        BindFactoryBonus();
    }

    private void BindGameBehaviour()
    {
        _gameBehaviourUIPrefab = Resources.Load<GameBehaviourUI>(AssetsPath.UI.GameBehaviour);
        GameBehaviourUI gameBehaviourUI = Instantiate(_gameBehaviourUIPrefab,
            _pointButtonsUI.GetComponent<RectTransform>().position,
            Quaternion.identity, _canvas.transform);
        Container.Bind<GameBehaviourUI>().FromInstance(gameBehaviourUI).AsSingle();
        gameBehaviourUI.Constructor(Container.Resolve<SceneService>(), Container.Resolve<SaveService>());
    }

    private void BindPauseService()
    {
        Container.Bind<PauseService>()
            .FromComponentInNewPrefabResource(AssetsPath.Services.PauseService)
            .AsSingle();
    }

    private void BindPlayerHealth()
    {
        _playerPrefab = Resources.Load<PlayerHealth>(AssetsPath.Player.PlayerHealth);
        PlayerHealth playerHealth = Container.InstantiatePrefabForComponent<PlayerHealth>(_playerPrefab,
            _pointPlayer.transform.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<PlayerHealth>().FromInstance(playerHealth).AsSingle();
    }

    private void BindFactoryBonus()
    {
        Container.Bind<FactoryBonus>()
            .FromComponentInNewPrefabResource(AssetsPath.Factory.FactoryBonus)
            .AsSingle();
    }
}