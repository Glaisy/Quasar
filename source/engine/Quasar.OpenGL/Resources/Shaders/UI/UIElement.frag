#version 400 core

out vec4 FragColor;

in vec2 uv;

uniform vec4 Color;
uniform sampler2D Texture;

void main()
{
	FragColor = texture(Texture, uv) * Color;
}