//shader_type canvas_item;
//void vertex() {}
	// Called for every vertex the material is visible on.
//void fragment() {}
	// Called for every pixel the material is visible on.

shader_type canvas_item;

uniform vec2 scroll_speed = vec2(0.1, 0.0); // Speed of scrolling

//! WORKING !! !!
void fragment() {
    // Use the provided UV coordinates from the texture
    vec2 uv = UV;

    // Scroll the UV coordinates based on time and speed
    uv += scroll_speed * TIME;

    // Wrap the UV coordinates to stay within the range [0, 1]
    uv = mod(uv, vec2(1.0, 1.0));

    // Sample the texture using the wrapped UV coordinates
    vec4 color = texture(TEXTURE, uv);

    // Set the output color
    COLOR =  color ;//* COLOR;
    // COLOR = texture(TEXTURE, UV);
}



// void fragment() {
//     vec2 uv = FRAGCOORD.xy / TEXTURE_PIXEL_SIZE.xy;
//     uv += scroll_speed * TIME; // Scroll the texture over time
//     uv = mod(uv, vec2(1.0, 1.0));//!new
//     vec4 color = texture(TEXTURE, uv);
//     COLOR = color;
// }

