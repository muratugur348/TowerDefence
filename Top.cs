using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    public ArrayList askerler = new ArrayList();
    oyunKontrol oyunKontrolScript;
    GameObject asker;
    Animator anim;
    public Sprite yukarisprite, asagisprite;
    int menzildekiasker = 0, maxIndex = 0;
    float max = 0,mermisayac=0;
    void Start()
    {
        
        oyunKontrolScript = GameObject.Find("_OyunKontrol").GetComponent<oyunKontrol>();
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }
    
    void Update()
    {
        if (menzildekiasker != 0)
        {
            print(askerler.Count + name);
            anim.enabled = true;
            max = 0;
            asker = (GameObject)askerler[0];
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
                if (asker.transform.position.x < transform.position.x)
                    GetComponent<SpriteRenderer>().flipX = false;
                else
                    GetComponent<SpriteRenderer>().flipX = true;

            }
            else if ((asker.transform.position.y > transform.position.y))
            {
                anim.Play("topyukari");
                if (asker.transform.position.x < transform.position.x)
                    GetComponent<SpriteRenderer>().flipX = true;
                else
                    GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            anim.enabled = false;
        }
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        askerler.Add(collision.gameObject);
        anim.enabled = true;
        menzildekiasker++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        askerler.Remove(collision.gameObject);
        oyunKontrolScript.askerler.RemoveAt(maxIndex);
        menzildekiasker--;

    }

}

