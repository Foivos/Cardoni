shader_type canvas_item;

uniform float aberration_strength : hint_range(0.0, 10.0, 0.1) = 1.0;

void fragment() {
    vec2 uv = UV;

    // Offset for each color channel (Red, Green, Blue)
    vec2 offset_r = vec2(0.005, 0.0) * aberration_strength;
    vec2 offset_b = vec2(-0.005, 0.0) * aberration_strength;

    // Sample the texture at slightly different UVs for each color channel
    float r = texture(TEXTURE, uv + offset_r).r;
    float g = texture(TEXTURE, uv).g;
    float b = texture(TEXTURE, uv + offset_b).b;

    // Combine the colors back together
    COLOR = vec4(r, g, b, 1.0);
}
