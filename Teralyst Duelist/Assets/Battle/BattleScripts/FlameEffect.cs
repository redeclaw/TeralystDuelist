using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameEffect : MonoBehaviour
{
    public GameObject SubFlame;
    public GameObject Canvas;
    public int randMod;
    public int counter = 0;
    public bool ToPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 1.5f);
        if (ToPlayer){
            transform.position = new Vector3(1660, 740);
        }
        else{
            transform.position = new Vector3(220, 740);
        }
        Canvas = GameObject.Find("Canvas");
        System.Random random = new System.Random();
        randMod = random.Next(0, 100);
        InvokeRepeating("Move", 0, 0.12f);
    }

    // Update is called once per frame
    void Move()
    {
        float xmod = Mathf.Cos((transform.position.x + randMod)/30) * 50;
        float ymod = Mathf.Sin((transform.position.x + randMod)/30) * 50;
        GameObject subFlame = Instantiate(SubFlame, new Vector2(transform.position.x + xmod, transform.position.y + ymod), Quaternion.identity);
        subFlame.transform.SetParent(Canvas.transform);
        //Debug.Log($"X: {transform.position.x} Y: {transform.position.y}");
       
    }
    void Update(){
        if (ToPlayer){
            transform.position -= new Vector3(960 * Time.deltaTime, 0);
            //transform.Translate(Vector3.left * Time.deltaTime);
        }
        else{
            transform.position += new Vector3(960 * Time.deltaTime, 0);
        }
    }
}
