#version 460 core

// Struct for light properties: position, color and intensity
struct Light
{
	vec3 position;
	vec3 color;
	float intensity;
};

layout(std140, binding = 0) buffer Lights
{
	Light lights[];
};

// Fragmentshader inputs
in vec3 fragPosition; // Position of the vertex in world space
in vec3 fragNormal; // Normal of the vertex in world space
in vec3 objectColor; // Color of the object

// Uniforms
uniform vec3 viewPosition; // Position of the camera in world space

// FragmentShader outputs
out vec4 fragmentColor;

void main()
{
	vec3 result = vec3(0.0);

	for (int i = 0; i < lights.length(); i++)
	{
		// Ambient 
		float ambientStrength = 0.1;
		vec3 ambient = ambientStrength * lights[i].color;

		// Diffuse
		vec3 norm = normalize(fragNormal);
		vec3 lightDir = normalize(lights[i].position - fragPosition);
		float diff = max(dot(norm, lightDir), 0.0);
		vec3 diffuse = diff * lights[i].color;

		// Specular
		float specularStrength = 0.5;
		vec3 viewDir = normalize(viewPosition - fragPosition);
		vec3 reflectDir = reflect(-lightDir, norm);
		float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
		vec3 specular = specularStrength * spec * lights[i].color;

		// Attenuation
		float distance = length(lights[i].position - fragPosition);
		float attenuation = 1.0 / (1.0 + 0.09 * distance + 0.032 * distance * distance);

		// Final color
		vec3 resultLight = (ambient + attenuation * (diffuse + specular)) * objectColor;
		result += lights[i].intensity * resultLight;
	}

    fragmentColor = vec4(result, 1.0);
}
