using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public bool[] BossDefeated = new bool[]
    {
        false, //Bullies
        false, //Nerd
        false, //Teacher1
        false, //Teacher2
        false, //Dean
    };
    public void DefeatBoss(int bossID, bool defeated)
    {
        if (BossDefeated.Length > bossID)
            BossDefeated[bossID] = defeated;
    }

    public bool[] WordsLearned = new bool[]
    {
        false, //Introduction
        false, //Heart
        false, //Light
        false, //Coffee
        false, //Chicken
        false, //Knife
        false  //Mountain
    };
    public void LearnWord(int wordID, bool learned)
    {
        if (WordsLearned.Length > wordID)
            WordsLearned[wordID] = learned;
    }
    public Button[] WordButtons;
    public GameObject ButtonPad;

    public NPC activeConversation;
    public int CurrentGameMode;
    public List<NPC> npcs;
    public Image conImage;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        npcs = new List<NPC>();
        CurrentGameMode = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (activeConversation != null && CurrentGameMode == (int)GameMode.Conversation)
            {
                activeConversation.OnMouseButtonDown();
            }
        }
            
    }

    public void EngageConversation(NPC partner)
    {
        ButtonPad.SetActive(false);
        //SetPicture
        activeConversation = partner;
        CurrentGameMode = (int)GameMode.Conversation;
        if(partner.conImage != null)
            conImage.sprite = partner.conImage;
        
        activeConversation.OnChosenForConversation();

    }
    
    public void ProceedConversation(int WordID)
    {
        ButtonPad.SetActive(false);
        CurrentGameMode = (int)GameMode.Conversation;
        activeConversation.OnProceededConversation(WordID);
    }
    public void EngageOverworld()
    {
        animator.SetTrigger("End");
        activeConversation.onEndOfConversation.Invoke();
        ButtonPad.SetActive(false);
        //RemovePicture
        activeConversation = null;
        CurrentGameMode = (int)GameMode.Overworld;
        MainTextBox.Instance.DisplayText("","");
        MainTextBox.Instance.StopVoiceOver();
    }
    public void EngageWordAction()
    {
        ButtonPad.SetActive(true);
        CurrentGameMode = (int)GameMode.WordAction;
        for (int i = 0; i < WordsLearned.Length; i++)
        {
            if (WordButtons.Length > i)
            {
                WordButtons[i].gameObject.SetActive(WordsLearned[i]);
            }
        }
    }
    public void ButtonLeaveCon()
    {
        EngageOverworld();
    }
    public void WordButton(int wordID)
    {
        ProceedConversation(wordID);
    }

    void OnNewSceneLoaded()
    {
        activeConversation = null;
        npcs = new List<NPC>();
    }
}
enum MessageIDs
{
    Introduction = 0,
    Heart = 1,
    Light = 2,
    Coffee = 3,
    Chicken = 4,
    Knife = 5,
    Mountain = 6
};
enum GameMode
{
    Conversation = 0,
    Overworld = 1,
    WordAction = 2
};

enum Bosses
{
    Bullies = 0,
    Nerd = 1,
    Teacher1 = 2,
    Teacher2 = 3,
    Dean = 4
}