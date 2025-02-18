shader_type canvas_item;

uniform float ripple_speed : hint_range(0.0, 10.0, 0.1) = 1.0;
uniform float ripple_amplitude : hint_range(0.0, 1.0, 0.01) = 0.05;
uniform float ripple_frequency : hint_range(0.0, 20.0, 0.01) = 10.0;
uniform vec2 ripple_center  ;//: hint_range(-1024.0, 1024.0, 1.0) = vec2(0.5, 0.5);

void fragment() {
    vec2 uv = UV;

    // Calculate distance from the ripple center to current UV coordinates
    vec2 ripple_pos = uv - ripple_center;
    float distance = length(ripple_pos);

    // Apply ripple distortion based on distance and time
    float ripple = sin(distance * ripple_frequency - TIME * ripple_speed) * ripple_amplitude;

    // Distort UV coordinates
    vec2 distorted_uv = uv + normalize(ripple_pos) * ripple;

    // Sample the texture using the distorted UVs
    COLOR = texture(TEXTURE, distorted_uv);
}
