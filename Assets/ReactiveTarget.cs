using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReactToHit()
    {
        StartCoroutine(Die());
     
    }
    
    private IEnumerator Die()
    {
        enemyMove movingEnemy = GetComponent<enemyMove>();
        Color c = GetComponent<Renderer>().material.color;
        if( movingEnemy != null)
        {
            movingEnemy.SetAlive(false);
        }
        this.transform.Rotate(-45, 0, 0);
        int s = 10;
        for(float i = 1; i > 0; i -= 0.1f)
        {
            s *= -1;
            this.transform.Rotate(s, 0, 0);
            //GetComponent<Renderer>().material.color=new Color(c.r,c.g,c.b,i);
            yield return new WaitForSeconds(0.4f);
        }
        
        Destroy(this.gameObject);
        
    }

}
