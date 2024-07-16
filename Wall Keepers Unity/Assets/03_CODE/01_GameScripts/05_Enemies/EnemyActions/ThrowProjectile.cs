using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEditor.SearchService;
using UnityEngine;

public class ThrowProjectile : BaseEnemyAction
{
    private bool b_startProjectileMovement;
    [SerializeField] List<GameObject> listofProjectiles = new List<GameObject>(); 
 
    internal override void TakeAction(GameObject obstacle, GameObject wall)
    {
        if (obstacle != null)
        {
            Obstacle_StaticInstant obstacleStats = obstacle.GetComponent<Obstacle_StaticInstant>();
            if (obstacleStats != null)
            {
                print("pega");
                ActivateOneProjectail(obstacle);
                return;
            }
        }
        if (wall != null)
        {
            WallManager wallStats = wall.GetComponent<WallManager>();
            if (wallStats != null)
            {
                print("pegaWall");
                ActivateOneProjectail(wall);
            }
        }
    }
    private void ActivateOneProjectail( GameObject target)
    {
        print("activaProyectil");
        for (int i = 0; i < listofProjectiles.Count; i++)
        {
            print(b_startProjectileMovement);
            if (!listofProjectiles[i].GetComponent<EnemyProjectile>().b_startProjectileMovement)
            {
                print(i+"Activado");
                //Set Projectile stats 
                listofProjectiles[i].GetComponent<EnemyProjectile>().SetStats(gameObject.transform.position, target, f_projectailSpeed, i_damage);
                listofProjectiles[i].SetActive(true);
            }
            else
            {
                print("No more projectails");
            }
        }
    }
    internal void OnDisable()//Reset Lightnings
    {
        for (int i = 0; i < listofProjectiles.Count; i++)
        {
            listofProjectiles[i].gameObject.GetComponent<EnemyProjectile>().ResetProjectile();
        }
    }


}
