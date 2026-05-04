using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier = 0.5f; // Controla la intensidad del efecto
    private Transform camaraTransform;
    private Vector3 previousCamaraPosition;
    private float spriteWidth, startPosition;
    void Start()
    {
        camaraTransform = Camera.main.transform;
        previousCamaraPosition = camaraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (camaraTransform.position.x - previousCamaraPosition.x) * parallaxMultiplier;
        float moveAmount = camaraTransform.position.x * (1 - parallaxMultiplier);
        
        transform.Translate(new Vector3(deltaX,0,0));
        previousCamaraPosition = camaraTransform.position;
        
        if(moveAmount > startPosition + spriteWidth) 
        {
            transform.Translate(new Vector3(spriteWidth,0,0));
            startPosition += spriteWidth;
        }
        else if(moveAmount < startPosition - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth,0,0));
            startPosition -= spriteWidth;
        }
    }
}
