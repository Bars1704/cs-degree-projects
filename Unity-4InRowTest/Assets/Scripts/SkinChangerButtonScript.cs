using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinChangerButtonScript : MonoBehaviour
{
    [HideInInspector]
    public SkinsSelecter parent;
    [HideInInspector]
    public PlayerSkin skin;
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
    }
    public void ChangeCkinButton()
    {
        parent.ChangeSkin(skin, button);
    }
}
