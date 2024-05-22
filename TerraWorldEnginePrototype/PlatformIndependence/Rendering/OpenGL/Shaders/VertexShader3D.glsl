#version 460 core

// VertexShader inputs
layout(location = 0) in vec3 position;
layout(location = 1) in vec4 color;

// VertexShader outputs
out vec4 vColor;

// VertexShader uniforms 
uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main()
{
	gl_Position = projection * view * model * vec4(position, 1.0);
	vColor = color;
}