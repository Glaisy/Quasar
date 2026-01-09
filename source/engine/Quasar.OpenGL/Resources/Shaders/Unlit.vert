#version 400 core

layout (location = 0) in vec3 VertexPosition;
layout (location = 1) in vec2 VertexUV;

#include <Matrices/MVP.inc>

out vec2 uv;

void main()
{
    gl_Position = ModelViewProjectionMatrix * vec4(VertexPosition, 1.0);
    uv = VertexUV;
}
