using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameController", menuName = "Managers/GameController")]
    public class GameController : BaseController
    {
    }

    public enum GameMode
    {
        DeathMatch,
        CaptureFlag
    }
}