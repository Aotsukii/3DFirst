using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    public Rigidbody rb;

    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    public int maxCollisions = 1;
    public float maxLifetime;

    int collisions;
    PhysicMaterial physics_mat;


    void Start()
    {
        Setup();
    }

    void Update()
    {
        maxLifetime -= Time.deltaTime;
        if ((collisions >= maxCollisions) || (maxLifetime <= 0))
        {
            Invoke("Delay",0.02f);
        }
    }

    private void Delay()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
    }
    private void Setup()
    {
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;

        GetComponent<SphereCollider>().material = physics_mat;

        rb.useGravity = useGravity;
    }
}
