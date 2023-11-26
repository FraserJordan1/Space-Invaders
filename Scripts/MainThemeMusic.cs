using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThemeMusic : MonoBehaviour
{
    private static MainThemeMusic instance = null;

    public static MainThemeMusic Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
