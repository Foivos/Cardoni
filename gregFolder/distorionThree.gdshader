shader_type canvas_item;

//! NICE HOMIE

uniform float amplitude_x : hint_range(-0.2, 0.2) = 0.05; // Distortion on the X axis
uniform float amplitude_y : hint_range(-0.2, 0.2) = 0.05; // Distortion on the Y axis
uniform float frequency : hint_range(-20.0, 20.0) = 7.5;
uniform float speed : hint_range(-5.0, 5.0) = 2.0;
uniform vec4 overlay_color : source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform vec2 texture_size = vec2(64.0, 64.0); // Texture size in pixels

void fragment() {
    // Converts UV coordinates to pixel coordinates
    vec2 pixel_coords = UV * texture_size;

    // Rounds the Y coordinate to ensure the distortion is applied per line
    pixel_coords.y = floor(pixel_coords.y) + 0.5;

    // Converts back to UV coordinates
    vec2 line_uv = pixel_coords / texture_size;

    // Applies distortion based on X and Y coordinates
    line_uv.x += sin(line_uv.y * frequency + TIME * speed) * amplitude_x; // Distortion on the X axis
    line_uv.y += sin(line_uv.y * frequency + TIME * speed) * amplitude_y; // Distortion on the Y axis

    // Fetches the texture color using the distorted coordinates
    vec4 texColor = texture(TEXTURE, line_uv);

    // Applies the overlay color
    COLOR = texColor * overlay_color;
}
