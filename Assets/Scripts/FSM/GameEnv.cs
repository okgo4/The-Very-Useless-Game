using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnv
{
    private static GameEnv instance;
    private List<GameObject> wps = new List<GameObject>();
    public List<GameObject> Wps { get { return wps; } }

    public static GameEnv Singleton 
    {
        get
        {
            if(instance == null)
            {
                instance = new GameEnv();
                instance.wps.AddRange(
                    GameObject.FindGameObjectsWithTag("wp")
                );
            }
            return instance;
        }
    }
}
