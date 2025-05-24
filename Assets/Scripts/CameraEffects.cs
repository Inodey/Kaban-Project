using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class CameraEffects : MonoBehaviour
{
    [Header("Grain + Chromatic Aberration")]
    [Range(0f, 1f)] public float grainIntensity = 0.2f;
    [Range(0f, 5f)] public float chromaticAberrationIntensity = 1f;

    [Header("Vignette")]
    [Range(0f, 1f)] public float vignetteIntensity = 0.4f;

    [Header("Color")]
    [Range(0f, 1f)] public float desaturationAmount = 0.3f;

    [Header("Bloom")]
    [Range(0f, 2f)] public float bloomIntensity = 0.5f;

    [Header("Lens Dirt")]
    public Texture2D lensDirtTexture;
    [Range(0f, 1f)] public float lensDirtIntensity = 0.3f;

    [Header("Depth of Field (Blur)")]
    [Range(0f, 10f)] public float blurSize = 1.5f;
    public bool enableBlur = true;

    [Header("Barrel Distortion")]
    [Range(-1f, 1f)] public float barrelDistortion = 0.1f;

    [Header("Rolling Shutter Wobble")]
    [Range(0f, 1f)] public float wobbleIntensity = 0.2f;

    [Header("VHS + Analog Effects")]
    public bool enableVHS = true;
    [Range(0f, 1f)] public float vhsNoiseStrength = 0.25f;
    [Range(0f, 1f)] public float scanlineIntensity = 0.2f;

    public Shader postProcessShader;
    private Material postProcessMaterial;

    void Start()
    {
        if (postProcessShader == null)
        {
            Debug.LogError("Assign the postProcessShader in inspector.");
            enabled = false;
            return;
        }

        postProcessMaterial = new Material(postProcessShader);
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (postProcessMaterial == null)
        {
            Graphics.Blit(src, dest);
            return;
        }

        // Pass params to shader
        postProcessMaterial.SetFloat("_GrainIntensity", grainIntensity);
        postProcessMaterial.SetFloat("_ChromaticAberrationIntensity", chromaticAberrationIntensity);
        postProcessMaterial.SetFloat("_VignetteIntensity", vignetteIntensity);
        postProcessMaterial.SetFloat("_DesaturationAmount", desaturationAmount);
        postProcessMaterial.SetFloat("_BloomIntensity", bloomIntensity);

        postProcessMaterial.SetTexture("_LensDirtTex", lensDirtTexture);
        postProcessMaterial.SetFloat("_LensDirtIntensity", lensDirtIntensity);

        postProcessMaterial.SetFloat("_BlurSize", enableBlur ? blurSize : 0f);
        postProcessMaterial.SetFloat("_BarrelDistortion", barrelDistortion);
        postProcessMaterial.SetFloat("_WobbleIntensity", wobbleIntensity);

        postProcessMaterial.SetFloat("_EnableVHS", enableVHS ? 1f : 0f);
        postProcessMaterial.SetFloat("_VHSNoiseStrength", vhsNoiseStrength);
        postProcessMaterial.SetFloat("_ScanlineIntensity", scanlineIntensity);
        
        Graphics.Blit(src, dest, postProcessMaterial);
    }
}