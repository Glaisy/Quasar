#version 400 core

#include <Arguments/DiffuseMap.inc>

in vec2 uv;

out vec4 FragColor;

void main()
{
    FragColor = texture(DiffuseMap, uv) * DiffuseColor;
}
