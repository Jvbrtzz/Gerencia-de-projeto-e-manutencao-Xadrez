using UnityEngine;
using static CommonData;

//Singleton class;
//Automatically instantiates a Manager;
//Can use a Prefab or an Empty GameObject;

public abstract class Singleton : MonoBehaviour
{
    public static Singleton Init(Singleton singleton = null, string managerType = "")
    {
        GameObject go;
        if (managerType != "")
        {
            go = Instantiate(Resources.Load(Common.singletonPrefabFolder + managerType)) as GameObject;
        }
        else
        {
            go = new GameObject();
            go.AddComponent(singleton.GetType());
        }

        go.name = managerType == "" ? singleton.GetType().Name : managerType;

        Debug.Log(string.Format("{0} successfully created!", go.name));

        DontDestroyOnLoad(go);

        return go.GetComponent<Singleton>();
    }

    public abstract void Setup();
}
