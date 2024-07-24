using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Color startColor = Color.white;
    [SerializeField]
    private Color endColor = Color.red;

    private Charger charger;
    // Start is called before the first frame update
    void Awake()
    {
        charger = GetComponent<Charger>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        charger.OnCharge += ChangeSpriteColorCharge;
        charger.OnDischarge += ChangeSpriteColorDischarge;
    }

    void OnDestroy()
    {
        charger.OnCharge -= ChangeSpriteColorCharge;
        charger.OnDischarge -= ChangeSpriteColorDischarge;
    }

    public void ChangeSpriteColorCharge()
    {
        Material material = spriteRenderer.material;
        StartCoroutine(ColorizeSprite(material, startColor, endColor, 1f));
    }

    public void ChangeSpriteColorDischarge()
    {
        Material material = spriteRenderer.material;
        StartCoroutine(ColorizeSprite(material, endColor, startColor, 1f));
    }


    private IEnumerator ColorizeSprite(Material material, Color startColor, Color endColor, float duration)
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
        spriteRenderer.color = endColor;
    }
}
