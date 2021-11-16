using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentLightingPrueba : MonoBehaviour
{
    public Color colorbase1;
    public Color colorbase2;
    public Color topColor1;
    public Color bottomColor1;
    public Color topColor2;
    public Color bottomColor2;
    [Range (0,1)]
    public float seasonValue;
    public GameObject[] leaves;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.ambientLight = Color.Lerp(colorbase1, colorbase2, seasonValue);
        RenderSettings.ambientEquatorColor = Color.Lerp(topColor1, topColor2, seasonValue);
        RenderSettings.ambientGroundColor = Color.Lerp(bottomColor1, bottomColor2, seasonValue);

        RenderSettings.skybox.SetFloat("_SeasonValue", Mathf.Lerp(0,4,seasonValue));

        for(int i = 0; i < leaves.Length; i++)
        {
            var particleEmission = leaves[i].GetComponentInChildren<ParticleSystem>().emission;
            if (seasonValue >= 0.6f)
            {
                particleEmission.rateOverTime = Mathf.Lerp(0, 6, seasonValue);
            }
            else
            {
                particleEmission.rateOverTime = 0;
            }
        }
        

    }
}
