using UnityEngine;
using System.Collections;


public class Link : MonoBehaviour
{

    public string id;
    public GameObject source;
    public GameObject target;
	public float weight;
	public float weightscale;

    public bool direction;
    public static float intendedLinkLength;
    public static float forceStrength;

    //private static GameController gameControl;
    //private static GraphController graphControl;

    private Component sourceRb;
    private Component targetRb;
    private LineRenderer lineRenderer;
    private float intendedLinkLengthSqr;
    private float distSqrNorm;

    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;

	//public GlobalScalers Script;
	//public GameObject CodeAttachment;


    void doAttraction()
    {
        Vector3 forceDirection = sourceRb.transform.position - targetRb.transform.position;
        float distSqr = forceDirection.sqrMagnitude;

        if (distSqr > intendedLinkLengthSqr)
        {
            //Debug.Log("(Link.FixedUpdate) distSqr: " + distSqr + "/ intendedLinkLengthSqr: " + intendedLinkLengthSqr + " = distSqrNorm: " + distSqrNorm);
            distSqrNorm = distSqr / intendedLinkLengthSqr;

            Vector3 targetRbImpulse = forceDirection.normalized * forceStrength * distSqrNorm;
            Vector3 sourceRbImpulse = forceDirection.normalized * -1 * forceStrength * distSqrNorm;

            if (true)
            {
                //Debug.Log("(Link.FixedUpdate) targetRb: " + targetRb + ". forceDirection.normalized: " + forceDirection.normalized + ". distSqrNorm: " + distSqrNorm + ". Applying Impulse: " + targetRbImpulse);
                ((Rigidbody)targetRb as Rigidbody).AddForce(targetRbImpulse);
                //Debug.Log("(Link.FixedUpdate) targetRb: " + sourceRb + ". forceDirection.normalized: " + forceDirection.normalized + "  * -1 * distSqrNorm: " + distSqrNorm + ". Applying Impulse: " + sourceRbImpulse);
                ((Rigidbody)sourceRb as Rigidbody).AddForce(sourceRbImpulse);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
		//GlobalScalers Script = GetComponent<GlobalScalers>();
		weightscale = 1f;
		//weightscale = Script.weightScale;
		GlobalScalers Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalScalers>();
		print("Script.largestLink: " + Script.largestLink);
		weightscale = Script.largestLink;


        //gameControl = FindObjectOfType<GameController>();
        //graphControl = FindObjectOfType<GraphController>();
		//weightScale = 1;
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        //color link according to status
        Color c;
        c = Color.cyan;
        c.a = 0.5f;
        


        //draw line
        //lineRenderer.material = new Material(Shader.Find("Self-Illumin/Diffuse"));
        lineRenderer.material = new Material(Shader.Find("Particles/Additive"));

        ////lineRenderer.material.color = (c);


        //lineRenderer.material.color = Color.Lerp(colorStart, colorEnd, (Mathf.PingPong(Time.time, 1) / 1));

        //lineRenderer.startColor = (Color.red);
        //lineRenderer.endColor=(c);
        //lineRenderer.SetColors(Color.red, Color.cyan);

        /*
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lineRenderer.colorGradient = gradient;
        */

		print("weight" + weight);
		print("weightScale" + weightscale);

        if (direction == true)
        {
			lineRenderer.startWidth = 0.5f*weight*weightscale;
			lineRenderer.endWidth = 0.1f*weight*weightscale;
            //lineRenderer.SetColors(Color.red, Color.cyan);
            lineRenderer.startColor = Color.blue;
            lineRenderer.endColor = Color.cyan;
        }
        else
        {
			lineRenderer.startWidth = 0.25f*weight*weightscale;
			lineRenderer.endWidth = 0.25f*weight*weightscale;
            //lineRenderer.SetColors(Color.red, Color.cyan);
            lineRenderer.startColor = Color.magenta;
            lineRenderer.endColor = Color.magenta;
        }

        //lineRenderer.startWidth = 0.5f;
        //lineRenderer.endWidth = 0.1f;

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);
        
        sourceRb = source.GetComponent<Rigidbody>();
        targetRb = target.GetComponent<Rigidbody>();        

        intendedLinkLengthSqr = intendedLinkLength * intendedLinkLength;
    }


    // Update is called once per frame
    void Update()
    {
        // moved from Start() in Update(), otherwise it won't see runtime updates of intendedLinkLength
        intendedLinkLengthSqr = intendedLinkLength * intendedLinkLength;
		/*
		GlobalScalers Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalScalers>();
		print("Script.largestLink: " + Script.largestLink);
		weightscale = Script.largestLink;


		if (direction == true)
		{
			lineRenderer.startWidth = 0.5f*weight/weightscale;
			lineRenderer.endWidth = 0.1f*weight/weightscale;

		}
		else
		{
			lineRenderer.startWidth = 0.25f*weight/weightscale;
			lineRenderer.endWidth = 0.25f*weight/weightscale;

		}
*/
        lineRenderer.SetPosition(0, source.transform.position);
        lineRenderer.SetPosition(1, target.transform.position);



        //Make line flash between red and green 
        //lineRenderer.material.color = Color.Lerp(colorStart, colorEnd, (Mathf.PingPong(Time.time, 1) / 1));
    }
		
}
