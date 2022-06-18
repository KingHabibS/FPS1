using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangingCrosshair : MonoBehaviour
{
    [SerializeField] private Image Image;

    private void Start()
    {
        Image.color = new Color(1, 1, 1, 0.75f);
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 3f) && hit.transform.gameObject.CompareTag("Interactable"))
        {
                Image.color = new Color(0, 128, 0, 0.75f);
        }
        else if(Physics.Raycast(transform.position, transform.forward, out hit, 3f) && hit.transform.gameObject.CompareTag("Enemy"))
            {
                Image.color = new Color(255, 0, 0, 0.75f);
            }
        else
        {
            Image.color = new Color(255, 255, 255, 0.75f);
        }
    }
}