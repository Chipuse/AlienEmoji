using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class DoorBehaviour : MonoBehaviour
{
    public int DestinationScene;
    public bool[] NecessaryBosses;
    public UnityEvent onDoorNotOpenYet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDoorClicked()
    {
        bool pass = true;
        for (int i = 0; i < NecessaryBosses.Length; i++)
        {
            if(GameManager.Instance.BossDefeated.Length > i)
            {
                if (NecessaryBosses[i] == false || GameManager.Instance.BossDefeated[i] == NecessaryBosses[i])
                {
                    
                }
                else
                {
                    
                    pass = false;
                }
            }
        }
        if (pass)
        {
            SceneManager.LoadScene(DestinationScene);
        }
        else
        {
            onDoorNotOpenYet.Invoke();
        }
    }
}
