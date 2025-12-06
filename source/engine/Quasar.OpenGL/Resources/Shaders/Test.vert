#version 400 core
layout (location = 0) in vec3 vPosition; 
layout (location = 1) in vec4 vColor; 
  
#include <Matrices/MVP.inc>

out vec4 fColor;

void main()
{
    gl_Position = ModelViewProjectionMatrix * vec4(vPosition, 1.0);
    fColor = vColor;
}