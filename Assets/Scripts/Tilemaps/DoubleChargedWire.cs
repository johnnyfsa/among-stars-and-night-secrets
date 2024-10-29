using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoubleChargedWire : MonoBehaviour
{
    [SerializeField]
    Wires wire1;
    [SerializeField]
    Wires wire2;

    [SerializeField]
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
    [SerializeField]
    private bool isCharged;

    void Awake()
    {
        isCharged = false;
        tilemap = GetComponent<Tilemap>();
        tilemapRenderer = GetComponent<TilemapRenderer>();
    }

    void Update()
    {
        if (!isCharged && (wire1.NumCharges == wire1.MaxNumCharges && wire2.NumCharges == wire2.MaxNumCharges))
        {
            ChangeTileColorCharge();
            isCharged = true;
        }
        else if (isCharged && (wire1.NumCharges != wire1.MaxNumCharges || wire2.NumCharges != wire2.MaxNumCharges))
        {
            ChangeTileColorDischarge();
            isCharged = false;
        }

    }

    public void ChangeTileColorCharge()
    {
        // Obter o material do tileset
        Material material = tilemapRenderer.material;
        StartCoroutine(ColorizeTiles(material, startColor, endColor, transitionDuration));

    }
    public void ChangeTileColorDischarge()
    {
        // Obter o material do tileset
        Material material = tilemapRenderer.material;
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
