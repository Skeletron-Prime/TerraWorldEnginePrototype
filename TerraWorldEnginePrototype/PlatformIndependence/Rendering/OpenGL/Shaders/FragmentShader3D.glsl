#version 460 core

// Fragmentshader inputs
in vec4 vColor;

// FragmentShader outputs
out vec4 fragmentColor;

void main()
{
	fragmentColor = vColor;
}
