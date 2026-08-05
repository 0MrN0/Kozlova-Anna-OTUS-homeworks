using UnityEngine;

namespace ShootEmUp
{
    public sealed class GameManager
    {
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }
    }
}