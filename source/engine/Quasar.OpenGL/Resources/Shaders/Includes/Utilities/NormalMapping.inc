vec3 ExtractNormal(sampler2D normalMap, vec2 uv, float strength)
{
    vec3 extractedNormal = (texture2D(normalMap, uv).rgb * 2.0) - 1.0;
    extractedNormal.rg *= strength;
    return normalize(extractedNormal);
}