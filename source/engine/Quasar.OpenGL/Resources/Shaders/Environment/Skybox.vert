#version 400 core

layout (location = 0) in vec3 VertexPosition;

uniform mat4 ViewRotationProjectionMatrix;

out vec3 cubeMapCoordinates;

void main()
{
    vec4 position = ViewRotationProjectionMatrix * vec4(VertexPosition, 1.0);
    gl_Position = position.xyww;
    cubeMapCoordinates = vec3(VertexPosition.x, -VertexPosition.y, VertexPosition.z);
}
