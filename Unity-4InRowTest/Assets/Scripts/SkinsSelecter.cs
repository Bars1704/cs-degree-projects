using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SkinsSelecter : MonoBehaviour
{
    public List<PlayerSkin> AllSkins;
    public GameObject butttonPrefab;
    public GameObject panel;

    PlayerSkin RandomSkin;

    Button previousButton;

    bool IsFirstSelect;

    public Animator secondButtonAnimator;

    public Text firstPlayerName;
    public Text secondPlayerName;
    public Image firstImage;
    public Image secondImage;
    void Start()
    {
        foreach (var skin in AllSkins)
        {
            var button = Instantiate(butttonPrefab, panel.transform);
            button.GetComponent<Image>().sprite = skin.roundSprite;
            var Changer = button.GetComponent<SkinChangerButtonScript>();
            Changer.skin = skin;
            Changer.parent = this;
            button.GetComponent<RectTransform>().localScale = GetScale(skin.roundSprite) * skin.SelectButtonScale;
            Debug.Log(button.GetComponent<RectTransform>().localScale);
        }
        RandomSkin = AllSkins.Last();
        GameManager.firstPlayerSkin = GetRandomSkin();
        GameManager.secondPlayerSkin = GetRandomSkin();
    }
    public void ChangeSkin(PlayerSkin skin, Button sender)
    {
        Text currentText = !IsFirstSelect ? firstPlayerName : secondPlayerName;
        Image currentImage = !IsFirstSelect ? firstImage : secondImage;
        currentText.text = skin.name;
        currentImage.sprite = skin.roundSprite;
        currentImage.SetNativeSize();
        currentImage.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.8f, 1);

        if (previousButton != null)
        {
            previousButton.interactable = true;
        }
        sender.interactable = false;
        previousButton = sender;

        if (!IsFirstSelect)
            GameManager.firstPlayerSkin = skin.name != RandomSkin.name ? skin : GetRandomSkin();
        else
            GameManager.secondPlayerSkin = skin.name != RandomSkin.name ? skin : GetRandomSkin();
    }
    PlayerSkin GetRandomSkin()
    {
        var result = AllSkins[Random.Range(0, AllSkins.Count)];
        if (result.name == RandomSkin.name) return GetRandomSkin();
        else if (IsFirstSelect && GameManager.firstPlayerSkin == result) return GetRandomSkin();
        else if (!IsFirstSelect && GameManager.secondPlayerSkin == result) return GetRandomSkin();
        else return result;
    }
    Vector3 GetScale(Sprite sprite)
    {
        var rect = sprite.rect;
        if (rect.width > rect.height)
        {
            return new Vector3(rect.width / rect.height, 1, 1);
        }
        else
        {
            return new Vector3(1, rect.height / rect.width, 1);
        }
    }
    public void SumbitChanging()
    {
        if (!IsFirstSelect)
        {
            IsFirstSelect = true;
            secondButtonAnimator.enabled = true;
            previousButton = null;
        }
        else
        {
            MainMenuNav.GoNextScene();
        }
    }
}
