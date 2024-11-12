#include "FBMNoiseInclude.cginc"
#include "VoronoiseInclude.cginc"

float sdf_sphere(float3 p, float3 sphereCenter, float sphereRadius)
{
    return length(p - sphereCenter) - sphereRadius;
}

float sdf_box(float3 p, float3 b)
{
    float3 q = abs(p) - b;
    return length(max(q,0.0)) + min(max(q.x,max(q.y,q.z)),0.0);
}

float smoothUnionSDF(float distA, float distB, float k ) {
    float h = clamp(0.5 + 0.5*(distA-distB)/k, 0., 1.);
    return lerp(distA, distB, h) - k*h*(1.-h);
}

float mapWorld(float3 p)
{
    float sphere1 = sdf_sphere(p, float3(0, 0.2, 0), 0.3);
    float box1 = sdf_box(p, float3(0.5, 0.1, 0.5));
    return smoothUnionSDF(sphere1, box1, 0.25);
}

float3 calculate_normal(in float3 p)
{
    const float3 small_step = float3(0.001, 0.0, 0.0);

    float gradient_x = mapWorld(p + small_step.xyy) - mapWorld(p - small_step.xyy);
    float gradient_y = mapWorld(p + small_step.yxy) - mapWorld(p - small_step.yxy);
    float gradient_z = mapWorld(p + small_step.yyx) - mapWorld(p - small_step.yyx);

    float3 normal = float3(gradient_x, gradient_y, gradient_z);

    return normalize(normal);
}

void raymarch_float(float3 rayOrigin, float3 rayDirection, float stepCount, float maxDistance, float minDistance,
    out float3 objectPos, out float3 objectNormal, out float hit, out float distanceTraveled)
{
    distanceTraveled = 0;
    
    for (int i = 0; i < stepCount; i++) {
        float3 currentPos = rayOrigin + distanceTraveled * rayDirection;
        
        float distanceToClosest = mapWorld(currentPos);
        
        if (distanceToClosest < minDistance) {
            objectPos = currentPos;
            objectNormal = calculate_normal(currentPos);
            hit = 1;
            return;
        }

        if (distanceTraveled > maxDistance) {
            break;
        }
        distanceTraveled += distanceToClosest;
    }
    objectPos = float3(0, 0, 0);
    objectNormal = float3(0, 0, 0);
    hit = 0;
    
}

void raymarchvolume_float(float3 rayOrigin, float3 rayDirection, float stepCount, float stepSize, float densityScale,
    UnityTexture3D volumeTex, float numLightSteps, float lightStepSize, float3 lightDirection, float lightAbsorb, float darknessThreshold, float transmittance,
    out float density, out float transmission, out float lightAccumulation, out float finalLight)
{
    density = 0;
    transmission = 0;
    lightAccumulation = 0;
    finalLight = 0;
    
    for (int i = 0; i < stepCount; i++) {
        rayOrigin += (rayDirection * stepSize);

        float3 samplePos = rayOrigin + 0.5;
        
        float sampledDensity = tex3D(volumeTex, samplePos).r;
        density += sampledDensity * densityScale;

        // Light loop
        float3 lightRayOrigin = samplePos;
        for (int j = 0; j < numLightSteps; j++)
        {
            lightRayOrigin += lightDirection * lightStepSize;
            float lightDensity = tex3D(volumeTex, lightRayOrigin).r;
            lightAccumulation += lightDensity * densityScale;
        }

        float lightTransmission = exp(-lightAccumulation);
        float shadow = darknessThreshold + lightTransmission * (1 - darknessThreshold);
        finalLight += density * transmittance * shadow;
        transmittance *= exp(-density * lightAbsorb);
    }
    transmission = exp(-density);
}

void raymarchvolume_half(half3 rayOrigin, half3 rayDirection, half stepCount, half stepSize, half densityScale,
    UnityTexture3D volumeTex, half numLightSteps, half lightStepSize, half3 lightDirection, half lightAbsorb, half darknessThreshold, half transmittance,
    out half density, out half transmission, out half lightAccumulation, out half finalLight)
{
    density = 0;
    transmission = 0;
    lightAccumulation = 0;
    finalLight = 0;
    
    for (int i = 0; i < stepCount; i++) {
        rayOrigin += (rayDirection * stepSize);

        float3 samplePos = rayOrigin + 0.5;
        
        float sampledDensity = tex3D(volumeTex, samplePos).r;
        density += sampledDensity * densityScale;

        // Light loop
        float3 lightRayOrigin = samplePos;
        for (int j = 0; j < numLightSteps; j++)
        {
            lightRayOrigin += lightDirection * lightStepSize;
            float lightDensity = tex3D(volumeTex, lightRayOrigin).r;
            lightAccumulation += lightDensity * densityScale;
        }

        float lightTransmission = exp(-lightAccumulation);
        float shadow = darknessThreshold + lightTransmission * (1 - darknessThreshold);
        finalLight += density * transmittance * shadow;
        transmittance *= exp(-density * lightAbsorb);
    }
    transmission = exp(-density);
}