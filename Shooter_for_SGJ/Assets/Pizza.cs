using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : MonoBehaviour
{
    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animation.Play();
        }
    }

    private void Awake()
    {
        animation = GetComponent<Animation>();
        if (animation != null)
        {
            animation.Play();
        }
    }

    
}
