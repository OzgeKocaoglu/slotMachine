using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public delegate void ParticleHandler(int comboId);
    public static ParticleHandler On_StartParticle;
    ParticleSystem system;


    private void Awake()
    {
        system = GetComponent<ParticleSystem>();
        On_StartParticle += ParticleStart;
    }
    private void OnDestroy()
    {
        On_StartParticle -= ParticleStart;
    }

    private void ParticleAmount(int comboId)
    {
        system.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, 10 * comboId) });
    }
    private void ParticleStart(int comboId)
    {
        ParticleAmount(comboId);
        system.Play();
    }
}
