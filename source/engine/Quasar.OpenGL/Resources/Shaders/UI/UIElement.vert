#version 400 core

layout (location = 0) in vec3 VertexPosition;
layout (location = 1) in vec2 VertexUV;

#include <Matrices/Projection.inc>

uniform vec2 Position;
uniform vec2 Scale = vec2(1);

out vec2 uv;

void main()
{
	gl_Position = ProjectionMatrix * vec4(VertexPosition.xy * Scale + Position, 0.0, 1.0);
	uv = VertexUV;
}