#version 330

uniform sampler2D tex;

uniform int hlpoints[10];
uniform float hlcolors[40];
uniform vec4 ncolor;

flat in int foundpoint;

out vec4 fragColor;

void main()
{
    int found = foundpoint;

    vec4 drawColor;

    if (found == -1)
    {
		drawColor = ncolor;
    }
    else
    {
        drawColor = vec4(hlcolors[found * 4], hlcolors[found * 4 + 1], hlcolors[found * 4 + 2], hlcolors[found * 4 + 3]);
    }    

    vec4 texOut = texture2D(tex, gl_PointCoord);
    fragColor =  drawColor * texOut.a;
}