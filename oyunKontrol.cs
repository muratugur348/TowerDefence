using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oyunKontrol : MonoBehaviour
{
    public ArrayList askerler = new ArrayList();
    public GameObject askerprefab, altobje;
    int dalgaaskersayisi = 3;
    float sayac = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(askerOlustur(dalgaaskersayisi));
    }

    // Update is called once per frame
    void Update()
    {
        sayac += Time.deltaTime;
        if (sayac > 15)
        {
            StartCoroutine(askerOlustur(++dalgaaskersayisi));

            sayac = 0;
        }
        
    }
    IEnumerator askerOlustur(int askersayisi)
    {
        for (int i = 0; i < askersayisi; i++)
        {

            GameObject olusturulan = Instantiate(askerprefab, altobje.transform.position,
                Quaternion.identity);
            olusturulan.transform.parent = altobje.transform;
            askerler.Add(olusturulan);
            
            yield return new WaitForSeconds(1);
        }
    }
}
