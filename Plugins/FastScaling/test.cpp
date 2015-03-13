#define CATCH_CONFIG_MAIN // tell catch to generate main
#include "catch.hpp"

#include "unmanaged_renderer.h"


bool test(int sx, int sy, int sbpp, int cx, int cy, int cbpp, InterpolationFilter filter)
{
    BitmapBgraPtr source = CreateBitmapBgra(sx, sy, true, sbpp);
    BitmapBgraPtr canvas = CreateBitmapBgra(cx, cy, true, cbpp);

    RenderDetailsPtr details = CreateRenderDetails();

    details->interpolation = CreateInterpolation(filter);

    details->sharpen_percent_goal = 50;


    RendererPtr p = CreateRenderer(source, canvas, details);

    PerformRender(p);

    DestroyRenderer(p);

    DestroyBitmapBgra(source);
    DestroyBitmapBgra(canvas);

    FreeLookupTables();
    return true;
}

TEST_CASE( "Render without crashing", "[fastscaling]") {
    REQUIRE( test(4000,3000,4,200,40,4,(InterpolationFilter)0) );
}

TEST_CASE( "Render with crashing", "[fastscaling]") {
    REQUIRE( test(200, 40, 4, 4000, 3000, 4, (InterpolationFilter)0) );
}
