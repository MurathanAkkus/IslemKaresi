using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NoteButton : Selectable, IPointerClickHandler
{
    public Sprite on_image;    
    public Sprite off_image;
    private bool active;

    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
