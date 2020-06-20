#ifndef PBRLIB_INCLUDED
#define PBRLIB_INCLUDED
half EnvBRDFApproxNonmetal_UE4(half roughness, half ndv)
{
    // Same as EnvBRDFApprox( 0.04, roughness, ndv )
    const half2 c0 = { -1, -0.0275 };
    const half2 c1 = { 1, 0.0425 };
    half2 r = roughness * c0 + c1;
    return min( r.x * r.x, exp2( -9.28 * ndv ) ) * r.x + r.y;
}
#endif