# MonoGame_fonts_black_borders
A test-repo to showcase a strange behavior that may be a bug.



### Notes

The repo should contain all the files necessary for compilation.

I've tried this using the latest develop NuGet available (3.6.0.1551)

The project is WindowsDX as is my game that I've encountered this behavior on.



##### Symptoms

![results](https://github.com/UnterrainerInformatik/MonoGame_fonts_black_borders/blob/master/MG3.6.0_pipeline.png)

As you can see there are black borders around most of the glyphs.
Seems to depend on the SamplerState AND the size of the font that is drawn.

###### Observations

The borders are only visible when:

- The pipeline tool is from the develop branch (post 3.5.1.1679)
- The font used is build from a font-texture (bitmap-font)
  - It doesn't happen with SpriteFonts (the thing built from an installed TTF with the descriptor XML)
- It happens when the scale of the font drawn is set to anything bigger 1f (```new Vector2(1.1f, 1.1f)``` or ```new Vector2(2f, 2f)```, not ```Vector2.One```)
  - It doesn't happen when the SamplerState is ```LinearClamp``` or ```PointClamp``` and the scale is ```new Vector2(1, 1)```.
  - It never happens when the scale is set so something smaller than 1f.

##### Seems to be the pipeline

I re-installed MG 3.5.1.1679 so the pipeline that's used during build is of that version and, lo and behold, it works again.

![results](https://github.com/UnterrainerInformatik/MonoGame_fonts_black_borders/blob/master/MG3.5.1_pipeline.png)


