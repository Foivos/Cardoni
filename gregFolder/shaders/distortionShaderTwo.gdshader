shader_type canvas_item;

uniform float wave_speed : hint_range(0.0, 10.0, 0.1) = 1.0;
uniform float wave_amplitude : hint_range(0.0, 1.0, 0.00) = 0.05;
uniform float wave_frequency : hint_range(0.0, 10.0, 0.1) = 5.0;

void fragment() {

    vec2 uv = UV;  // Use the object's UV coordinates

    // Apply a sine wave-based distortion to the UV coordinates
    float distortion_x = sin(uv.y * wave_frequency + TIME * wave_speed) * wave_amplitude;
    float distortion_y = sin(uv.x * wave_frequency + TIME * wave_speed) * wave_amplitude;

    // Distort the UVs based on the wave calculations
    vec2 distorted_uv = uv + vec2(distortion_x, distortion_y);

    // Sample the texture of the current object using the distorted UVs
    COLOR = texture(TEXTURE, distorted_uv);
}

