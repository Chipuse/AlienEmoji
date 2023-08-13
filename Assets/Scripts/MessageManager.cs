using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public NPCAnswer[] LoadFinalBossAnswers(string nPCName, string[] dialogOptions)
    {
        NPCAnswer[] answerPackage;
        answerPackage = new NPCAnswer[dialogOptions.Length];
        string tempFileLocation = "";
        string tempAudioFileLocation = "";
        for (int i = 0; i < dialogOptions.Length; i++)
        {
            tempFileLocation = "";
            tempFileLocation = nPCName + "/Text/" + dialogOptions[i];
            answerPackage[i].type = i;
            string textFromFile = Resources.Load<TextAsset>(tempFileLocation).text;

            textFromFile = textFromFile.Replace("\\n", "/n");
            textFromFile = textFromFile.Replace("\n", "");
            textFromFile = textFromFile.Replace("/n", "\n");

            if (true)
            {
                answerPackage[i].textBoxes = CreateTextBoxFromString(textFromFile);
            }
            else
            {
                Debug.LogError("File does not exist: " + tempFileLocation);
            }
            tempAudioFileLocation = "";
            tempAudioFileLocation = "Audio/" + nPCName + "/" + dialogOptions[i];
            for (int j = 0; j < answerPackage[i].textBoxes.Length; j++)
            {
                AudioClip audioFromFile = Resources.Load<AudioClip>(tempAudioFileLocation + j.ToString());
                answerPackage[i].textBoxes[j].voiceOver = audioFromFile;
            }
        }

        return answerPackage;
    }
    public NPCAnswer[] LoadBossAnswers(string nPCName, string[] dialogOptions, string rName,string oName)
    {
        NPCAnswer[] answerPackage;
        answerPackage = new NPCAnswer[dialogOptions.Length];
        string tempFileLocation = "";
        string tempAudioFileLocation = "";
        for (int i = 0; i < dialogOptions.Length; i++)
        {
            tempFileLocation = "";
            tempFileLocation = nPCName + "/Text/" + dialogOptions[i];
            answerPackage[i].type = i;
            string textFromFile = Resources.Load<TextAsset>(tempFileLocation).text;
            
            textFromFile = textFromFile.Replace("\\n", "/n");
            textFromFile = textFromFile.Replace("\n", "");
            textFromFile = textFromFile.Replace("/n", "\n");
            textFromFile = textFromFile.Replace(oName+"::", rName+"::");

            
            
            if (true)
            {
                answerPackage[i].textBoxes = CreateTextBoxFromString(textFromFile);
            }
            else
            {
                Debug.LogError("File does not exist: " + tempFileLocation);
            }
            tempAudioFileLocation = "";
            tempAudioFileLocation = "Audio/" + nPCName + "/" + dialogOptions[i];
            for (int j = 0; j < answerPackage[i].textBoxes.Length; j++)
            {
                AudioClip audioFromFile = Resources.Load<AudioClip>(tempAudioFileLocation + j.ToString());
                answerPackage[i].textBoxes[j].voiceOver = audioFromFile;
            }
        }
        
        return answerPackage;
    }
    public NPCAnswer[] LoadNPCAnswers(string nPCName, string rName, string oName)
    {
        NPCAnswer[] answerPackage;
        answerPackage = new NPCAnswer[7];
        string tempFileLocation = "";
        string tempAudioFileLocation = "";
        for (int i = 0; i < answerPackage.Length; i++)
        {
            tempFileLocation = "";
            tempFileLocation = nPCName + "/Text/"+ (MessageIDs)i;
            answerPackage[i].type = i;            
            string textFromFile = Resources.Load<TextAsset>(tempFileLocation).text;
            textFromFile = textFromFile.Replace("\\n", "/n");
            textFromFile = textFromFile.Replace("\n", "");
            textFromFile = textFromFile.Replace("/n", "\n");
            textFromFile = textFromFile.Replace(oName+"::", rName+"::");
            


            
            if (true)
            {
                answerPackage[i].textBoxes = CreateTextBoxFromString(textFromFile);
                
            }
            else
            {
                Debug.LogError("File does not exist: " + tempFileLocation);
            }
            tempAudioFileLocation = "";
            tempAudioFileLocation = "Audio/" + nPCName + "/" + (MessageIDs)i;
            for (int j = 0; j < answerPackage[i].textBoxes.Length; j++)
            {
                AudioClip audioFromFile = Resources.Load<AudioClip>(tempAudioFileLocation+j.ToString());
                answerPackage[i].textBoxes[j].voiceOver = audioFromFile;
            }
        }

        return answerPackage;
    }

    TextBox[] CreateTextBoxFromString(string Input)
    {
        TextBox[] textBoxes;
        List<TextBox> tempList = new List<TextBox>();
        
        string source = Input;
        string[] stringSeparators = new string[] { ">>" };
        string[] seperatedTextBoxes;
        seperatedTextBoxes = source.Split(stringSeparators, StringSplitOptions.None);
        string[] dividedNameAndBody;
        TextBox tempTextBox;
        foreach (var item in seperatedTextBoxes)
        {
            
            tempTextBox = new TextBox();
            dividedNameAndBody = item.Split(new string[] { "::" }, StringSplitOptions.None);
            
            if(dividedNameAndBody.Length >= 2)
            {
                tempTextBox.speakerName = dividedNameAndBody[0];
                tempTextBox.textBody = dividedNameAndBody[1];
                tempList.Add(tempTextBox);
            }
            else
            {
                tempTextBox.speakerName = "empty Name";
                tempTextBox.textBody = "empty TextBody";
                
            }
                       
        }
        textBoxes = tempList.ToArray();
        for(int i = 0; i< textBoxes.Length; i++)
        {
            //load audio and put it into the textbox[i].audio
        }
        
        return textBoxes;
    }
    

}

