using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }

    public List<Effect> effects;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Play(string name, Vector3 position, Quaternion rotation)
    {
        Effect effect = effects.Find(s => s.name == name);
        if (effect == null)
        {
            Debug.LogWarning($"Effect: {name} not found!");
            return;
        }
        GameObject visual = Instantiate(effect.prefab, position, rotation);
        Destroy(visual, 2f);
    }
}
