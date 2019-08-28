using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public LayerMask levelMask;

    private bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 3f);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Spawns explosion at bomb's position

        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));

        GetComponent<MeshRenderer>().enabled = false; // Disables mesh renderer, making bomb invisible
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false); // Disables collider, allowing players to move through and walk into explosion
        Destroy(gameObject, .3f); // Destroys bomb after 0.3 seconds; ensures all explosions spawn before GameObject is destroyed
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!exploded && other.CompareTag("Explosion"))
        {
            CancelInvoke("Explode");
            Explode();
        }
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        for (int i = 1; i < 3; i++)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position + new Vector3(0, .5f, 0), direction, out hit,
              i, levelMask);
            if (!hit.collider)
            {
                Instantiate(explosionPrefab, transform.position + (i * direction),
                explosionPrefab.transform.rotation);
            }
            else
            {
                break;
            }
            Camera.main.GetComponent<CameraShake>().shakeDuration = 0.5f;
            yield return new WaitForSeconds(.05f);
        }
    }
}
