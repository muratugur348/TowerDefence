using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{
    public ArrayList askerler = new ArrayList();
    public oyunKontrol oyunKontrolScript;
    GameObject asker,exitedAsker,shot;
    Animator anim;
    public GameObject gulle;
    public Sprite yukarisprite, asagisprite;
    public int menzildekiasker = 0;
    int topyon=1;
    bool animbasladi=false,finishAnimation=false;
    Vector2 gullekonum;
    float timeOfAnimation,currentTimeOfAnimation=0, currentTimeOfAnimationForShot=0, instantiateShotTime;
    void Start()
    {
        gullekonum = new Vector2(transform.position.x + 0.731f, transform.position.y + 0.459f);
        oyunKontrolScript = GameObject.Find("_OyunKontrol").GetComponent<oyunKontrol>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (menzildekiasker != 0)
        {
            animbasladi = true;
            GameObject firstAsker = (GameObject)askerler[0];
            float wayToTaken = 0;
            
            timeOfAnimation = anim.runtimeAnimatorController.animationClips[0].length;
            instantiateShotTime = 0.64f * timeOfAnimation;

            for (int i = 0; i < askerler.Count; i++)
            {
                GameObject currentAsker = (GameObject)askerler[i];
                if(currentAsker.GetComponent<AskerMovement>().getAldigiYol()>wayToTaken)
                {
                    wayToTaken = currentAsker.GetComponent<AskerMovement>().getAldigiYol();
                    asker = currentAsker;
                }
            }
            
            
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
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    gullekonum = new Vector2(transform.position.x - 0.651f, transform.position.y - 0.359f);
                    topyon = 0;
                }
                else
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    gullekonum = new Vector2(transform.position.x + 0.701f, transform.position.y - 0.409f);
                    topyon = 1;
                }

            }
            else if ((asker.transform.position.y > transform.position.y))
            {
                topyon = 0;
                anim.Play("topyukari");
                if (asker.transform.position.x < transform.position.x)
                {
                    gullekonum = new Vector2(transform.position.x - 0.731f, transform.position.y + 0.459f);
                    GetComponent<SpriteRenderer>().flipX = true;
                    topyon = 2;
                }
                else
                {
                    gullekonum = new Vector2(transform.position.x + 0.731f, transform.position.y + 0.459f);
                    GetComponent<SpriteRenderer>().flipX = false;
                    topyon = 3;
                }

            }
            
            currentTimeOfAnimation += Time.deltaTime;
            currentTimeOfAnimationForShot += Time.deltaTime;
            if(currentTimeOfAnimationForShot>=instantiateShotTime)
            {
                GameObject shot = Instantiate(gulle, gullekonum, Quaternion.identity);
                
                GameObject childShot = shot.transform.GetChild(0).gameObject;
                this.shot = childShot;
                if (topyon<2)
                {
                    
                    childShot.GetComponent<SpriteRenderer>().sortingLayerName = "toponu";
                }
                else
                {
                    childShot.GetComponent<SpriteRenderer>().sortingLayerName = "toparkasi";
                }
                currentTimeOfAnimationForShot = 0;
                mermiHareket mHScript = shot.GetComponent<mermiHareket>();
                mHScript.setTarget(asker);
                mHScript.setTop(this.gameObject);
            }
            if(currentTimeOfAnimation>=timeOfAnimation)
            {
                currentTimeOfAnimation = 0;
                currentTimeOfAnimationForShot = 0;
            }
            
        }
        else
        {
            if (animbasladi)
            {
                StartCoroutine(WaitforAnimationToFinish());
                animbasladi = false;
            }
        }
        if(finishAnimation)
        {
            currentTimeOfAnimation += Time.deltaTime;
            if (currentTimeOfAnimation >= instantiateShotTime)
            {
                GameObject shot = Instantiate(gulle, gullekonum, Quaternion.identity);
                GameObject childShot = shot.transform.GetChild(0).gameObject;
                if (topyon < 2)
                {

                    childShot.GetComponent<SpriteRenderer>().sortingLayerName = "toponu";
                }
                else
                {
                    childShot.GetComponent<SpriteRenderer>().sortingLayerName = "toparkasi";
                }
                mermiHareket mHScript = shot.GetComponent<mermiHareket>();
                mHScript.setTarget(exitedAsker);
                mHScript.setTop(this.gameObject);
                currentTimeOfAnimation = 0;
                finishAnimation = false;
            }
        }
    }
    IEnumerator WaitforAnimationToFinish()
    {
        if(currentTimeOfAnimation<instantiateShotTime && currentTimeOfAnimation!=0)
        {
            finishAnimation = true;
        }
        print(currentTimeOfAnimation);
        yield return new WaitForSeconds(timeOfAnimation-currentTimeOfAnimation);
        currentTimeOfAnimation = 0;
        currentTimeOfAnimationForShot = 0;
        if (topyon==0)
        {
            anim.Play("topasagiidle");
            GetComponent<SpriteRenderer>().flipX = false;

        }
        else if (topyon==1)
        {
            anim.Play("topasagiidle");
            GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else if(topyon==2)
        {
            anim.Play("topyukariidle");
            GetComponent<SpriteRenderer>().flipX = true;

        }
        else if(topyon==3)
        {
            anim.Play("topyukariidle");
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        askerler.Add(collision.gameObject);
        menzildekiasker++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exitedAsker = collision.gameObject;
        askerler.Remove(collision.gameObject);
        menzildekiasker--;

    }

}

