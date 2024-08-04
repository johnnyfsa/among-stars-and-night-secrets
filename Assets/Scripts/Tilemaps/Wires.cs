using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wires : MonoBehaviour
{
    [SerializeField]
    GameObject charger;
    private Charger chargerScript;

    // Referência ao TilemapRenderer
    private TilemapRenderer tilemapRenderer;
    private Tilemap tilemap;

    // Cor inicial
    public Color startColor = Color.white;

    // Cor final
    public Color endColor = Color.red;

    // Duração da transição (em segundos)
    public float transitionDuration = 1.0f;

    [SerializeField]
    private int maxNumCharges;
    private int numCharges;

    void Awake()
    {
        numCharges = 0;
        tilemap = GetComponent<Tilemap>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        chargerScript = charger.GetComponent<Charger>();
        chargerScript.OnChargesCarriedNumberIncreased += ChangeTileColorCharge;
        chargerScript.OnChargesCarriedNumberDecreased += DecreaseCharges;
        chargerScript.OnDischarge += ChangeTileColorDischarge;

    }

    private void DecreaseCharges()
    {
        numCharges--;
    }

    void OnDestroy()
    {
        chargerScript.OnCharge -= ChangeTileColorCharge;
        chargerScript.OnDischarge -= ChangeTileColorDischarge;
        chargerScript.OnChargesCarriedNumberIncreased -= ChangeTileColorCharge;
        chargerScript.OnChargesCarriedNumberDecreased -= DecreaseCharges;
    }

    // Referência ao Tilemap



    // Evento que aciona a mudança de cor
    public void ChangeTileColorCharge()
    {
        numCharges++;
        if (numCharges == maxNumCharges)
        {
            // Obter o material do tileset
            Material material = tilemapRenderer.material;

            // Iniciar a corização gradual
            StartCoroutine(ColorizeTiles(material, startColor, endColor, transitionDuration));
        }

    }

    public void ChangeTileColorDischarge()
    {
        // Obter o material do tileset
        Material material = tilemapRenderer.material;

        // Iniciar a corização gradual
        StartCoroutine(ColorizeTiles(material, endColor, startColor, transitionDuration));
    }

    // Coroutine para realizar a transição de cor
    private IEnumerator ColorizeTiles(Material material, Color startColor, Color endColor, float duration)
    {
        // Tempo inicial
        float startTime = Time.time;

        // Cor atual
        Color currentColor = startColor;

        // Loop enquanto a transição não terminar
        while (Time.time < startTime + duration)
        {
            // Calcular o progresso da transição
            float progress = (Time.time - startTime) / duration;

            // Interpolar a cor
            currentColor = Color.Lerp(startColor, endColor, progress);

            // Atualizar a cor do material
            material.color = currentColor;

            // Aguardar o próximo frame
            yield return null;
        }

        // Definir a cor final
        material.color = endColor;
        tilemap.color = endColor;
    }
}
