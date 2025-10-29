using UnityEngine;

public class LeafBlowerMechanics : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float blowRange = 5f;
    public float blowRadius = 1f;
    private string blowableTag = "Blowable";
    public float blowForce = 10f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0)){
            Blow();
        }
    }

    void Blow() {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, blowRadius, transform.forward, blowRange);

        //Debug.DrawRay(transform.position, transform.forward * blowRange, Color.red, 0.5f);

        foreach (RaycastHit hit in hits)
        {
            // Make sure the object has the desired tag
            if (hit.collider.CompareTag(blowableTag))
            {
                Rigidbody rb = hit.collider.attachedRigidbody;

                if (rb != null)
                {
                    // Calculate direction and apply force
                    Vector3 forceDir = hit.collider.transform.position - transform.position;
                    rb.AddForce(forceDir.normalized * blowForce, ForceMode.Impulse);
                }
            }
        }
    }
}
