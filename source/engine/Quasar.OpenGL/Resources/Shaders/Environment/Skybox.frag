#version 400 core

out vec4 FragColor;

in vec3 cubeMapCoordinates;

uniform float Exposure = 1.0;
uniform vec4 TintColor = vec4(1);
uniform samplerCube CubeMapTexture;

void main()
{
    vec4 textureColor = texture(CubeMapTexture, cubeMapCoordinates);
    vec3 skyboxColor = textureColor.rgb * TintColor.rgb * Exposure;
    FragColor = vec4(skyboxColor.rgb,  1.0);
}
