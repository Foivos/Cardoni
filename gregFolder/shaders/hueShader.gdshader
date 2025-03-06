shader_type canvas_item;

uniform float hue_shift : hint_range(-1.0, 1.0) = 0.0;  // Hue shift value

// Function to convert RGB to HSV
vec3 rgb2hsv(vec3 c) {
    vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
    vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return vec3(abs((q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

// Function to convert HSV back to RGB
vec3 hsv2rgb(vec3 c) {
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

void fragment() {
    // Sample the texture color
    vec4 tex_color = texture(TEXTURE, UV);
    
    // Convert the RGB color to HSV
    vec3 hsv = rgb2hsv(tex_color.rgb);
    
    // Shift the hue by the uniform value, only if hue_shift is non-zero
    if (abs(hue_shift) > 0.0) {
        hsv.x += hue_shift;
        hsv.x = fract(hsv.x);  // Wrap the hue between 0.0 and 1.0
    }
    
    // Convert back to RGB
    vec3 rgb = hsv2rgb(hsv);
    
    // Set the final color
    COLOR = vec4(rgb, tex_color.a);  // Maintain the original alpha value
}