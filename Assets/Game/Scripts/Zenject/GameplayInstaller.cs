using UnityEngine;
using Zenject;
public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IControllable>().To<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CharacterController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GamePadInputController>().FromComponentInHierarchy().AsSingle();
    }
}
