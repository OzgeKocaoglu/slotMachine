using UnityEngine;
using Zenject;

public class DefaultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISlotMachine>().To<SlotMachine>().AsSingle();
        Container.Bind<IGameManager>().To<GameManager>().AsSingle();
    }
}