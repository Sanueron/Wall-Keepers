using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WallManager.e_OnWallDestroyed += WallManager_e_OnWallDestroyed;
    }

    private void WallManager_e_OnWallDestroyed(object sender, System.EventArgs e)
    {

        //Show results 

        Debug.Log("finish");

    }

   
}
