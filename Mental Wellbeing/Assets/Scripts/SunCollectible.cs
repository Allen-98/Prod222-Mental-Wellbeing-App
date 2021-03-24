using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunCollectible : MonoBehaviour
{
    
    //
    // Sun collectible that increases the plant's health when clicked
    //
    
    private bool collected = false;

    [Header("References")]
    [Tooltip("When the sun is collected the effect object will be enabled.")]
    public GameObject collectEffect;
    [Tooltip("Tag name used to get the object with the PlantHealth script - there should only be one.")]
    public string plantTagName = "Player";
    [Tooltip("Optional trail renderer to scale down after object is collected.")]
    public TrailRenderer optionalCollectTrailEffect = null;

    [Header("Sun")]
    [Tooltip("How much health to give the plant when the sun is collected.")]
    public float healthIncrease = 1f;
    [Tooltip("Velocity to move the sun along in units per second.")]
    public Vector2 velocity = Vector2.zero;
    [Tooltip("How long until the sun is destroyed in seconds.")]
    public float lifespan = 30f;

    private PlantHealth plantHealth;

    void Start()
    {
        plantHealth = GameObject.FindGameObjectWithTag(plantTagName).GetComponent<PlantHealth>();
    }

    void Update()
    {
        if (collected)
        {
            transform.position = Vector3.Lerp(transform.position, plantHealth.plantTop.transform.position, Time.deltaTime * 5f);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * 8f);

            TrailRenderer tr = optionalCollectTrailEffect;
            if (tr != null) tr.startWidth = Mathf.Lerp(tr.startWidth, 0, Time.deltaTime * 8f);

            if (transform.localScale.magnitude < 0.01f)
            {
                collectEffect.transform.SetParent(null, true);
                Destroy(collectEffect, 5f);
                Destroy(gameObject);
            }
        }
        else
        {
            lifespan -= Time.deltaTime;
            if (lifespan < 0f) Destroy(gameObject);

            transform.localPosition += new Vector3(velocity.x, velocity.y, 0f) * Time.deltaTime;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
                if (hit.collider != null && hit.transform == transform)
                {
                    collected = true;
                    collectEffect.SetActive(true);
                    plantHealth.AddHealth(healthIncrease);

                    foreach (ParticleSystem part in GetComponentsInChildren<ParticleSystem>())
                    {
                        part.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
}
