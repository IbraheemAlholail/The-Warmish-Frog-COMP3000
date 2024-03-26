using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pots : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
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
        yield return new WaitForSeconds(0.3f);
        this.gameObject.SetActive(false);
    }

}
