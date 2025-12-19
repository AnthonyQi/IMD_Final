using UnityEngine;

public class ModelWalk : MonoBehaviour
{
    public Rigidbody irb;
    public bool pingpong = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        irb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Up")
        {
            //Debug.Log("yo");
            irb.linearVelocity = new Vector3(0f,-.4f,0f);
        }
        else if (other.gameObject.tag == "Down")
        {
            irb.linearVelocity = new Vector3(0f,.4f,0f);
        }
    }
}
