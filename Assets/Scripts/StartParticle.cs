using UnityEngine;

public class StartParticle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト（パーティクル）")] 
    private ParticleSystem particle;

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            ParticleSystem newParticle = Instantiate(particle);
            newParticle.transform.position = this.transform.position;
            newParticle.Play();
            Destroy(newParticle.gameObject, 5.0f);
        }
    }
}