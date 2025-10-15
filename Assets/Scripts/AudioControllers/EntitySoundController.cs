using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(AudioSource))]
    public class EntitySoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private EntityScript entityScript;

        [Header("Sound Clips")]
        [SerializeField] private AudioClip damageSound;

        [SerializeField] private AudioClip jumpSound;
        [SerializeField] private AudioClip deathSound;
        [SerializeField] private AudioClip landSound;

        private void Awake()
        {
            if (!audioSource) audioSource = GetComponent<AudioSource>();
            if (!entityScript) entityScript = GetComponent<EntityScript>();

            if (entityScript)
            {
                entityScript.OnTakeDamage += PlayDamageSound;
                entityScript.OnJump += PlayJumpSound;
                entityScript.OnDie += PlayDeathSound;
                entityScript.OnLand += PlayLandSound;
            }
        }

        private void PlayDamageSound()
        {
            if (damageSound && audioSource)
            {
                audioSource.PlayOneShot(damageSound);
            }
        }

        private void PlayJumpSound()
        {
            if (jumpSound && audioSource)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }

        private void PlayDeathSound()
        {
            if (deathSound && audioSource)
            {
                audioSource.PlayOneShot(deathSound);
            }
        }

        private void PlayLandSound()
        {
            if (landSound && audioSource)
            {
                audioSource.PlayOneShot(landSound);
            }
        }

        private void OnDestroy()
        {
            if (entityScript)
            {
                entityScript.OnTakeDamage -= PlayDamageSound;
                entityScript.OnJump -= PlayJumpSound;
                entityScript.OnDie -= PlayDeathSound;
                entityScript.OnLand -= PlayLandSound;
            }
        }
    }
}