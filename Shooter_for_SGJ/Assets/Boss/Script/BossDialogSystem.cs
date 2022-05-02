using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogSystem : MonoBehaviour
{
    public bool Ask;
    public bool Answer;
    public bool WrongAnswer;
    public bool Run;
    public bool Melee;
    public bool Range;
    public bool Dead;
    private bool _ask;
    private bool _answer;
    //private bool _melee;
    //private bool _range;
    //private bool _dead;
    //private bool _run;
    private Animator _bossAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _bossAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AskQuestion(bool Ask, bool Answer)
    {
        _answer = Answer;
        _ask = Ask;
        _bossAnimator.SetBool("Ask",_ask);
        _bossAnimator.SetBool("Answer", _answer);
    }

    public void AnswerQuestion(bool Answer)
    {
        _answer = Answer;
        _bossAnimator.SetBool("Answer",_answer);
    }

    public void SittingIdle(bool Ask, bool Answer)
    {
        _answer=Answer;
        _ask=Ask;
        _bossAnimator.SetBool("Ask", _ask);
        _bossAnimator.SetBool("Answer", _answer);
    }

    public void MeleeAttack()
    {
        
        _bossAnimator.SetLayerWeight(0, 0);
        _bossAnimator.SetLayerWeight(1, 1);
        _bossAnimator.SetBool("Melee",true);
        _bossAnimator.SetBool("Range",false);
        _bossAnimator.SetBool("Run",false);
        _bossAnimator.SetBool("Dead",false);
    }

    public void RangeAttack()
    {
 
        _bossAnimator.SetLayerWeight(0, 0);
        _bossAnimator.SetLayerWeight(1, 1);
        _bossAnimator.SetBool("Range", true);
        _bossAnimator.SetBool("Melee", false);
        _bossAnimator.SetBool("Run", false);
        _bossAnimator.SetBool("Dead", false);
    }

    public void RunIdle()
    {
        
        _bossAnimator.SetLayerWeight(0, 0);
        _bossAnimator.SetLayerWeight(1, 1);
        _bossAnimator.SetBool("Range",false);
        _bossAnimator.SetBool("Melee", false);
        _bossAnimator.SetBool("Run", true);
        _bossAnimator.SetBool("Dead", false);
    }
    public void RoarIdle()
    {
       
        _bossAnimator.SetLayerWeight(0, 0);
        _bossAnimator.SetLayerWeight(1, 1);
        _bossAnimator.SetBool("Range", false);
        _bossAnimator.SetBool("Melee", false);
        _bossAnimator.SetBool("Run", false);
        _bossAnimator.SetBool("Dead", false);
    }

    public void DeadIdle()
    {
        
        _bossAnimator.SetLayerWeight(0, 0);
        _bossAnimator.SetLayerWeight(1, 1);
        _bossAnimator.SetBool("Range", false);
        _bossAnimator.SetBool("Melee", false);
        _bossAnimator.SetBool("Run", false);
        _bossAnimator.SetBool("Dead", true);
    }
}
