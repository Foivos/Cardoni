//shader_type canvas_item;
//void vertex() {}
	// Called for every vertex the material is visible on.
//void fragment() {}
	// Called for every pixel the material is visible on.

shader_type canvas_item;

uniform vec2 scroll_speed = vec2(0.1, 0.0); // Speed of scrolling

void fragment() {
    vec2 uv = FRAGCOORD.xy / TEXTURE_PIXEL_SIZE.xy;
    uv += scroll_speed * TIME; // Scroll the texture over time
    vec4 color = texture(TEXTURE, uv);
    COLOR = color;
}

