using MelonLoader;
using UnityEngine;

[RegisterTypeInIl2Cpp]
public class FireMonkeyAnimationEvents : MonoBehaviour
{
    private ParticleSystem rightSystem;
    public void PlayBlastR()
    {
        rightSystem.Play();
    }

    public void StopBlastR()
    {
        rightSystem.Stop();
    }
}
