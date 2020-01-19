using UnityEngine;


public class EnviromentManager : MonoBehaviour, IRestart
{
    [SerializeField] WoodWall woodWall;

    public void Restart()
    {
        woodWall.Restart();
    }
}