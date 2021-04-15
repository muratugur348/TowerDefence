using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    oyunKontrol oyunKontrolScript;
    GameObject asker;
    Animator anim;
    public Sprite yukarisprite, asagisprite;
    int menzildekiasker = 0, maxIndex = 0;
    ArrayList askerler;
    float max = 0,mermisayac=0;
    void Start()
    {
        
        oyunKontrolScript = GameObject.Find("_OyunKontrol").GetComponent<oyunKontrol>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    
    void Update()
    {
        print(menzildekiasker + "menzilde");
        if (menzildekiasker != 0)
        {
            anim.enabled = true;
            max = 0;
            for (int i = 0; i < oyunKontrolScript.askerler.Count; i++)
            {
                if(float.Parse((oyunKontrolScript.askerler[i].ToString()).Substring(0, 4))> max)
                {
                    max = float.Parse(oyunKontrolScript.askerler[i].ToString().Substring(0, 4));
                    maxIndex = i;
                }
            }
            asker = GameObject.Find(oyunKontrolScript.askerler[maxIndex].ToString().Substring(0, 4).ToString());
            print(asker.ToString());
            if ((asker.transform.position.x < transform.position.x && asker.transform.position.y > transform.position.y)
            || (asker.transform.position.x > transform.position.x && asker.transform.position.y < transform.position.y))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if ((asker.transform.position.y < transform.position.y))
            {
                anim.Play("topasagi");
                if (asker.transform.position.x<transform.position.x)
                        GetComponent<SpriteRenderer>().flipX = false;
                    else
                        GetComponent<SpriteRenderer>().flipX = true;
                
            }
            else if((asker.transform.position.y > transform.position.y))
            {
                anim.Play("topyukari");
                if (asker.transform.position.x < transform.position.x)
                    GetComponent<SpriteRenderer>().flipX = true;
                else
                    GetComponent<SpriteRenderer>().flipX = false;
            }
            mermisayac += Time.deltaTime;
            if (mermisayac>3)
            {
                oyunKontrolScript.askerler.RemoveAt(maxIndex);
                DestroyImmediate(asker);
                mermisayac = 0;
                menzildekiasker--;
            }
            if ((asker.transform.position.y < transform.position.y))
                {
                    GetComponent<SpriteRenderer>().sprite = asagisprite;
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = yukarisprite;

                }
        }
        else
        {
            anim.enabled = false;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        anim.enabled = true;
        menzildekiasker++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        oyunKontrolScript.askerler.RemoveAt(maxIndex);
        menzildekiasker--;

    }

}

