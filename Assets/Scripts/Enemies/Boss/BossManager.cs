using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    #region Boss States Changer Variables
        public BossBaseState bossCurrentState;
        public Animator bossAnimator;
        public int bossCurrentHP;
        public GameObject LifeBarObj;
        public GameObject player;
        public GameObject attackGrassPrefab;
        public GameObject attackSporePrefab;
        public Transform attackGrassMaxRange;
        public Transform attackGrassMinRange;
        public Transform attackSporeSpawnPositionTransform;
        public Transform groundPositionTransform;
        public AudioSource backgroundMusic;
        public AudioSource soundEffects;
    #endregion

    #region Boss Data
        public BossData bossData;
    #endregion

    #region Boss States Instantiation
        public BossBeforeSpawnState bossBeforeSpawn = new BossBeforeSpawnState();
        public BossSpawnState bossSpawn = new BossSpawnState();
        public BossIdleState bossIdle = new BossIdleState();
        public BossAttackSporeState bossAttackSpore = new BossAttackSporeState();
        public BossAttackGrassState bossAttackGrass = new BossAttackGrassState();
        public BossDeathState bossDeath = new BossDeathState();
    #endregion

    void Awake()
    {
        bossCurrentHP = bossData.bossMaxHP;

        bossCurrentState = bossBeforeSpawn;
        bossCurrentState.EnterState(this);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bossCurrentState.UpdateState(this);
    }

    void FixedUpdate()
    {
        bossCurrentState.FixedUpdateState(this);
    }

    public void OnRangeEnter2D(Collider2D other)
    {
        bossCurrentState.OnRangeEnter2D(this, other);
    }

    public void OnRangeExit2D(Collider2D other)
    {
        bossCurrentState.OnRangeExit2D(this, other);
    }

    public void OnHitBoxEnter2D(Collider2D other)
    {
        bossCurrentState.OnHitBoxEnter2D(this, other);
    }

    public void OnHitBoxStay2D(Collider2D other)
    {
        bossCurrentState.OnHitBoxStay2D(this, other);
    }

    public void SwitchState(BossBaseState newState)
    {
        newState.isInvunerable = bossCurrentState.isInvunerable;
        newState.invunerableTotalTimer = bossCurrentState.invunerableTotalTimer;
        newState.invunerableFlashTimer = bossCurrentState.invunerableFlashTimer;
        newState.spriteVisibility = bossCurrentState.spriteVisibility;
        newState.attackGrassTimer = bossCurrentState.attackGrassTimer;
        newState.attackSporeTimer = bossCurrentState.attackSporeTimer;
        bossCurrentState = newState;
        bossCurrentState.EnterState(this);
    }

    public void TakeDamage(int damage)
    {
        if(!bossCurrentState.isInvunerable && bossCurrentState != bossBeforeSpawn && bossCurrentState != bossSpawn && bossCurrentState != bossDeath)
        {
            soundEffects.PlayOneShot(bossData.bossDamageSounds[UnityEngine.Random.Range(0, bossData.bossDamageSounds.Length)]);
            bossCurrentHP -= damage;
            bossCurrentState.isInvunerable = true;
            bossCurrentState.invunerableTotalTimer = 0;
            bossCurrentState.invunerableFlashTimer = 0;
            bossCurrentState.spriteVisibility = true;
            if (bossCurrentHP <= 0)
            {
                bossCurrentHP = 0;
                SwitchState(bossDeath);
            }
            UpdateLifeBar();
        }
    }

    public void SpawnSpores()
    {
        for(int i=0; i<bossData.bossAttackSporeNumberOfProjectilesPerShoot; i++)
        {   
            var spore=Instantiate(attackSporePrefab, attackSporeSpawnPositionTransform.position, quaternion.identity);
            spore.GetComponent<SporeManager>().yInitialPosition = attackSporeSpawnPositionTransform.position.y;
            spore.GetComponent<SporeManager>().yFinalPosition = groundPositionTransform.position.y;
            spore.GetComponent<SporeManager>().xInitialPosition = attackSporeSpawnPositionTransform.position.x;
            spore.GetComponent<SporeManager>().xFinalPosition = player.transform.position.x;
        }
    }

    public void SpawnGrass()
    {
        for(int i=0; i<bossData.bossAttackGrassNumberOfGrass; i++)
        {   
            var randomXPosition = attackGrassMaxRange.position.x - attackGrassMinRange.position.x;
            randomXPosition = UnityEngine.Random.Range(0, randomXPosition);
            Vector3 temp = attackGrassMinRange.position;
            temp.y = groundPositionTransform.position.y - bossData.bossAttackGrassSize/2;
            temp.x += randomXPosition;
            var grass=Instantiate(attackGrassPrefab, temp, quaternion.identity);
        }
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateLifeBar()
    {
        LifeBarObj.transform.Find("GreenPortion_N").transform.localScale = new Vector3((float)bossCurrentHP/bossData.bossMaxHP, LifeBarObj.transform.Find("GreenPortion_N").transform.localScale.y, LifeBarObj.transform.Find("GreenPortion_N").transform.localScale.z);
    }
}
