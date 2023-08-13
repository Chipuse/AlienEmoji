using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public Scene StartScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartCredits()
    {
        GetComponent<Animator>().SetTrigger("goCredits");
    }

    public void EndCredits()
    {
        SceneManager.LoadScene("Initiater");
        GameManager.Instance.WordsLearned = new bool[] {true,true,true,true,false,false,false };
        GameManager.Instance.BossDefeated = new bool[] { false, false, false, false, false };
    }
}
