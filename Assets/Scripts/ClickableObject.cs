using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ClickableObject : MonoBehaviour
{
    bool highlighted = false;
    public UnityEvent onClick;
    
    public Sprite Normal;
    public Sprite Highlighted;
    public SpriteRenderer sRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
        if (GetComponent<SpriteRenderer>() != null)
            sRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        highlighted = true;
        //animator.SetBool("Highlighted", highlighted);
        sRenderer.sprite = Highlighted;
    }
    private void OnMouseExit()
    {
        highlighted = false;
        sRenderer.sprite = Normal;
        //animator.SetBool("Highlighted", highlighted);
    }
    private void OnMouseUpAsButton()
    {
        if(GameManager.Instance.CurrentGameMode == (int)GameMode.Overworld)
            onClick.Invoke();
    }
}
