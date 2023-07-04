using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamUISelect : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] Button Button;

    public void SetImage(Sprite image)
    {
        Image.sprite = image;
    }

    public void SetButton(bool Isactive)
    {
        Button.gameObject.SetActive(Isactive);
    }
    public void SetButtonInteractble(bool Isactive)
    {
        Button.interactable = Isactive;
    }
    
}
