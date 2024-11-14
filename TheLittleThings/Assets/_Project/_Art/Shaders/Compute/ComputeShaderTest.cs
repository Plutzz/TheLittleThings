using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComputeShaderTest : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;
    public Material material;
    public Vector2Int resolution = new Vector2Int(512, 512);
    public int divisor = 8;
    public static Texture3D tex3D;
    // Start is called before the first frame update
    void Start() {
        renderTexture = new RenderTexture(resolution.x, resolution.y, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
        
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.SetInt("width", renderTexture.width);
        computeShader.SetInt("height", renderTexture.height);
        computeShader.SetInt("divisor", divisor);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Generate Texture")]
    void CreateTexture() {
        renderTexture = new RenderTexture(resolution.x, resolution.y, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();
        
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.SetInt("width", renderTexture.width);
        computeShader.SetInt("height", renderTexture.height);
        computeShader.SetInt("divisor", divisor);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
        
        material.SetTexture("_Tex", renderTexture);
    }

    [ContextMenu("Create 3D Texture")]
    void Create3DTexture()
    {
        if (tex3D == null)
        {
            tex3D = new Texture3D(resolution.x, resolution.x, resolution.x, TextureFormat.RFloat, false);
        }
        int pixels = resolution.x * resolution.x * resolution.x;
        ComputeBuffer buffer = new ComputeBuffer(pixels, sizeof(float));
        computeShader.SetBuffer(0, "Result", buffer);
    }
    
    [ContextMenu("Save Noise")]
    void CreateTexture3D()
    {
        // Save the texture to your Unity Project
        AssetDatabase.CreateAsset(tex3D, "Assets/_Project/_Art/Shaders/Compute/3DTexture.asset");
    }
    
}
