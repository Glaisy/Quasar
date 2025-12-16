#version 400 core

layout (location = 0) in vec3 VertexPosition;

#include <Matrices/MVP.inc>

void main()
{
    gl_Position = ModelViewProjectionMatrix * vec4(VertexPosition, 1.0);
}
