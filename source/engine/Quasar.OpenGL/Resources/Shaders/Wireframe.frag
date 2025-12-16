#version 400 core

out vec4 FragColor;

uniform vec4 FillColor = vec4(0.85, 0.85, 0.85, 1.0);
uniform vec4 LineColor = vec4(0.25, 0.25, 0.25, 1.0);
uniform float Thickness = 1;

in vec3 distances;

void main()
{
	// Compute the shortest distance to the edge
	float shortestDistance = abs(min(distances[0], min(distances[1], distances[2])));
 
	// Compute line intensity and then fragment color
	float intensity = exp(-20.0 * shortestDistance / Thickness);
 
	// Interpolate between colors based on the intensity
	FragColor = intensity * LineColor + (1.0 - intensity) * FillColor; 
}
