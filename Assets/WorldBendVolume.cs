using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
[VolumeComponentMenu("Custom/World Bend")]
public class WorldBendVolume : VolumeComponent
{
    public BoolParameter enableEffect =
        new BoolParameter(true);

    public ClampedFloatParameter startDistance =
        new ClampedFloatParameter(20f, 0f, 500f);

    public ClampedFloatParameter fadeDistance =
        new ClampedFloatParameter(50f, 1f, 500f);

    public ClampedFloatParameter bendStrength =
        new ClampedFloatParameter(0.15f, 0f, 2f);
}