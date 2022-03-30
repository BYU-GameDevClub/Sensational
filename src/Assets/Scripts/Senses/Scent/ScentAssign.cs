using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentAssign : MonoBehaviour
{
    [HideInInspector]
    public ParticleSystem scentEffect;

    public enum Smell // your custom enumeration
    {
        Stinky,
        Bloody,
        Minty
    };

    public Smell smell = Smell.Minty;  // this public var should appear as a drop down
    // Start is called before the first frame update
    void Start()
    {
        scentEffect = GetComponentInChildren<ParticleSystem>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        UpdateColor();     
    }

    private void UpdateColor()
    {
        if (scentEffect == null) return;
        switch (smell)
        {
            case Smell.Stinky:
                ChangeColor(Color.green);
                break;
            case Smell.Bloody:
                ChangeColor(Color.red);
                break;
            case Smell.Minty:
                ChangeColor(Color.cyan);
                break;
        }
    }

    private void ChangeColor(Color newColor)
    {
        var grad = scentEffect.colorOverLifetime;
        grad.color = new ParticleSystem.MinMaxGradient(newColor, Color.clear);
    }

    public void CreateChild()
    {
        if (GetComponentInChildren<ParticleSystem>() == null)
        {
            GameObject g = Resources.Load<GameObject>("Prefabs/ScentEffect");
            GameObject child = Instantiate(g, transform.position, transform.rotation, transform);
            scentEffect = child.GetComponent<ParticleSystem>();
        }
    }
}
