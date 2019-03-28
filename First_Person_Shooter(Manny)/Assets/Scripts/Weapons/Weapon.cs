using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SphereCollider))]

public class Weapon : MonoBehaviour, IInteractable {

    public int damage = 10;
    public int maxAmmo = 500;
    public int maxClip = 30;
    public float range = 10f;
    public float shootRate = 0.2f;
    public float lineDelay = 0.1f;
    public Transform shotOrigin;

    private int ammo = 0;
    private int clip = 0;
    private float shootTimer = 0f;
    private bool canShoot = false;

    // Components
    private Rigidbody rigid;
    private BoxCollider boxCollider;
    private SphereCollider sphereCollider;
    private LineRenderer lineRenderer;

    void Awake()
    {
        GetReferences();
    }

    void GetReferences()
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        lineRenderer = GetComponent<LineRenderer>();
        sphereCollider = GetComponent<SphereCollider>();

    }

    private void Reset()
    {
        GetReferences();

        // Collect all bounds inside of children
        Renderer[] children = GetComponentsInChildren<MeshRenderer>();
        Bounds bounds = new Bounds(transform.position, Vector3.zero);
        foreach (Renderer rend in children)
        {
            bounds.Encapsulate(rend.bounds);
        }
        
        // turn of line renderer
        lineRenderer.enabled = false;

        // turn of rigidbody
        rigid.isKinematic = false;

        // apply boudns to collider
        boxCollider.center = bounds.center - transform.position;
        boxCollider.size = bounds.size;

        // Enable trigger
        sphereCollider.isTrigger = true;
        sphereCollider.center = boxCollider.center;
        sphereCollider.radius = boxCollider.size.magnitude * .5f;
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // Increase shoot timer
        shootTimer += Time.deltaTime;
        // if time reaches rate
        if(shootTimer >= shootRate)
        {
            canShoot = true;
        }
    }

    public void Pickup()
    {
        rigid.isKinematic = true;
    }

    public void Drop()
    {
        rigid.isKinematic = false;
    }

    IEnumerator ShowLine(Ray bulletRay, float lineDelay)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, bulletRay.origin);
        lineRenderer.SetPosition(1, bulletRay.origin + bulletRay.direction * range);

        yield return new WaitForSeconds(lineDelay);

        lineRenderer.enabled = false;
    }

    public virtual void Reload()
    {
        // results in unlimited ammo...
        clip += ammo;
        ammo -= maxClip;
    }
    public virtual void Shoot()
    {
        // Can Shoot?
        if(canShoot)
        {
            // Create a bullet ray from shot origin to forward
            Ray bulletRay = new Ray(shotOrigin.position, shotOrigin.forward);
            RaycastHit hit;
            if(Physics.Raycast(bulletRay, out hit, range))
            {
                // Try getting enemy from hit
                IKillable killable = hit.collider.GetComponent<IKillable>();
                if (killable != null)
                {
                    killable.TakeDamage(damage);
                }
            }

            // Show Line 
            StartCoroutine(ShowLine(bulletRay, lineDelay));
            // reset 
            shootTimer = 0;
            canShoot = false;
        }
    }
}
