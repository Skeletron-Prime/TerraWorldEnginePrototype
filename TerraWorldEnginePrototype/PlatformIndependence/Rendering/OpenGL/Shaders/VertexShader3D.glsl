#version 460 core

// VertexShader inputs
layout(location = 0) in vec3 position; // Vertex position
layout(location = 1) in vec3 normal; // Vertex normal
layout(location = 2) in vec3 color; // Vertex color

// VertexShader outputs
out vec3 fragPosition; // Position of the vertex in world space
out vec3 fragNormal; // Normal of the vertex in world space
out vec3 objectColor; // Color of the vertex

// VertexShader uniforms 
uniform mat4 model; // Model matrix
uniform mat4 view; // View matrix
uniform mat4 projection; // Projection matrix

void main()
{
	gl_Position = projection * view * model * vec4(position, 1.0); // Calculate the vertex position in clip space
	fragPosition = vec3(model * vec4(position, 1.0)); // Calculate the vertex position in world space
	fragNormal = mat3(transpose(inverse(model))) * normal; // Calculate the vertex normal in world space
	objectColor = color; // Set the vertex color
}