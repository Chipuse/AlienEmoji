using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    public bool forceConversationOnStart = false;
    public Sprite conImage;


    public string ownName;
    public string oldName;
    public string replaceName;


    public string textString;
    public NPCAnswer[] ownAnswers;
    protected int currentDialog = 0;
    
    public int currentWord = (int)MessageIDs.Introduction;
    protected bool textBoxFinished = false;

    public UnityEvent onStartOfConversation;
    public UnityEvent onEndOfConversation;

    // Start is called before the first frame update
    void Start()
    {
        CreateAnswers();
        GameManager.Instance.npcs.Add(this);
        if (forceConversationOnStart)
        {
            OnClickInOverworld();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public virtual void CreateAnswers()
    {
        
        ownAnswers = MessageManager.Instance.LoadNPCAnswers(ownName,replaceName,oldName);
    }

    public void DialogBehavior()
    {
        StartCoroutine("DoCheck");
    }

    public void OnMouseButtonDown()
    {
        //&& !MainTextBox.Instance.voiceOver.isPlaying
        if (textBoxFinished )
        {
            Next();
        }
        else
        {
            SetTextBoxFinished(true);
            MainTextBox.Instance.textBoxTypeWriter.Skip();                     
        }        
    }
    public void SetTextBoxFinished(bool newStatus)
    {
        textBoxFinished = newStatus;
    }
    IEnumerator DoCheck()
    {          
        yield return new WaitForSeconds(.1f);
        SetTextBoxFinished(true);
    }
    public void Next()
    {
        Debug.Log(currentWord);
        if (currentDialog >= ownAnswers[currentWord].textBoxes.Length)
        {
            ReachedEndOfText();
            return;
        }
        SetTextBoxFinished(false);
        MainTextBox.Instance.DisplayText(ownAnswers[currentWord].textBoxes[currentDialog].speakerName, ownAnswers[currentWord].textBoxes[currentDialog].textBody, this);
        MainTextBox.Instance.StopVoiceOver();
        if (ownAnswers[currentWord].textBoxes[currentDialog].voiceOver != null)
        {
            MainTextBox.Instance.PlayVoiceOver(ownAnswers[currentWord].textBoxes[currentDialog].voiceOver);
        }
            
        currentDialog++;
        //audio
    }

    public virtual void ReachedEndOfText()
    {
        
        if(currentWord >= ownAnswers.Length)
        {
            GameManager.Instance.EngageOverworld();
            
        }
        else
        {
            currentDialog = 0;
            GameManager.Instance.EngageWordAction();
        }
    }

    public void OnClickInOverworld()
    {
        if(GameManager.Instance.CurrentGameMode == (int)GameMode.Overworld)
        {
            //Engage Conversation!!!
            GameManager.Instance.EngageConversation(this);
        }
    }
    public virtual void OnChosenForConversation()
    {
        GameManager.Instance.animator.SetTrigger("BossStart");
        onStartOfConversation.Invoke();
        currentDialog = 0;
        currentWord = 0;
        StartCoroutine("WaitForAnimation");
        
    }
    
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        Next();
    }
    public virtual void OnProceededConversation(int WordID)
    {
        currentDialog = 0;
        currentWord = WordID;
        Next();
    }
}

public struct NPCAnswer
{
    public int type;
    public TextBox[] textBoxes;
}


public struct TextBox
{
    public string speakerName;
    public string textBody;
    //audio
    public AudioClip voiceOver;
}
