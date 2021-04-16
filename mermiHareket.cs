using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermiHareket : MonoBehaviour
{
    public GameObject target,top;
    float mesafe;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject can = target.transform.GetChild(0).gameObject;
        
        mesafe = Vector3.Distance(transform.position, target.transform.position);
        transform.Translate(Vector3.Normalize(target.transform.position - transform.position) * 0.02f);
        
        if (mesafe <0.2f)
        {
            can.GetComponent<SpriteRenderer>().size -= new Vector2(3f, 0);
            if (can.GetComponent<SpriteRenderer>().size.x <= 0)
            {
                if (top.GetComponent<Top>().askerler.Count != 0 && 
                        (Object)top.GetComponent<Top>().askerler[0]== target)
                {

                    top.GetComponent<Top>().askerler.RemoveAt(0);
                    top.GetComponent<Top>().menzildekiasker--;         
                }
                top.GetComponent<Top>().oyunKontrolScript.askerler.Remove(target);
                DestroyImmediate(target);
            }
            Destroy(this.gameObject);
        }
        

    }
    public void setTarget(GameObject target)
    {
        this.target = target;
    }
    public void setTop(GameObject top)
    {
        this.top= top;
    }
}
