using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMeleeAttack : BaseEnemyAction
{

    #region Action
    internal override void TakeAction(WallManager wall, Obstacle_StaticInstant staticObstacle)
    {
        // Attack single target in range
        Attack();

        if(wall != null)
        {

            // hace daño al wall
        }


        if (staticObstacle != null)
        {

            // hace daño al obstaculo
        }



       

    }

    internal void Attack()// diferenciar muralla de obstaculo o obsmoving 
    {
        print("Attack"); 
    }
    #endregion
}
