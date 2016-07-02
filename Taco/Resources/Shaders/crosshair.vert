#version 330

layout(location = 0) in vec3 vert;

uniform mat4 projection;
uniform mat4 modelView;

void main()
{
    gl_PointSize = 26.0;
    gl_Position = projection * modelView * vec4(vert, 1);
}