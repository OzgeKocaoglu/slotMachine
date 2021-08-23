using UnityEngine;
using Zenject;

public class MainZenject : MonoBehaviour
{
    private IGameManager _gameManager;

   [Inject]
   public void Setup(IGameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void Start()
    {
        _gameManager.StartGame();
    }
}
