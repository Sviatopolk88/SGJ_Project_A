using AssemblyCSharp.Assets.Boss.Script.Interface;
using AssemblyCSharp.Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossFightAI : MonoBehaviour, IBoss , IHittable
{
    public int Health = 100;
    public int Damage = 10;
    public float RunSpeed = 6f;
    public float RestingSpeed = 3.5f;
    public bool isDead => Health <= 0;
    public bool StartFight = false;
    private BossDialogSystem _bossAnimator;
    private TMP_Text _bossHp;
    private GameObject _player;
    public void HitObject(int damage)
    {
        this.Health -= damage;
        //_bossHp.text = Health.ToString();
        if (isDead)
            Destroy(gameObject); // Добавить анимацию смерти
    }

    public void Confusion()
    {
        throw new System.NotImplementedException();
    }

    public void MeleeAttack()
    {
        StartCoroutine(CheckForPlayer());
        
    }

    public void Rage()
    {
        throw new System.NotImplementedException();
    }

    public void RangeAttack()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        _bossAnimator = GetComponent<BossDialogSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //BossAnimationsCheck();
        
        //if (!isDead && StartFight)
        //{
        //    GetHpBar();
        //    GetPlayer();
            
           
        //   // var playerPos = GameObject.Find("Player").transform.position;
        //   // transform.position = Vector3.MoveTowards(transform.position, playerPos, RunSpeed * Time.deltaTime);
        //}
    }

    private void GetHpBar()
    {
        try
        {
            if (_bossHp == null)
            {
                _bossHp = GameObject.Find("BossHP").GetComponent<TMP_Text>();
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    private void GetPlayer()
    {
        try
        {
            if (_player == null)
            {
                //_player = GameObject.Find("Player");
                //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            }
            else
            {
                transform.LookAt(_player.transform);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }


    private void BossAnimationsCheck()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _bossAnimator.RoarIdle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _bossAnimator.RunIdle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _bossAnimator.MeleeAttack();
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _bossAnimator.RangeAttack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _bossAnimator.DeadIdle();
        }
    }

    void Fight()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private IEnumerator CheckForPlayer()
    {
        yield return new WaitForSeconds(1.5f);
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2.5f);
        foreach (var x in colliders)
        {
            if (x.CompareTag("Player"))
            {
                x.transform.GetChild(1).GetComponent<PlayerUI>().SetHP(50);
            }
        }
    }

}
