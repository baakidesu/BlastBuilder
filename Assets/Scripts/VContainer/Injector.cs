using UnityEngine;

public class Injector : MonoBehaviour, IGetInjected
{
    public GameObject GetGameObject()
    {
        return gameObject;
    }
}

interface IGetInjected
{
    GameObject GetGameObject();
}