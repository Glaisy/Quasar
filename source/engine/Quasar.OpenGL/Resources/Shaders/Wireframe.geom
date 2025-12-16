#version 400 core

layout(triangles) in;
layout(triangle_strip, max_vertices=3) out;

uniform float Thickness = 1;

out vec3 distances;

void main()
{
	float scale = 1.0 / max(0.5, Thickness);

	// Compute 3 points from vertices
	vec4 v0 = gl_in[0].gl_Position;  
	vec2 p0 = v0.xy / v0.w; 
 
	vec4 v1 = gl_in[1].gl_Position;  
	vec2 p1 = v1.xy / v1.w; 

	vec4 v2 = gl_in[2].gl_Position;  
	vec2 p2 = v2.xy / v2.w; 
 
	//--------------------------------
	// Project p1 and p2 and compute the vectors v1 = p1-p0
	// and v2 = p2-p0                                  
	vec2 v10 = scale * (p1 - p0);   
	vec2 v20 = scale * (p2 - p0);   
 
	// Compute 2D area of triangle.
	float area0 = abs(v10.x * v20.y - v10.y * v20.x);
 
	// Compute distance from vertex to line in 2D coords
	float h0 = area0 / length(v10 - v20); 
 
	distances = vec3(h0 * v0.w, 0.0, 0.0);
	gl_Position = v0;
	EmitVertex();
  
	//--------------------------------
	// Project p0 and p2 and compute the vectors v01 = p0-p1
	// and v21 = p2-p1                                  
	vec2 v01 = scale *(p0 - p1);   
	vec2 v21 = scale *(p2 - p1);   
 
	// Compute 2D area of triangle.
	float area1 = abs(v01.x * v21.y - v01.y * v21.x);
 
	// Compute distance from vertex to line in 2D coords
	float h1 = area1 / length(v01 - v21); 
 
	distances = vec3(0.0, h1 * v1.w, 0.0);
	gl_Position = v1;
	EmitVertex();
 
	//--------------------------------
	// Project p0 and p1 and compute the vectors v02 = p0-p2
	// and v12 = p1-p2                                  
	vec2 v02 = scale *(p0 - p2);   
	vec2 v12 = scale *(p1 - p2);   
 
	// Compute 2D area of triangle.
	float area2 = abs(v02.x * v12.y - v02.y * v12.x);
 
	// Compute distance from vertex to line in 2D coords
	float h2 = area2 / length(v02 - v12); 
 
	distances = vec3(0.0, 0.0, h2 * v2.w);
 	gl_Position = v2;
	EmitVertex();
 
	EndPrimitive();
}
