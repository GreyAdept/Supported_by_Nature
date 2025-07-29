using UnityEngine;


public class WeedCutParticles : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private gameTile parentTile;
    
    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        parentTile = GetComponentInParent<gameTile>();
    }

    private void Awake()
    {
        PlacementSystem.onCutWeeds += PlayCutParticles;
    }

    private void PlayCutParticles(gameTile context)
    {
        if (parentTile == null) return;

        if (context == parentTile)
        {
            Debug.Log("received event");
            SoundManager.Instance.PlayGameSound("cutGrass");
            particleSystem.Play();
        }
    }

    private void OnDisable()
    {
        PlacementSystem.onCutWeeds -= PlayCutParticles;
    }
}
