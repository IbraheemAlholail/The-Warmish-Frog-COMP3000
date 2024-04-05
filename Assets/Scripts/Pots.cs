using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pots : MonoBehaviour
{
    private Animator anim;
    public bool hasItem;
    public GameObject[] items;



    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void destroy() 
    {
        anim.SetBool("destroy",true);
        StartCoroutine(breakCo());
    }

    IEnumerator breakCo()
    {
        if (hasItem)
        {
            yield return new WaitForSeconds(0.3f);
            int i = Random.Range(0, items.Length);
            Instantiate(items[i], this.transform.position, Quaternion.identity);
            items[i].transform.position = this.transform.position;
            this.gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
            this.gameObject.SetActive(false);
        }
    }

}
