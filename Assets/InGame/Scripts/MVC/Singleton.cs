using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monosingleton<T>:MonoBehaviour where T:Monosingleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance=GameObject.FindObjectOfType<T>();
                if(instance==null)
                {
                    var Obj= new GameObject(typeof(T).Name);
                    instance= Obj.AddComponent<T>();
                }
                else
                {
                    instance.Init();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this as T;
            instance.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void Init()
    {
        
    }

    public virtual void Release()
    {

    }
}

public class Singleton<T>where T:new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance== null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}