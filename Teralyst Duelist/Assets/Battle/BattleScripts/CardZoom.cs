using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardZoom : MonoBehaviour
{
    public TextMeshProUGUI ZoomText;
    private TextMeshProUGUI zoomText;
    private string ZoomDesc;
    public GameObject TextBackground;
    public GameObject ZoomCard;
    private GameObject zoomCard;
    private Sprite ZoomSprite;
    private CardIdentification cardIdentification;
    [SerializeField] private GameObject PlayerCard;
    private GameObject ZoomCanvas;
    private int XOffset;
    private int YOffset;
    // Start is called before the first frame update
    void Start()
    {
        ZoomCanvas = GameObject.Find("ZoomCanvas");
        cardIdentification = gameObject.GetComponent<CardIdentification>();
    }

    // Update is called once per frame
    public void OnHoverEnter()
    {
        if (Input.mousePosition.x >= 960)
        {
            XOffset = -200;
        }
        else
        {
            XOffset = 200;
        }
        if (Input.mousePosition.y >= 600){
            YOffset = -200;
        }
        else{
            YOffset = 200;
        }
        ZoomDesc = cardIdentification.Description;
        zoomCard = Instantiate(ZoomCard, new Vector2(Input.mousePosition.x + XOffset, Input.mousePosition.y + YOffset), Quaternion.identity);
        //zoomCard = Instantiate(ZoomCard, new Vector2(Input.mousePosition.x + XOffset, Input.mousePosition.y + YOffset), Quaternion.identity);
        zoomCard.GetComponent<Image>().sprite = GetComponent<CardIdentification>().CardImage.sprite;
        //zoomCard.transform.SetParent(PlayerCard.transform, false);
        zoomCard.transform.SetParent(ZoomCanvas.transform, true);
        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(300, 360);
        GameObject textBackground = Instantiate(TextBackground, new Vector2(300, 0), Quaternion.identity);
        //GameObject textBackground = Instantiate(TextBackground, new Vector2(Input.mousePosition.x + XOffset + 280, Input.mousePosition.y + YOffset), Quaternion.identity);
        textBackground.transform.SetParent(zoomCard.transform, false);
        RectTransform rectBack = textBackground.GetComponent<RectTransform>();
        rectBack.sizeDelta = new Vector2(250, 300);
        zoomText = Instantiate(ZoomText, new Vector2(300, 100), Quaternion.identity);
        //zoomText = Instantiate(ZoomText, new Vector2(Input.mousePosition.x + XOffset + 280, Input.mousePosition.y + YOffset + 100), Quaternion.identity);
        zoomText.transform.SetParent(zoomCard.transform, false);
        zoomText.text = ZoomDesc;
    }
    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
    void OnDestroy(){
        Destroy(zoomCard);
    }
    void Update()
    {
        
    }
}
