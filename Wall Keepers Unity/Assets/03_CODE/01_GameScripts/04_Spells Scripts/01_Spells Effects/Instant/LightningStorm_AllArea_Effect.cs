using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStorm_AllArea_Effect : BaseSpellsEffects
{
    [SerializeField] private List<GameObject> go_oneThunder = new List<GameObject>();
    private int i_ligthningsDroped;
    private bool b_lightningInstantiated;
    #region InstantArea Methods
    //Receive target 
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        SetTarget(target);
    }
    private void SetTarget(Vector3 target)
    {
        transform.parent = null;
        gameObject.transform.position = target;
        for(int i = 0; i < go_oneThunder.Count; i++)
        {
            go_oneThunder[i].GetComponent<OneLightningEffect>().SetDmg(f_damage);

        }
    }
    private void Start()
    {
        StartCoroutine(CastInBattlefield());
        
    }

    //Wait until spell effect finish 
    private void FixedUpdate()//1º Loop FireWall
    {
        CheckIfLevelPaused();
        if (b_spellEffectActiveInBattlefield)
        {
            
            //En vez de un whild hacer otra corrutina para que si no ha finalizado lance los rayos 
        }
    }

    internal IEnumerator CastInBattlefield()
    {
        // Reset the lightning counter
        i_ligthningsDroped = 0;

        // Start casting lightnings
        StartCoroutine(CastOneLightning());
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
    }

    internal IEnumerator CastOneLightning()
    {
        if (i_ligthningsDroped < go_oneThunder.Count-1)
        {
            print("Dropped " + i_ligthningsDroped+"//"+ go_oneThunder.Count);
            ++i_ligthningsDroped;
            gameObject.transform.position = GenerateRandomPosition();
            //go_oneThunder[i_ligthningsDroped].GetComponent<OneLightningEffect>().SetDmg(f_damage);
            Instantiate(go_oneThunder[i_ligthningsDroped], transform);
            WaitForSeconds wait_TimeBtwLightning = new WaitForSeconds(1f);
            yield return wait_TimeBtwLightning;
            // Call CastOneLightning again until the condition is met
            StartCoroutine(CastOneLightning());
        }
        else
        {
            print("Finished Casting All Lightnings");
        }
    }
    private Vector3 GenerateRandomPosition()
    {
        // Generate random x and y positions
        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-6f, 12f);

        // Create a random position Vector3
        return new Vector3(randomX, randomY, 0f);

    }
    internal void InstantiateOneLightning()
    {
        if (!b_lightningInstantiated)
        {
           
            b_lightningInstantiated = true;
        }
       
    }

    internal override void HurtEnemies()
    {
        foreach (Enemy enemy in enemiesScripts_List)
        {
            enemy.ReceiveDamage((int)f_damage);
        }
    }
    #endregion

    #region SFX Play Methods
    internal void StopLoopSfx()
    {
        aS_spellAudioSource.Stop();
    }
    #endregion

    #region Collisions
    // When an enemy enters the area, repeatedly hurt the enemy
    private void  OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            AddToEnemiesList(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == i_EnemyLayer)
        {
            RemoveFromEnemiesList(collision.gameObject.GetComponent<Enemy>());
        }
        
    }
    #endregion
}
