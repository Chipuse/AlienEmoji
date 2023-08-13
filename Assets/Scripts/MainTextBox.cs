using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KoganeUnityLib;
using TMPro;

public class MainTextBox : MonoBehaviour
{
    public static MainTextBox Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
    }
    public TMP_Typewriter textBoxTypeWriter;
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI nameBox;
    public AudioSource voiceOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayVoiceOver(AudioClip audio)
    {
        if(audio != null)
        {
            voiceOver.clip = audio;
            voiceOver.Play();
        }
    }
    public void StopVoiceOver()
    {
        if (voiceOver.clip != null && voiceOver.isPlaying)
            voiceOver.Stop();
    }

    public void DisplayText(string speaker, string text, NPC npc)
    {
        nameBox.text = speaker;
        
        textBoxTypeWriter.Play
        (
            text: text,
            speed: 30,
            onComplete: () =>npc.DialogBehavior()
        );
    }
    public void DisplayText(string speaker, string text)
    {
        nameBox.text = speaker;

        textBoxTypeWriter.Play
        (
            text: text,
            speed: 30,
            onComplete: () => Debug.Log("")
        );
    }

    void ClearTextBox()
    {

    }
 
}
