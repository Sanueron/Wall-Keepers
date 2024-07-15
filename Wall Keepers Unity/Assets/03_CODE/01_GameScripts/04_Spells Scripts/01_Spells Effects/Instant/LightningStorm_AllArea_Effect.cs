using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningStorm_AllArea_Effect : BaseSpellsEffects
{
    #region Variables
    [SerializeField] private List<GameObject> go_oneThunder = new List<GameObject>();
    private int i_ligthningsDroped;
    private bool b_lightningInstantiated;
    #endregion
    #region BaseSpells Methods
    public override void ActivateEffect(Vector3 starPosition, Vector3 target)
    {
        SetTarget(target);
    }
    private void SetTarget(Vector3 target)
    {
        transform.parent = null;
        gameObject.transform.position = target;
    }
    #endregion
    #region Unity Life Cycle
    private void OnEnable()
    {
        for (int i = 0; i < go_oneThunder.Count; i++)
        {
            go_oneThunder[i].GetComponent<OneLightningEffect>().SetDmg(f_damage);
        }
    }
    private void Start()
    {
        StartCoroutine(CastInBattlefield());   
    }
   
    private void FixedUpdate()//1º Loop FireWall
    {
        CheckIfLevelPaused();
        if (b_spellEffectActiveInBattlefield)
        {
            //En vez de un whild hacer otra corrutina para que si no ha finalizado lance los rayos 
        }
    }
    #endregion
    #region LStorm Methods
    internal IEnumerator CastInBattlefield()//Primero casteamos la tormenta durante la duracion determinada 
    {
        // Reset the lightning counter
        i_ligthningsDroped = 0;
        // Start casting lightnings
       //Lanzamos una corrutina que dura 1 segundo, que se repetira hasta agotar los rayos 
        WaitForSeconds wait_durationInBattlefield = new WaitForSeconds(f_duration);
        yield return wait_durationInBattlefield;
    }
    internal void CastOneLightning()
    {
            gameObject.transform.position = GenerateRandomPosition();
            go_oneThunder[i_ligthningsDroped].GetComponent<OneLightningEffect>().SetDmg(f_damage);
            Instantiate(go_oneThunder[i_ligthningsDroped], transform);
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
