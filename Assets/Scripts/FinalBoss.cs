using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FinalBoss : Boss
{
    int numberOfAnswersWrong = 0;
    bool lastGivenAnswer = false;
    public int numberOfAnswersNeedToBeWrong = 4;
    public UnityEvent OnCarlaBesiegt;
    public override void OnChosenForConversation()
    {
        GameManager.Instance.animator.SetTrigger("NPCStart");
        base.OnChosenForConversation();
        GameManager.Instance.animator.ResetTrigger("BossStart");
        lastGivenAnswer = false;
        pass = true;
        numberOfAnswersWrong = 0;
    }
    public override void OnProceededConversation(int WordID)
    {
        if (rightAnswers.Length > currentWord)
        {
            
            if (rightAnswers[currentWord] != WordID)
            {
                pass = false;
                numberOfAnswersWrong++;
                Debug.Log("wrong answer");
                
                currentDialog = 0;
                if(lastGivenAnswer)
                    currentWord++;
                currentWord++;
                currentWord++;
                Next();
                lastGivenAnswer = false;
            }
            else
            {
                
                currentDialog = 0;
                
                if (lastGivenAnswer)
                    currentWord++;
                currentWord++;
                Next();
                lastGivenAnswer = true;
            }
            

        }
        else
        {
            pass = false;

        }
        if (currentWord >= DialogOptions.Length - 2)
        {

            if (numberOfAnswersWrong < numberOfAnswersNeedToBeWrong)
            {

                defeated = true;
                currentWord = DialogOptions.Length-2;
            }

            else
            {

                currentWord = DialogOptions.Length - 1;
            }
            currentDialog = 0;
            Next();
        }
        
        Debug.Log(pass);
    }

    public void OnFinalBossEnd()
    {
        if(numberOfAnswersWrong >= numberOfAnswersNeedToBeWrong)
        {
            Debug.Log("Credits?");
            OnCarlaBesiegt.Invoke();
        }
    }
}
