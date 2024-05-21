#version 460 core

// VertexShader inputs
layout(location = 0) in vec3 position;
layout(location = 1) in vec4 color;

// VertexShader outputs
out vec4 vColor;

void main()
{
	gl_Position = vec4(position, 1.0);
	vColor = color;
}