#version 400 core

#include <Arguments/DiffuseMap.inc>
#include <Arguments/NormalMap.inc>
#include <Arguments/AmbientLight.inc>
#include <Arguments/MainLight.inc>

in vec2 uv;
in vec3 lightDirection;

out vec4 FragColor;

#include <Utilities/NormalMapping.inc>

void main()
{
    // normal mapping
    vec3 normal = ExtractNormal(NormalMap, uv, NormalStrength);
    float nDotL = dot(normal, lightDirection);

    // diffuse lighting
    float brightness = max(AmbientLightIntensity, nDotL);
    vec3 diffuseLight = DiffuseColor.rgb * LightColor.rgb * brightness;

    FragColor = texture(DiffuseMap, uv) * vec4(diffuseLight, DiffuseColor.a);
}
