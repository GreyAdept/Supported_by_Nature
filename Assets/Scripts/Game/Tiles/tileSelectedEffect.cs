using UnityEngine;

public class tileSelectedEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public ParticleSystem ps;
    private bool isPlaying;

    public void PlayParticle()
    {
        if (!isPlaying)
        { 
            ps.Play();
        }

        isPlaying = true;

    }

    public void StopParticle()
    {
        if (isPlaying)
        {   
            ps.Stop();
            ps.Clear();
        }
        isPlaying = false;
    }
}
