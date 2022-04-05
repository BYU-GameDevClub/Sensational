using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentAssign : MonoBehaviour
{

    [HideInInspector]
    public ParticleSystem scentEffect;

    public int scentStrength = 10;

    public enum Smell // your custom enumeration
    {
        Stinky,
        Bloody,
        Minty
    };

    public Smell smell = Smell.Minty;  // this public var should appear as a drop down

    private Transform player;
    private int playerScentLevel = 1;
    private Color scentColor;

    // Start is called before the first frame update
    void Start()
    {
        scentEffect = GetComponentInChildren<ParticleSystem>();
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (scentEffect.isPlaying) {
            setIntensity();
        }
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
                scentColor = Color.green;
                break;
            case Smell.Bloody:
                scentColor = Color.red;
                break;
            case Smell.Minty:
                scentColor = Color.cyan;
                break;
        }
    }

    float startAlpha = 150.0f;
    float startSpeed = 0.03f;
    float startStrength = 0.1f;
    private void ChangeColor()
    {
        float alpha = startAlpha;
        Gradient ourGradient = new Gradient();
        ourGradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(scentColor, 0.0f), new GradientColorKey(Color.clear, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );

        var grad = scentEffect.colorOverLifetime;
        grad.color = new ParticleSystem.MinMaxGradient(ourGradient);

        ParticleSystem.MainModule main = scentEffect.main;
        main.startSpeed = startSpeed;
        ParticleSystem.NoiseModule noise = scentEffect.noise;
        noise.strength = startStrength;
    }

    private void setIntensity()
    {
        const float MAXALPHA = 0.6f;
        float MAXVISIBLEDISTANCE = 20.0f;
        const float MAXSTRENGTH = 1.0f;
        const float MAXSPEED = 1.0f;

        float distanceFromPlayer = (transform.position - player.position).magnitude;


        //value 0.0 - 1.0
        float intensity = (scentStrength * playerScentLevel / 150.0f) * Mathf.Max(1 - distanceFromPlayer / MAXVISIBLEDISTANCE , 0);

        startAlpha = intensity * MAXALPHA;
        startSpeed = intensity * MAXSPEED;
        startStrength = intensity * MAXSTRENGTH;

        ChangeColor(); 
    }

    public void setActive(bool active, Transform playerPosition, int playerScentLevel)
    {
        this.playerScentLevel = playerScentLevel;
        player = playerPosition;
        if (active)
        {
            scentEffect.Play();
            setIntensity();
        }
        else
        {
            scentEffect.Stop();
            scentEffect.Clear();
        }
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
