#version 330

in vec4 vert;

uniform mat4 projection;
uniform mat4 modelView;
uniform float pointsize;
uniform int hlpoints[10];
uniform float hlsizes[10];

flat out int foundpoint;

void main()
{
    int found = -1;

    for(int i = 0; i < 10; i++)
    {
        if(gl_VertexID == hlpoints[i])
        {
            found = i;
        }
    }

    if (found == -1)
    {
        gl_PointSize = pointsize;
    }
    else
    {
        gl_PointSize = pointsize + hlsizes[found];
    }

    foundpoint = found;
    gl_Position = projection * modelView * vert;

}