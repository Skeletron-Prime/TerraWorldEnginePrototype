#version 460 core

// Fragmentshader inputs
in vec3 fragPosition; // Position of the vertex in world space
in vec3 fragNormal; // Normal of the vertex in world space
in vec3 objectColor; // Color of the object

// Uniforms
uniform vec3 lightPosition; // Position of the light in world space
uniform vec3 lightColor; // Color of the light
uniform vec3 viewPosition; // Position of the camera in world space

// FragmentShader outputs
out vec4 fragmentColor;

void main()
{
    // Normalize inputs
    vec3 norm = normalize(fragNormal);
    vec3 lightDir = normalize(lightPosition - fragPosition);
    vec3 viewDir = normalize(viewPosition - fragPosition);

    // Ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;

    // Diffuse
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    // Specular
    float specularStrength = 0.5;
    vec3 reflectDir = reflect(-lightDir, norm);
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 32);
    vec3 specular = specularStrength * spec * lightColor;

    // Combine results
    vec3 result = (ambient + diffuse + specular) * objectColor;
    fragmentColor = vec4(result, 1.0);
}
