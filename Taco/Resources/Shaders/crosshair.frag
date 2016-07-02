#version 330

uniform sampler2D tex;

out vec4 fragColor;

void main()
{
    vec4 texOut = texture2D(tex, gl_PointCoord);
    fragColor =  texOut;
}