using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : NPC
{
    public string[] DialogOptions;
    public int[] rightAnswers;
    protected bool pass = true;
    protected bool defeated = false;
    public override void CreateAnswers()
    {
        
        //GameManager.Instance.activeConversation = this;
        ownAnswers = MessageManager.Instance.LoadBossAnswers(ownName, DialogOptions,replaceName,oldName);
        
        //MainTextBox.Instance.DisplayText(ownAnswers[0].textBoxes[currentDialog].speakerName, ownAnswers[0].textBoxes[currentDialog].textBody, this);
    }

    public override void OnChosenForConversation()
    {
        GameManager.Instance.animator.SetTrigger("NPCStart");
        base.OnChosenForConversation();
        GameManager.Instance.animator.ResetTrigger("BossStart");
        pass = true;
    }

    public override void OnProceededConversation(int WordID)
    {
        if(rightAnswers.Length > currentWord)
        {
            if(rightAnswers[currentWord] != WordID)
            {
                pass = false;
                Debug.Log("wrong answer");
            }
            currentDialog = 0;
            currentWord++;
            Next();
        }
        else
        {
            pass = false;
            
        }
        if (currentWord == rightAnswers.Length)
        {
            
            if (pass)
            {
                
                defeated = true;
                currentWord = rightAnswers.Length;
            }
                
            else{
                
                currentWord = rightAnswers.Length + 1;
            }
            currentDialog = 0;
            Next();
        }
        Debug.Log(pass);
    }
    public override void ReachedEndOfText()
    {
        if (currentWord >= rightAnswers.Length)
        {
            
            GameManager.Instance.EngageOverworld();
            return;
        }
            
        base.ReachedEndOfText();
    }
    public void LearnWordOnSuccess(int wordID)
    {
        if(defeated)
            GameManager.Instance.LearnWord(wordID, true);
    }
    public void DefeatBossOnSuccess(int BossID)
    {
        if(defeated)
            GameManager.Instance.DefeatBoss(BossID, true);
    }
}
