using Zenject;

public class DefaultInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ISlotMachine>().To<SlotMachine>().AsSingle();
        Container.Bind<IGameManager>().To<GameManager>().AsSingle();
        Container.Bind<IDataManager>().To<DataManager>().AsSingle();
    }
}