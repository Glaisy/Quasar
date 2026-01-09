#version 400 core

layout (location = 0) in vec3 VertexPosition;
layout (location = 1) in vec3 VertexNormal;
layout (location = 2) in vec3 VertexTangent;
layout (location = 3) in vec2 VertexUV;

#include <Arguments/MainLight.inc>

#include <Matrices/MVP.inc>
#include <Matrices/Model.inc>

out vec2 uv;
out vec3 lightDirection;

void main()
{
    gl_Position = ModelViewProjectionMatrix * vec4(VertexPosition, 1.0);
    uv = VertexUV;

    // normal mapping
    vec3 normal = normalize((ModelMatrix * vec4(VertexNormal, 0)).xyz);
    vec3 tangent = normalize((ModelMatrix * vec4(VertexTangent.xyz, 0)).xyz);
    vec3 bitangent = normalize(cross(tangent, normal));

    lightDirection = vec3(
        dot(tangent, LightDirectionWS),
        dot(bitangent, LightDirectionWS),
        dot(normal, LightDirectionWS));
}
