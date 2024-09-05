using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinFlip : MonoBehaviour
{
    private GameObject CoinCanvas;
    [SerializeField] private GameObject CoinBackground;
    [SerializeField] private TextMeshProUGUI CoinText;
    [SerializeField] private Sprite[] Faces;
    public Image CoinImage;
    public int NumFlips = 10;
    void Start(){
        CoinCanvas = GameObject.Find("CoinCanvas");
        StartCoroutine(FlipCoin(.005f, 1, NumFlips));
    }
    public IEnumerator FlipCoin(float duration, float size, int NumFlips){
        for (int i = 0; i < NumFlips; i++){
            while (size > 0.1){
                size -= 0.07f;
                transform.localScale = new Vector3(1, size, 1);
                yield return new WaitForSeconds(duration);
            }
            CoinImage.sprite = Faces[i % 2];
            while (size < 0.99){
                size += 0.07f;
                transform.localScale = new Vector3(1, size, size);
                yield return new WaitForSeconds(duration);
            }
        }
        yield return new WaitForSeconds(0.2f);
        GameObject coinBackground = Instantiate(CoinBackground, new Vector2(0, 0), Quaternion.identity);
        coinBackground.transform.SetParent(CoinCanvas.transform, false);
        TextMeshProUGUI coinText = Instantiate(CoinText, new Vector2(0, 0), Quaternion.identity);
        if (NumFlips % 2 == 0){
            coinText.text = "Your opponent goes first";
        }
        else{
            coinText.text = "You go first";
        }
        coinText.transform.SetParent(coinBackground.transform, false);
        yield return new WaitForSeconds(2);
        Destroy(coinBackground);
        Destroy(gameObject);
    }
}
