using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColor : MonoBehaviour
{
    public List<Color> colors;

    // Start is called before the first frame update
    void Start()
    {
       // Debug.Log("Color Set");

        Renderer color = GetComponent<Renderer>();
        
        color.material.color = colors[Random.Range(0, colors.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
