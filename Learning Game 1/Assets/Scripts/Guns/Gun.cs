using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Gun : MonoBehaviour
{

    

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem flash;
    public GameObject impactEffect;

    public TextMeshProUGUI ammoDisplay;
    public string slash = "/";

    private float nextTimeToFire = 0f;

    public Animator animator;

    void Start ()
    {
        if (currentAmmo == -1)
            currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.text = currentAmmo.ToString() + slash + maxAmmo;
        if (isReloading)
            return;
        
        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R)))
        {
            StartCoroutine(Reload());
            return;
        }

        
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();

        }


    }

    IEnumerator Reload ()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        flash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target !=null)
            {
                target.TakeDamage(damage);
            }

            var hitBox = hit.collider.GetComponent<HitBox>();
            if (hitBox)
            {
                hitBox.OnRaycastHit(this, fpsCam.transform.forward);
            }

            GameObject imapctGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(imapctGO, 2f);        
        }
    }
}
