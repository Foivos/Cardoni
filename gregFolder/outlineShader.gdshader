shader_type canvas_item;

uniform int width:hint_range(0, 5, 1);
uniform vec4 outline_color:source_color;

void fragment() {
    float pixel_size_x = 1.0 / float(textureSize(TEXTURE, 0).x);
    float pixel_size_y = 1.0 / float(textureSize(TEXTURE, 0).y);
    vec2 pixel_size = vec2(pixel_size_x, pixel_size_y);
    
    COLOR = texture(TEXTURE, UV);
    
    if (texture(TEXTURE, UV).a == 0.) {
        for (int x = -width; x <= width; x++) {
            for (int y = -width; y <= width; y++) {
                vec2 pixel_offset = vec2(float(x), float(y));
                if (texture(TEXTURE, UV + (pixel_offset * pixel_size)).a > 0.) {
                    COLOR = outline_color;
                }
            }
        }
    } else {
        COLOR = texture(TEXTURE, UV);
    }
}
