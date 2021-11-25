using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonVisuales : MonoBehaviour
{
    public Color colorbase1;
    public Color colorbase2;
    public Color topColor1;
    public Color bottomColor1;
    public Color topColor2;
    public Color bottomColor2;

    public Color colorBaseLluvia;
    public Color colorTopLluvia;
    public Color colorBottomLluvia;

    public Color colorBaseSequia;
    public Color colorTopSequia;
    public Color colorBottomSequia;

    [Range (0,1)]
    public float seasonValue;
    public GameObject[] leaves;
    public Material heatMaterial;
    public float desviacionCalor = 0.002f;
    public float cantidadHojas = 6;

    public bool lluvia = false;
    public bool sequia = false;

    public float tiempoAparicionEvento = 1.5f;
    public GameObject lluviaPS;
    public float tmp = 0;
    bool deLluvia;
    bool deSequia;


    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox.SetFloat("_SeasonValue", 0);
        var particleEmission = lluviaPS.GetComponent<ParticleSystem>().emission;
        particleEmission.rateOverTime = 0;
        tmp = tiempoAparicionEvento;
    }

    // Update is called once per frame
    void Update()
    {
        if(lluvia == true)
        {
            tmp += Time.deltaTime;
            var particleEmission = lluviaPS.GetComponent<ParticleSystem>().emission;
            particleEmission.rateOverTime = Mathf.Lerp(0, 70, tmp/tiempoAparicionEvento);
            //RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, colorBaseLluvia, tmp/tiempoAparicionEvento);
            //RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, colorTopLluvia, tmp / tiempoAparicionEvento);
            //RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, colorBottomLluvia, tmp / tiempoAparicionEvento);
            RenderSettings.ambientLight = Color.Lerp(Color.Lerp(colorbase1, colorbase2, seasonValue), colorBaseLluvia, tmp / tiempoAparicionEvento);
            RenderSettings.ambientEquatorColor = Color.Lerp(Color.Lerp(topColor1, topColor2, seasonValue), colorTopLluvia, tmp / tiempoAparicionEvento);
            RenderSettings.ambientGroundColor = Color.Lerp(Color.Lerp(bottomColor1, bottomColor2, seasonValue), colorBottomLluvia, tmp / tiempoAparicionEvento);
            deLluvia = true;
        }
        else if(sequia == true)
        {
            tmp += Time.deltaTime;

            //RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, colorBaseSequia, tmp / tiempoAparicionEvento);
            //RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, colorTopSequia, tmp / tiempoAparicionEvento);
            //RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, colorBottomSequia, tmp / tiempoAparicionEvento);
            RenderSettings.ambientLight = Color.Lerp(Color.Lerp(colorbase1, colorbase2, seasonValue), colorBaseSequia, tmp / tiempoAparicionEvento);
            RenderSettings.ambientEquatorColor = Color.Lerp(Color.Lerp(topColor1, topColor2, seasonValue), colorTopSequia, tmp / tiempoAparicionEvento);
            RenderSettings.ambientGroundColor = Color.Lerp(Color.Lerp(bottomColor1, bottomColor2, seasonValue), colorBottomSequia, tmp / tiempoAparicionEvento);
            deSequia = true;
        }
        else
        {
            var particleEmission = lluviaPS.GetComponent<ParticleSystem>().emission;
            if (tmp > tiempoAparicionEvento)
            {
                particleEmission.rateOverTime = 0;
                RenderSettings.ambientLight = Color.Lerp(colorbase1, colorbase2, seasonValue);
                RenderSettings.ambientEquatorColor = Color.Lerp(topColor1, topColor2, seasonValue);
                RenderSettings.ambientGroundColor = Color.Lerp(bottomColor1, bottomColor2, seasonValue);
                deSequia = false;
                deLluvia = false;
            }
            else
            {
                tmp += Time.deltaTime;
                
                particleEmission.rateOverTime = Mathf.Lerp(particleEmission.rateOverTime.constant, 0, tmp / tiempoAparicionEvento);
                if (deLluvia)
                {

                    RenderSettings.ambientLight = Color.Lerp(colorBaseLluvia, Color.Lerp(colorbase1, colorbase2, seasonValue), tmp / tiempoAparicionEvento);
                    RenderSettings.ambientEquatorColor = Color.Lerp(colorTopLluvia, Color.Lerp(topColor1, topColor2, seasonValue), tmp / tiempoAparicionEvento);
                    RenderSettings.ambientGroundColor = Color.Lerp(colorBottomLluvia, Color.Lerp(bottomColor1, bottomColor2, seasonValue), tmp / tiempoAparicionEvento);
                }
                if (deSequia)
                {

                    RenderSettings.ambientLight = Color.Lerp(colorBaseSequia, Color.Lerp(colorbase1, colorbase2, seasonValue), tmp / tiempoAparicionEvento);
                    RenderSettings.ambientEquatorColor = Color.Lerp(colorTopSequia, Color.Lerp(topColor1, topColor2, seasonValue), tmp / tiempoAparicionEvento);
                    RenderSettings.ambientGroundColor = Color.Lerp(colorBottomSequia, Color.Lerp(bottomColor1, bottomColor2, seasonValue), tmp / tiempoAparicionEvento);
                }
            }
        }
        

        RenderSettings.skybox.SetFloat("_SeasonValue", Mathf.Lerp(0,4,seasonValue));

        for(int i = 0; i < leaves.Length; i++)
        {
            var particleEmission = leaves[i].GetComponentInChildren<ParticleSystem>().emission;
            if (seasonValue >= 0.66f)
            {
                particleEmission.rateOverTime = Mathf.Lerp(0, cantidadHojas, seasonValue);
            }
            else
            {
                particleEmission.rateOverTime = 0;
            }
        }
        if(seasonValue >= 0.33f && seasonValue <= 0.66f)
        {
            heatMaterial.SetFloat("_Strength", desviacionCalor);
        }
        else
        {
            heatMaterial.SetFloat("_Strength", 0);
        }
        

    }
}
