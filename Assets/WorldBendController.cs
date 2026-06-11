using UnityEngine;
using UnityEngine.Rendering;

public class WorldBendController : MonoBehaviour
{
    [SerializeField]
    private Material material;

    private void LateUpdate()
    {
        var volume =
            VolumeManager.instance.stack
                .GetComponent<WorldBendVolume>();

        if (volume == null)
            return;

        material.SetFloat(
            "_StartDistance",
            volume.startDistance.value);

        material.SetFloat(
            "_FadeDistance",
            volume.fadeDistance.value);

        material.SetFloat(
            "_BendStrength",
            volume.bendStrength.value);

        material.SetFloat(
            "_Enabled",
            volume.enableEffect.value ? 1f : 0f);
    }
}