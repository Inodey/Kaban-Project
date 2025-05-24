// using UnityEngine;

// public class FootstepSound : MonoBehaviour
// {
//     public AudioSource audioSource;
//     public AudioClip[] footstepClips;
//     public float stepCooldown = 0.5f;

//     private float cooldownTimer = 0f;
//     private animationsScript animScript; // Replace with your actual script name

//     void Start()
//     {
//         animScript = GetComponent<animationsScript>(); // Make sure it's on the same GameObject
//     }

//     void Update()
//     {
//         if (animScript != null && animScript.WalkAnimationIsActive()) // Replace with your actual method
//         {
//             cooldownTimer += Time.deltaTime;

//             if (cooldownTimer >= stepCooldown)
//             {
//                 PlayFootstep();
//                 cooldownTimer = 0f;
//             }
//         }
//         else
//         {
//             cooldownTimer = stepCooldown; // Reset so it's instant on resume
//         }
//     }

//     void PlayFootstep()
//     {
//         if (footstepClips.Length > 0)
//         {
//             AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
//             audioSource.PlayOneShot(clip);
//         }
//     }
// }