using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInjector : MonoBehaviour
{
    private IObjectResolver _objectResolver;
    private List<GameObject> _listInjectable;

    [Inject]
    void Construct(IObjectResolver objectResolver)
    {
        _objectResolver = objectResolver;
        InjectGameObjects();
    }

    void InjectGameObjects()
    {
        FindAllInjectors();

        foreach (var injectable in _listInjectable)
        {
            _objectResolver.InjectGameObject(injectable);
        }
    }

    void FindAllInjectors()
    {
        _listInjectable ??= new();
        var allComponents = FindObjectsOfType<Injector>();
        foreach (var comp in allComponents)
        {
            if (!_listInjectable.Contains(comp.GetGameObject()))
            {
                _listInjectable.Add(comp.GetGameObject());
            }
        }
    }
}