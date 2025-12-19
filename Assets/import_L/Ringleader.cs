using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Ringleader : MonoBehaviour
{
    private bool baudimoovan = true;
    public GameObject icon;
    public Renderer ir;
    private ModelWalk iconcs;
    public Rigidbody irb;
    public Rigidbody rb;
    public GameObject icon2;
    public Renderer ir2;
    public GameObject icon3;
    public Renderer ir3;
    public AudioSource aux;
    //private int n = 0;
    public TextMeshProUGUI n1;
    public TextMeshProUGUI n2;
    public TextMeshProUGUI n3;
    public TextMeshProUGUI n4;
    public TextMeshProUGUI n5;
    public TextMeshProUGUI n1t;
    public TextMeshProUGUI n2t;
    public TextMeshProUGUI n3t;
    public TextMeshProUGUI n4t;
    public TextMeshProUGUI n5t;
    public TMP_Text it1;
    public TMP_Text it2;
    public TMP_Text it3;
    private bool thebiglabeling = false;
    private List<float> jscores = new List<float>();
    private List<float> rscores = new List<float>();
    private List<string> names = new List<string> { "- SpringCrayon", "- MaliciousToejam", "- NumeroUno", "- 67676767"};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ir = icon.GetComponent<Renderer>();
        ir.material.color = Color.black;
        ir2 = icon2.GetComponent<Renderer>();
        ir2.material.color = Color.black;
        ir3 = icon3.GetComponent<Renderer>();
        ir3.material.color = Color.black;
        iconcs = icon.GetComponent<ModelWalk>();
        irb = icon.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        aux = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (baudimoovan == true)
        {
            rb.linearVelocity = new Vector3(0f, -2f, 0f);
        }
        else
        {
            rb.linearVelocity = new Vector3(0f, 0f, 0f);
        }

        if(aux.isPlaying)
        {
            //grey icon
        }
        else
        {
            if (iconcs.pingpong == false)
            {
                irb.linearVelocity = new Vector3(0f,.4f,0f);
                iconcs.pingpong = true;
            }

            if (thebiglabeling == true)
            {
                float randnum = Random.Range(10.0f, 29.5f);
                jscores.Add(randnum);
                randnum = Random.Range(10.0f, 29.5f);
                jscores.Add(randnum);
                randnum = Random.Range(10.0f, 29.5f);
                jscores.Add(randnum);
                randnum = Random.Range(10.0f, 29.5f);
                jscores.Add(randnum);
                jscores.Add(22.0f); //ur score
                jscores.Sort();
                jscores.Reverse();
                n1.text = jscores[0].ToString("F2");
                n2.text = jscores[1].ToString("F2");
                n3.text = jscores[2].ToString("F2");
                n4.text = jscores[3].ToString("F2");
                n5.text = jscores[4].ToString("F2");
                if (jscores[0] == 22.0f)
                {
                    n1.text += ("- YOU");
                    n2.text += (names[0]);
                    n3.text += (names[1]);
                    n4.text += (names[2]);
                    n5.text += (names[3]);
                }
                else if (jscores[1] == 22.0f)
                {
                    n1.text += (names[0]);
                    n2.text += ("- YOU");
                    n3.text += (names[1]);
                    n4.text += (names[2]);
                    n5.text += (names[3]);
                }
                else if (jscores[2] == 22.0f)
                {
                    n1.text += (names[0]);
                    n3.text += ("- YOU");
                    n2.text += (names[1]);
                    n4.text += (names[2]);
                    n5.text += (names[3]);
                }
                else if (jscores[3] == 22.0f)
                {
                    n1.text += (names[0]);
                    n4.text += ("- YOU");
                    n3.text += (names[2]);
                    n2.text += (names[1]);
                    n5.text += (names[3]);
                }
                else if (jscores[4] == 22.0f)
                {
                    n1.text += (names[0]);
                    n5.text += ("- YOU");
                    n3.text += (names[2]);
                    n2.text += (names[1]);
                    n4.text += (names[3]);
                }

                randnum = Random.Range(8.0f, 14.0f);
                rscores.Add(randnum);
                randnum = Random.Range(8.0f, 14.0f);
                rscores.Add(randnum);
                randnum = Random.Range(8.0f, 14.0f);
                rscores.Add(randnum);
                randnum = Random.Range(8.0f, 14.0f);
                rscores.Add(randnum);
                rscores.Add(12.0f); //ur score
                rscores.Sort();
                //rscores.Reverse();
                n1t.text = rscores[0].ToString("F2");
                n2t.text = rscores[1].ToString("F2");
                n3t.text = rscores[2].ToString("F2");
                n4t.text = rscores[3].ToString("F2");
                n5t.text = rscores[4].ToString("F2");
                if (rscores[0] == 12.0f)
                {
                    n1t.text += ("- YOU");
                    n2t.text += (names[0]);
                    n3t.text += (names[1]);
                    n4t.text += (names[2]);
                    n5t.text += (names[3]);
                }
                else if (rscores[1] == 12.0f)
                {
                    n1t.text += (names[0]);
                    n2t.text += ("- YOU");
                    n3t.text += (names[1]);
                    n4t.text += (names[2]);
                    n5t.text += (names[3]);
                }
                else if (rscores[2] == 12.0f)
                {
                    n1t.text += (names[0]);
                    n3t.text += ("- YOU");
                    n2t.text += (names[1]);
                    n4t.text += (names[2]);
                    n5t.text += (names[3]);
                }
                else if (rscores[3] == 12.0f)
                {
                    n1t.text += (names[0]);
                    n4t.text += ("- YOU");
                    n3t.text += (names[2]);
                    n2t.text += (names[1]);
                    n5t.text += (names[3]);
                }
                else if (rscores[4] == 12.0f)
                {
                    n1t.text += (names[0]);
                    n5t.text += ("- YOU");
                    n3t.text += (names[2]);
                    n2t.text += (names[1]);
                    n4t.text += (names[3]);
                }

                thebiglabeling = false;
            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Freddy")
        {
            baudimoovan = false;
            thebiglabeling = true;
        }
    }
}
