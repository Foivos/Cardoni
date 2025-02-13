shader_type canvas_item;

uniform float strength = 0.05;  // Strength of the distortion effect
uniform float frequency = 10.0; // Frequency of the wave distortion
uniform float speed = 1.0;      // Speed of the distortion over time

void fragment() {
    // Get the base UV coordinates
    vec2 uv = UV;

    // Create a wave distortion effect using sin()
    uv.x += sin(uv.y * frequency + TIME * speed) * strength;
    uv.y += sin(uv.x * frequency + TIME * speed) * strength;

    // Sample the texture with the distorted UV coordinates
    vec4 color = texture(TEXTURE, uv);

    // Output the final color
    COLOR = color * COLOR;
}

