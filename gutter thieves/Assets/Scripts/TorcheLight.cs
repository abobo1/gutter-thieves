using UnityEngine;
using System.Collections;

public class TorcheLight : MonoBehaviour {
	
	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;
	public float MaxLightIntensity;
	public float IntensityLight;

    private ParticleSystem mainFlamePS;
    private ParticleSystem baseFlamePS;
    private ParticleSystem etincellesPS;
    private ParticleSystem fumeePS;

	void Start () {
		TorchLight.GetComponent<Light>().intensity=IntensityLight;

        mainFlamePS = MainFlame.GetComponent<ParticleSystem>();
        var mainFlameEmission = mainFlamePS.emission;
        mainFlameEmission.rate = IntensityLight * 20f;

        baseFlamePS = BaseFlame.GetComponent<ParticleSystem>();
        var baseFlameEmission = baseFlamePS.emission;
        baseFlameEmission.rate = IntensityLight * 15f;

        etincellesPS = Etincelles.GetComponent<ParticleSystem>();
        var etincellesEmission = etincellesPS.emission;
        etincellesEmission.rate = IntensityLight * 7f;

        fumeePS = Fumee.GetComponent<ParticleSystem>();
        var fumeeEmission = fumeePS.emission;
        fumeeEmission.rate = IntensityLight * 12f;

    }
	

	void Update () {
		if (IntensityLight<0) IntensityLight=0;
		if (IntensityLight>MaxLightIntensity) IntensityLight=MaxLightIntensity;		

		TorchLight.GetComponent<Light>().intensity=IntensityLight/2f+Mathf.Lerp(IntensityLight-0.1f,IntensityLight+0.1f,Mathf.Cos(Time.time*30));

		TorchLight.GetComponent<Light>().color=new Color(Mathf.Min(IntensityLight/1.5f,1f),Mathf.Min(IntensityLight/2f,1f),0f);

        var mainFlameEmission = mainFlamePS.emission;
        mainFlameEmission.rate = IntensityLight * 20f;

        var baseFlameEmission = baseFlamePS.emission;
        baseFlameEmission.rate = IntensityLight * 15f;

        var etincellesEmission = etincellesPS.emission;
        etincellesEmission.rate = IntensityLight * 7f;

        var fumeeEmission = fumeePS.emission;
        fumeeEmission.rate = IntensityLight * 12f;

    }
}
