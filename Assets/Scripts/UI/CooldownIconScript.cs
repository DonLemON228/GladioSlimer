using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownIconScript : MonoBehaviour
{
    public Image cooldownImage;
    private bool isCoolingDown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCooldown(0);
    }
    
    public void StartCooldown(float cooldownDuration)
    {
        if (!isCoolingDown)
        {
            StartCoroutine(CooldownRoutine(cooldownDuration));
        }
    }

    private IEnumerator CooldownRoutine(float duration)
    {
        isCoolingDown = true;
        float elapsed = 0f;

        // Устанавливаем начальные параметры заполнения
        cooldownImage.fillAmount = 1f; // Полное заполнение в начале

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cooldownImage.fillAmount = 1f - (elapsed / duration); // Обновляем заполнение
            yield return null; // Ждем следующего кадра
        }

        // Завершаем кулдаун
        cooldownImage.fillAmount = 0f; // Полное опустошение
        isCoolingDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
