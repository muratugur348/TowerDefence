using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AskerMovement : MonoBehaviour
{
    Rigidbody2D fizik;
    string yon = "sol";
    float aldigiyol = 0;
    void Start()
    {

        fizik = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        aldigiyol += Time.deltaTime;
        if (yon.Equals("sol"))
            fizik.velocity = new Vector2(-1, 0);
        else if (yon.Equals("asagi"))
            fizik.velocity = new Vector2(0, -1);
        else if (yon.Equals("yukari"))
            fizik.velocity = new Vector2(0, 1);
        else if (yon.Equals("sag"))
            fizik.velocity = new Vector2(1, 0);
        if (aldigiyol >= 10)
        {
            gameObject.name = aldigiyol.ToString("n1");
        }
        else
        {
            gameObject.name = aldigiyol.ToString("n2");
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("asagi"))
        {
            yon = "asagi";
        }
        else if (other.gameObject.tag.Equals("sol"))
        {
            GetComponent<SpriteRenderer>().flipX = true;

            yon = "sol";
        }
        else if (other.gameObject.tag.Equals("yukari"))
            yon = "yukari";
        else if (other.gameObject.tag.Equals("sag"))
        {
            yon = "sag";
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
